using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using Horker.Numerics.DataMaps;

namespace Horker.Numerics.Estimators
{
    public class LinearRegressionEstimator : IEstimator
    {
        public MultipleLinearRegressionAnalysis Model { get; set; }
        public bool UseIntercept { get; set; }
        public bool IsRobust { get; set; }

        public DataMap Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public LinearRegressionEstimator()
        {
            UseIntercept = true;
            IsRobust = false;
        }

        public void Fit(DataMap x, DataMap y)
        {
            Model = new MultipleLinearRegressionAnalysis();
            Model.OrdinaryLeastSquares.UseIntercept = UseIntercept;
            Model.OrdinaryLeastSquares.IsRobust = IsRobust;

            Model.Learn(x.ToJagged<double>(), y.First.ToArray<double>());

            Model.Inputs = x.ColumnNames.ToArray();
        }

        public DataMap Predict(DataMap x)
        {
            var predict = Model.Transform(x.ToJagged<double>());

            var result = new DataMap();
            result.Add("predict", predict);

            return result;
        }

        public double Score(DataMap x, DataMap y)
        {
            var predicted = Predict(x).First.ToArray<double>();
            var expected = y.First.ToArray<double>();
            return Metrics.AdjustedRSquared(expected, predicted);
        }
    }
}
