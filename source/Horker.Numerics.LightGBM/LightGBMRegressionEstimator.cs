using System;
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
    public abstract class LightGBMRegressionEstimator : IEstimator, IDisposable
    {
        private Parameters _parameters;
        private RegressionTrainer _trainer;
        private NativePredictorBase<double> _predicator;
        private string[] _outputCategories;

        public DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public RegressionTrainer Trainer => _trainer;
        public NativePredictorBase<double> Predictor => _predicator;

        public string[] OutputCategories
        {
            get
            {
                if (_outputCategories == null)
                    _outputCategories = new[] { "y" };

                return _outputCategories;
            }

            set
            {
                _outputCategories = value;
            }
        }

        public LightGBMRegressionEstimator(Parameters parameters)
        {
            if (parameters == null)
                parameters = new Parameters();

            _parameters = parameters;
            _trainer = new RegressionTrainer(parameters.Learning, parameters.Objective);
        }

        public LightGBMRegressionEstimator(NativePredictorBase<double> predictor)
        {
            _predicator = predictor;
        }

        public void Save(string path)
        {
            var text = _predicator.Booster.GetModelString();
            File.WriteAllText(path, text);
        }

        public void Fit(DataMap x, DataMap y, DataMap validX, DataMap validY)
        {
            var trainDense = new DataDense()
            {
                Features = x.ToJagged<float>(),
                Labels = y.First.AsArray<float>()
            };

            DataDense validDense = null;
            if (validX != null)
            {
                validDense = new DataDense()
                {
                    Features = validX.ToJagged<float>(),
                    Labels = validY.First.AsArray<float>()
                };
            }

            using (var datasets = new Datasets(_parameters.Common, _parameters.Dataset, trainDense, validDense))
            {
                datasets.Training.SetFeatureNames(x.ColumnNames.ToArray());
                if (validX != null)
                    datasets.Validation.SetFeatureNames(validX.ColumnNames.ToArray());

                var predicators = _trainer.Train(datasets);
                _predicator = (NativePredictorBase<double>)predicators.Native;
            }
        }

        public void Fit(DataMap x, DataMap y)
        {
            Fit(x, y, null, null);
        }

        public DataMap Predict(DataMap x, Booster.PredictType predictType)
        {
            var pred = _predicator.Booster.PredictForMatsMulti(predictType, (float[][])x, -1);
            return DataMap.From2DArray(pred, OutputCategories);
        }

        public DataMap Predict(DataMap x)
        {
            return Predict(x, Booster.PredictType.Normal);
        }

        public DataMap GetFeatureImportance(Booster.ImportanceType importanceType, int numIteration = 0)
        {
            var featureNames = _predicator.Booster.FeatureNames;
            var imp = _predicator.Booster.GetFeatureImportance(numIteration, importanceType);

            var result = new DataMap();
            result.Add("Name", featureNames);
            result.Add("Importance", imp);

            return result;
        }

        public double Score(DataMap x, DataMap y)
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
