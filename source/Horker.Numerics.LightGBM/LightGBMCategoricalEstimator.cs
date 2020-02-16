using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Estimators;
using Horker.Numerics.Transformers;
using Horker.Numerics.Utilities;
using LightGBMNet.Train;

namespace Horker.Numerics.LightGBM
{
    public abstract class LightGBMCategoricalEstimator<E, T> : LightGBMEstimatorBase<T>, IDisposable
        where E: TrainerBase<T>
    {
        private Parameters _parameters;
        private TrainerBase<T> _trainer;
        private string[] _outputCategories;

        public override DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public E Trainer => (E)_trainer;

        public string[] OutputCategories
        {
            get
            {
                if (_outputCategories == null)
                {
                    var classCount = _predicator.Booster.NumClasses;
                    _outputCategories = new string[classCount];
                    for (var i = 0; i < _outputCategories.Length; ++i)
                        _outputCategories[i] = "Class" + i;
                }

                return _outputCategories;
            }

            set
            {
                _outputCategories = value;
            }
        }

        public LightGBMCategoricalEstimator(Parameters parameters, E trainer)
        {
            if (parameters == null)
                parameters = new Parameters();

            _parameters = parameters;
            _trainer = trainer;
        }

        public LightGBMCategoricalEstimator(NativePredictorBase<T> predictor)
        {
            _predicator = predictor;
        }

        public Tuple<float[][], float[], float[][], float[]> GetDataAsArray(DataMap x, DataMap y, DataMap validX, DataMap validY)
        {
            var xArray = x.ToJagged<float>();
            var yArray = y.First.AsArray<float>();
            var validXArray = validX.ToJagged<float>();
            var validYArray = validY.First.AsArray<float>();

            return Tuple.Create(xArray, yArray, validXArray, validYArray);
        }

        public void Fit(DataMap x, DataMap y, DataMap validX, DataMap validY)
        {
            var (xArray, yArray, validXArray, validYArray) = GetDataAsArray(x, y, validX, validY);
            Fit(xArray, yArray, validXArray, validYArray, x.ColumnNames.ToArray(), validX.ColumnNames.ToArray());
        }

        public void Fit(float[][] x, float[] y, float[][] validX = null, float[] validY = null, string[] columnNames = null, string[] validColumnNames = null)
        {
            var trainDense = new DataDense()
            {
                Features = x,
                Labels = y
            };

            DataDense validDense = null;
            if (validX != null)
            {
                validDense = new DataDense()
                {
                    Features = validX,
                    Labels = validY
                };
            }

            using (var datasets = new Datasets(_parameters.Common, _parameters.Dataset, trainDense, validDense))
            {
                if (columnNames != null)
                    datasets.Training.SetFeatureNames(columnNames);

                if (validX != null && validColumnNames != null)
                    datasets.Validation.SetFeatureNames(validColumnNames);

                var predicators = _trainer.Train(datasets);
                _predicator = (NativePredictorBase<T>)predicators.Native;
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

        public void Dispose()
        {
            if (_trainer != null)
                _trainer.Dispose();

            if (_predicator != null)
                _predicator.Dispose();
        }
    }

    public class LightGBMBinaryEstimator : LightGBMCategoricalEstimator<BinaryTrainer, double>
    {
        public LightGBMBinaryEstimator(Parameters parameters)
            : base(parameters, new BinaryTrainer(parameters.Learning, parameters.Objective))
        { }

        public LightGBMBinaryEstimator(NativePredictorBase<double> predictor)
            : base(predictor)
        { }

        public LightGBMBinaryEstimator(string path)
            : base(new BinaryNativePredictor(LoadBooster(path)))
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

        public LightGBMMulticlassEstimator(NativePredictorBase<double[]> predictor)
            : base(predictor)
        { }

        public LightGBMMulticlassEstimator(string path)
            : base(new MulticlassNativePredictor(LoadBooster(path)))
        { }

        public override double Score(DataMap x, DataMap y)
        {
            var predicted = Predict(x).ToJagged<double>();
            var expected = y.ToJagged<double>();
            return 1.0 - Metrics.Accuracy(expected, predicted);
        }
    }
}
