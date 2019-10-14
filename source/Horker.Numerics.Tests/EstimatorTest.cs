using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Accord.Statistics.Analysis;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Estimators;
using Horker.Numerics.Transformers;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class EstimatorTest
    {
        [Fact]
        public void TestLinearRegressionTest()
        {
            var inputs = new double[][] {
                // age, smokes?
                new double[] { 55,    0  },
                new double[] { 28,    0  },
                new double[] { 65,    1  },
                new double[] { 46,    0  },
                new double[] { 86,    1  },
                new double[] { 56,    1  },
                new double[] { 85,    0  },
                new double[] { 33,    0  },
                new double[] { 21,    1  },
                new double[] { 42,    1  },
            };
            
            var output = new double[] {
                // Whether each patient had lung cancer or not
                0, 0, 0, 1, 1, 1, 0, 0, 0, 1
            };

            // Accord.NET

            var analysis = new MultipleLinearRegressionAnalysis();
            analysis.OrdinaryLeastSquares.UseIntercept = true;
            analysis.OrdinaryLeastSquares.IsRobust = false;
            analysis.Learn(inputs, output);

            // Estimator

            var x = DataMap.FromJagged(inputs, new string[] { "age", "smokes" });
            var y = new DataMap();
            y.Add("lung_cancer", output);

            var est = new LinearRegressionEstimator();
            est.Fit(x, y);

            var model = est.Model;

            Assert.Equal(analysis.Coefficients.Count, model.Coefficients.Count);
            Assert.Equal(analysis.Coefficients[0].Value, model.Coefficients[0].Value);
            Assert.Equal(analysis.Coefficients[1].Value, model.Coefficients[1].Value);
            Assert.Equal(analysis.Coefficients[2].Value, model.Coefficients[2].Value);
        }

        [Fact]
        public void TestLogisticRegressionTest()
        {
            var inputs = new double[][] {
                // age, smokes?
                new double[] { 55,    0  },
                new double[] { 28,    0  },
                new double[] { 65,    1  },
                new double[] { 46,    0  },
                new double[] { 86,    1  },
                new double[] { 56,    1  },
                new double[] { 85,    0  },
                new double[] { 33,    0  },
                new double[] { 21,    1  },
                new double[] { 42,    1  },
            };
            
            var outputs = new double[][] {
                // Whether each patient had lung cancer or not
                new double[] { 1, 0 },
                new double[] { 1, 0 },
                new double[] { 1, 0 },
                new double[] { 0, 1 },
                new double[] { 0, 1 },
                new double[] { 0, 1 },
                new double[] { 1, 0 },
                new double[] { 1, 0 },
                new double[] { 1, 0 },
                new double[] { 0, 1 }
            };

            // Accord.NET

            var analysis = new MultinomialLogisticRegressionAnalysis();
            analysis.Learn(inputs, outputs);
            var expected = analysis.Regression.Probabilities(inputs);

            // Estimator

            var x = DataMap.FromJagged(inputs, new string[] { "age", "smokes" });
            var y = DataMap.FromJagged(outputs, new string[] { "no_cancer", "cancer" });

            var est = new LogisticRegressionEstimator();
            est.Fit(x, y);

            var model = est.Model;

            Assert.Equal(analysis.Coefficients.Count, model.Coefficients.Count);
            Assert.Equal(analysis.Coefficients[0].Value, model.Coefficients[0].Value);
            Assert.Equal(analysis.Coefficients[1].Value, model.Coefficients[1].Value);
            Assert.Equal(analysis.Coefficients[2].Value, model.Coefficients[2].Value);

            var predicted = est.Predict(x).ToJagged<double>();

            Assert.Equal(expected[0], predicted[0]);
            Assert.Equal(expected[1], predicted[1]);
            Assert.Equal(expected[5], predicted[5]);
            Assert.Equal(expected[9], predicted[9]);
        }
    }
}
