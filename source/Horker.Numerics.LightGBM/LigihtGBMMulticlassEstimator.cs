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
    public abstract class LightGBMCategoricalEstimator<E, T> : IEstimator, IDisposable
        where E: TrainerBase<T>
    {
        private Parameters _parameters;
        private TrainerBase<T> _trainer;
        private NativePredictorBase<T> _predicator;
        private string[] _outputCategories;

        public DataMap Parameters { get => null; set => throw new NotImplementedException(); }

        public E Trainer => (E)_trainer;
        public NativePredictorBase<T> Predictor => _predicator;

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
                _predicator = (NativePredictorBase<T>)predicators.Native;
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
            result.Add("Names", featureNames);
            result.Add("Importance", imp);

            return result;
        }

        public abstract double Score(DataMap x, DataMap y);

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
            : base(new BinaryNativePredictor(Booster.FromFile(path)))
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
            : base(new MulticlassNativePredictor(Booster.FromFile(path)))
        { }

        public override double Score(DataMap x, DataMap y)
        {
            var predicted = Predict(x).ToJagged<double>();
            var expected = y.ToJagged<double>();
            return 1.0 - Metrics.Accuracy(expected, predicted);
        }
    }
}
