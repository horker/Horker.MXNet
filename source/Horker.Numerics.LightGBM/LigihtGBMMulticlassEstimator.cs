using System;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Estimators;
using Horker.Numerics.Transformers;
using Horker.Numerics.Utilities;
using LightGBMNet.Train;

namespace Horker.Numerics.LightGBM
{
    public abstract class LightGBMCategoricalEstimator<E, T> : IEstimator, IDisposable
        where E: TrainerBase<T>
    {
        private Parameters _parameters;
        private TrainerBase<T> _trainer;
        private Predictors<T> _predicators;
        private string[] _categories;

        public DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public E Trainer => (E)_trainer;
        public Predictors<T> Predictors => _predicators;
        public string[] Categories => _categories;

        public LightGBMCategoricalEstimator(Parameters parameters, E trainer)
        {
            if (parameters == null)
                parameters = new Parameters();

            _parameters = parameters;
            _trainer = trainer;
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

        public abstract double Score(DataMap x, DataMap y);

        public void Dispose()
        {
            _trainer.Dispose();
            _predicators.Dispose();
        }
    }

    public class LightGBMBinaryEstimator : LightGBMCategoricalEstimator<BinaryTrainer, double>
    {
        public LightGBMBinaryEstimator(Parameters parameters)
            : base(parameters, new BinaryTrainer(parameters.Learning, parameters.Objective))
        { }

        public override double Score(DataMap x, DataMap y)
        {
            var predicted = Predict(x).First.ToArray<int>();
            var expected = y.First.ToArray<int>();
            return 1.0 - Metrics.Accuracy(expected, predicted);
        }
    }

    public class LightGBMMulticlassEstimator : LightGBMCategoricalEstimator<MulticlassTrainer, double[]>
    {
        public LightGBMMulticlassEstimator(Parameters parameters)
            : base(parameters, new MulticlassTrainer(parameters.Learning, parameters.Objective))
        { }

        public override double Score(DataMap x, DataMap y)
        {
            var predicted = Predict(x).ToJagged<double>();
            var expected = y.ToJagged<double>();
            return 1.0 - Metrics.Accuracy(expected, predicted);
        }
    }
}
