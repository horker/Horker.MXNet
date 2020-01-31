using Horker.Numerics.DataMaps;
using Horker.Numerics.Estimators;
using LightGBMNet.Train;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Horker.Numerics.LightGBM
{
    abstract public class LightGBMEstimatorBase<T> : IEstimator
    {
        public abstract DataMap Parameters { get; set; }

        public abstract void Fit(DataMap x, DataMap y);
        public abstract DataMap Predict(DataMap x);
        public abstract double Score(DataMap x, DataMap y);

        protected NativePredictorBase<T> _predicator;
        public NativePredictorBase<T> Predictor => _predicator;

        public void Save(string path)
        {
            var text = _predicator.Booster.GetModelString();
            File.WriteAllText(path, text, new UTF8Encoding(true));
        }

        protected static Booster LoadBooster(string path)
        {
            var modelString = File.ReadAllText(path, Encoding.UTF8);
            return Booster.FromString(modelString);
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
    }
}
