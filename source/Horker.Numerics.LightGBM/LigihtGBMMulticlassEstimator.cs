using System;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Estimators;
using Horker.Numerics.Transformers;
using Horker.Numerics.Utilities;
using LightGBMNet.Train;

namespace Horker.Numerics.LightGBM
{
    public sealed class LightGBMMulticlassEstimator : IEstimator, IDisposable
    {
        private Parameters _parameters;
        private MulticlassTrainer _trainer;
        private Predictors<double[]> _predicators;
        private string[] _categories;

        public DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public MulticlassTrainer Trainer => _trainer;
        public Predictors<double[]> Predictors => _predicators;
        public string[] Categories => _categories;

        public LightGBMMulticlassEstimator()
            : this(null)
        {
        }

        public LightGBMMulticlassEstimator(Parameters parameters)
        {
            if (parameters == null)
                parameters = new Parameters();

            _parameters = parameters;
            _trainer = new MulticlassTrainer(_parameters.Learning, _parameters.Objective);
        }

        public void Fit(DataMap x, DataMap y)
        {
            SeriesBase labels;
            var dataType = y.First.DataType;
            if (Utils.IsNumeric(dataType))
            {
                labels = y.First;
            }
            else
            {
                var trans = new LabelEncodingTransformer<float>();
                labels = trans.FitTransform(y.First);
                _categories = trans.Categories.Cast<string>().ToArray();
            }

            var dense = new DataDense()
            {
                Features = x.ToJagged<float>(),
                Labels = labels.AsArray<float>()
            };

            using (var datasets = new Datasets(_parameters.Common, _parameters.Dataset, dense, null))
            {
                _predicators = _trainer.Train(datasets);
            }
        }

        public DataMap Predict(DataMap x)
        {
            var pred = _trainer.Evaluate(Booster.PredictType.Normal, (float[][])x, -1);
            return DataMap.From2DArray(pred, _categories);
        }

        public double Score(DataMap x, DataMap y)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _trainer.Dispose();
            _predicators.Dispose();
        }
    }
}
