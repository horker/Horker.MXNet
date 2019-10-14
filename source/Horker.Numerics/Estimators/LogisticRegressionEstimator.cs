using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Analysis;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.Estimators
{
    public class LogisticRegressionEstimator : IEstimator
    {
        public MultinomialLogisticRegressionAnalysis Model { get; set; }

        public DataMap Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public LogisticRegressionEstimator()
        {
            Model = new MultinomialLogisticRegressionAnalysis();
        }

        public void Fit(DataMap x, DataMap y)
        {
            Model.Learn(x.ToJagged<double>(), y.ToJagged<double>());

            Model.InputNames = x.ColumnNames.ToArray();
            Model.OutputNames = y.ColumnNames.ToArray();
        }

        public DataMap Predict(DataMap x)
        {
            var predicted = Model.Regression.Probabilities(x.ToJagged<double>());
            return DataMap.FromJagged(predicted, Model.OutputNames);
        }

        public double Score(DataMap x, DataMap y)
        {
            var predicted = Predict(x).ToJagged<double>();
            var expected = y.ToJagged<double>();
            return 1.0 - Metrics.Accuracy(expected, predicted);
        }
    }
}
