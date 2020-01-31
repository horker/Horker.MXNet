using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Estimators;
using Horker.Numerics.Transformers;
using Horker.Numerics.Utilities;
using LightGBMNet.Train;

namespace Horker.Numerics.LightGBM
{
    public class LightGBMRankingEstimator : LightGBMEstimatorBase<double>, IDisposable
    {
        private Parameters _parameters;
        private RankingTrainer _trainer;
        private string[] _outputCategories;
        private string _groupColumnName;
        private int[] _groups;
        private int[] _groupsValid;
        private bool _keepGroupColumn;

        public override DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public RankingTrainer Trainer => _trainer;

        public bool KeepGroupColumn
        {
            get => _keepGroupColumn;
            set => _keepGroupColumn = value;
        }

        public string[] OutputCategories
        {
            get
            {
                if (_outputCategories == null)
                    _outputCategories = new string[] { "Class0" };

                return _outputCategories;
            }

            set
            {
                _outputCategories = value;
            }
        }

        public string GroupColumnName
        {
            get => _groupColumnName;
            set => _groupColumnName = value;
        }

        public int[] Groups
        {
            get => _groups;
            set => _groups = value;
        }

        public int[] GroupsValid
        {
            get => _groupsValid;
            set => _groupsValid = value;
        }

        public static int[] GetGroups(IList groupColumn)
        {
            if (groupColumn.Count == 0)
                throw new ArgumentException("groupColumn should not be empty");

            var result = new List<int>();

            var last = groupColumn[0];
            int count = 0;
            for (var i = 0; i < groupColumn.Count; ++i)
            {
                if (groupColumn[i].Equals(last))
                {
                    ++count;
                }
                else
                {
                    result.Add(count);
                    last = groupColumn[i];
                    count = 1;
                }
            }

            result.Add(count);
            return result.ToArray();
        }

        public LightGBMRankingEstimator(Parameters parameters)
        {
            if (parameters == null)
                parameters = new Parameters();

            _parameters = parameters;
            _trainer = new RankingTrainer(parameters.Learning, parameters.Objective);
            _keepGroupColumn = false;
        }

        public LightGBMRankingEstimator(NativePredictorBase<double> predictor)
        {
            _predicator = predictor;
        }

        public LightGBMRankingEstimator(string path)
            : this(new RankingNativePredictor(LoadBooster(path)))
        { }

        public void Fit(DataMap x, DataMap y, DataMap validX, DataMap validY)
        {
            DataMap x0 = x;
            DataMap validX0 = validX;
            if (_groupColumnName != null)
            {
                _groups = GetGroups(x[_groupColumnName].UnderlyingList);
                if (!_keepGroupColumn)
                    x0 = x.UnselectColumns(_groupColumnName);
                if (validX != null)
                {
                    _groupsValid = GetGroups(validX[_groupColumnName].UnderlyingList);
                    if (!_keepGroupColumn)
                        validX0 = validX.UnselectColumns(_groupColumnName);
                }
            }

            if (_groups == null)
                throw new ArgumentException("One of Groups or GroupColumnName should be specified");

            if (validX != null && _groupsValid == null)
                throw new ArgumentException("One of GroupsValid or GroupColumnName should be specified");


            var trainDense = new DataDense()
            {
                Features = x0.ToJagged<float>(),
                Labels = y.First.AsArray<float>(),
                Groups = _groups
            };

            DataDense validDense = null;
            if (validX != null)
            {
                validX0 = validX.UnselectColumns(_groupColumnName);
                validDense = new DataDense()
                {
                    Features = validX0.ToJagged<float>(),
                    Labels = validY.First.AsArray<float>(),
                    Groups = _groupsValid
                };
            }

            using (var datasets = new Datasets(_parameters.Common, _parameters.Dataset, trainDense, validDense))
            {
                datasets.Training.SetFeatureNames(x0.ColumnNames.ToArray());
                if (validX0 != null)
                    datasets.Validation.SetFeatureNames(validX0.ColumnNames.ToArray());

                var predicators = _trainer.Train(datasets);
                _predicator = (NativePredictorBase<double>)predicators.Native;
            }
        }

        public override void Fit(DataMap x, DataMap y)
        {
            Fit(x, y, null, null);
        }

        public DataMap Predict(DataMap x, Booster.PredictType predictType)
        {
            DataMap x0 = x;
            if (_groupColumnName != null && !_keepGroupColumn)
                x0 = x.UnselectColumns(_groupColumnName);

            var pred = _predicator.Booster.PredictForMatsMulti(predictType, (float[][])x0, -1);
            return DataMap.From2DArray(pred, OutputCategories);
        }

        public override DataMap Predict(DataMap x)
        {
            return Predict(x, Booster.PredictType.Normal);
        }

        public override double Score(DataMap x, DataMap y)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_trainer != null)
                _trainer.Dispose();

            if (_predicator != null)
                _predicator.Dispose();
        }
    }
}
