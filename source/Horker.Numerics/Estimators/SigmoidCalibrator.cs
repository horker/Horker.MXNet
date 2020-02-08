using Accord.Math;
using Horker.Numerics.DataMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.Numerics.Estimators
{
    // ref: Accord.NET
    // https://github.com/accord-net/framework/blob/master/Sources/Accord.MachineLearning/VectorMachines/Learning/Probabilistic/ProbabilisticOutputCalibration.cs

    public class SigmoidCalibrator : IEstimator
    {
        // Parameter setting
        private int maxIterations = 100;     // Maximum number of iterations
        private double minStepSize = 1e-10;  // Minimum step taken in line search
        private double sigma = 1e-12;        // Set to any value > 0
        private double tolerance = 1e-5;

        public DataMap Parameters { get; set; }

        public SigmoidCalibrator()
        {
        }

        /// <summary>
        ///   Gets or sets the maximum number of
        ///   iterations. Default is 100. 
        /// </summary>
        /// 
        public int Iterations
        {
            get { return maxIterations; }
            set { maxIterations = value; }
        }

        /// <summary>
        ///   Gets or sets the tolerance under which the
        ///   answer must be found. Default is 1-e5.
        /// </summary>
        /// 
        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        /// <summary>
        ///   Gets or sets the minimum step size used 
        ///   during line search. Default is 1e-10.
        /// </summary>
        /// 
        public double StepSize
        {
            get { return minStepSize; }
            set { minStepSize = value; }
        }

        // Model parameters

        public double A { get; set; }
        public double B { get; set; }

        public void Learn(double[] distances, bool[] outputs)
        {
            // This method is a direct implementation of the algorithm
            // as published by Hsuan-Tien Lin, Chih-Jen Lin and Ruby C.
            // Weng, 2007. See references in documentation for more details.

            // Learning data
            var targets = new double[outputs.Length];
            int positives = 0;
            int negatives = 0;

            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i])
                    positives++;
                else
                    negatives++;
            }

            // Define the target probabilities we aim to produce
            double high = (positives + 1.0) / (positives + 2.0);
            double low = 1.0 / (negatives + 2.0);

            for (int i = 0; i < outputs.Length; i++)
                targets[i] = outputs[i] ? high : low;

            // Initialize 
            double A = 0.0;
            double B = Math.Log((negatives + 1.0) / (positives + 1.0));
            double logLikelihood = 0;
            int iterations = 0;

            // Compute the log-likelihood function
            for (int i = 0; i < distances.Length; i++)
            {
                double y = distances[i] * A + B;

                if (y >= 0)
                    logLikelihood += targets[i] * y + Special.Log1p(Math.Exp(-y));
                else
                    logLikelihood += (targets[i] - 1) * y + Special.Log1p(Math.Exp(y));
            }

            // Start main algorithm loop.
            while (iterations < maxIterations)
            {
                //                if (Token.IsCancellationRequested)
                //                    break;

                iterations++;

                // Update the Gradient and Hessian
                //  (Using that H' = H + sigma I)

                double h11 = sigma;
                double h22 = sigma;
                double h21 = 0;

                double g1 = 0;
                double g2 = 0;

                for (int i = 0; i < distances.Length; i++)
                {
                    double p, q;
                    double y = distances[i] * A + B;

                    if (y >= 0)
                    {
                        p = Math.Exp(-y) / (1.0 + Math.Exp(-y));
                        q = 1.0 / (1.0 + Math.Exp(-y));
                    }
                    else
                    {
                        p = 1.0 / (1.0 + Math.Exp(y));
                        q = Math.Exp(y) / (1.0 + Math.Exp(y));
                    }

                    double d1 = targets[i] - p;
                    double d2 = p * q;

                    // Update Hessian
                    h11 += distances[i] * distances[i] * d2;
                    h22 += d2;
                    h21 += distances[i] * d2;

                    // Update Gradient
                    g1 += distances[i] * d1;
                    g2 += d1;
                }

                // Check if the gradient is near zero as stopping criteria
                if (Math.Abs(g1) < tolerance && Math.Abs(g2) < tolerance)
                    break;

                // Compute modified Newton directions
                double det = h11 * h22 - h21 * h21;
                double dA = -(h22 * g1 - h21 * g2) / det;
                double dB = -(-h21 * g1 + h11 * g2) / det;
                double gd = g1 * dA + g2 * dB;

                double stepSize = 1;

                // Perform a line search
                while (stepSize >= minStepSize)
                {
                    double newA = A + stepSize * dA;
                    double newB = B + stepSize * dB;
                    double newLogLikelihood = 0.0;

                    // Compute the new log-likelihood function
                    for (int i = 0; i < distances.Length; i++)
                    {
                        double y = distances[i] * newA + newB;

                        if (y >= 0)
                            newLogLikelihood += (targets[i]) * y + Special.Log1p(Math.Exp(-y));
                        else
                            newLogLikelihood += (targets[i] - 1) * y + Special.Log1p(Math.Exp(y));
                    }

                    // Check if a sufficient decrease has been obtained
                    if (newLogLikelihood < logLikelihood + 1e-4 * stepSize * gd)
                    {
                        // Yes, it has. Update parameters with the new values
                        A = newA; B = newB; logLikelihood = newLogLikelihood;
                        break;
                    }
                    else
                    {
                        // Decrease the step size until it can achieve
                        // a sufficient decrease or until it fails.
                        stepSize /= 2.0;
                    }

                    if (stepSize < minStepSize)
                    {
                        // No decrease could be obtained. 
                        break; // throw new LineSearchFailedException("No sufficient decrease was obtained.");
                    }
                }
            }

            this.A = A;
            this.B = B;
        }

        public void Fit(DataMap x, DataMap y)
        {
            Learn(x.First.ToArray<double>(), y.First.AsArray<bool>());
        }

        public double Compute(double x)
        {
            return 1.0 / (1.0 + Math.Exp(A * x + B));
        }

        public DataMap Predict(DataMap x)
        {
            var result = new List<double>(x.First.Count);
            foreach (var item in x.First.AsList<double>())
                result.Add(Compute(item));

            var d = new DataMap();
            d.Add("Class0", result);
            return d;
        }

        public double Score(DataMap x, DataMap y)
        {
            throw new NotImplementedException();
        }
    }
}
