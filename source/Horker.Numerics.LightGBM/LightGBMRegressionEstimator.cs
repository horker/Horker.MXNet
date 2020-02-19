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
    public class LightGBMRegressionEstimator : LightGBMEstimatorBase<double>, IDisposable
    {
        private Parameters _parameters;
        private RegressionTrainer _trainer;
        private string[] _outputCategories;

        public override DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public RegressionTrainer Trainer => _trainer;

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

        public LightGBMRegressionEstimator(string path)
            : this(new RegressionNativePredictor(LoadBooster(path)))
        { }

        public void Fit(DataMap x, DataMap y, DataMap validX, DataMap validY)
        {
            var trainDense = new DataDense()
            {
                Features = x.ToJagged<float>(),
                Labels = y.First.AsArray<float>(),
                Weights = Weights
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

        public override void Fit(DataMap x, DataMap y)
        {
            Fit(x, y, null, null);
        }

        public DataMap Predict(DataMap x, Booster.PredictType predictType)
        {
            var pred = _predicator.Booster.PredictForMatsMulti(predictType, (float[][])x, -1);
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
