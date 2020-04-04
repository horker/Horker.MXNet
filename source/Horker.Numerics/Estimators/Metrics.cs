using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Analysis;

namespace Horker.Numerics.Estimators
{
    public static class Metrics
    {
        public static double Accuracy(double[][] expected, double[][] predicted)
        {
            return Accuracy(expected.ArgMax(1), predicted.ArgMax(1));
        }

        public static double Accuracy(double[,] expected, double[,] predicted)
        {
            return Accuracy(expected.ArgMax(1), predicted.ArgMax(1));
        }

        public static double Accuracy(int[] expected, int[] predicted)
        {
            return 1.0 - new AccuracyLoss(expected).Loss(predicted);
        }

        public static double BinaryCrossEntropy(double[] expected, double[] predicted)
        {
            return new BinaryCrossEntropyLoss(expected).Loss(predicted);
        }

        public static double CategoryCrossEntropy(double[][] expected, double[][] predicted)
        {
            return new CategoryCrossEntropyLoss(expected).Loss(predicted);
        }

        public static double MeanAbsoluteError(double[] expected, double[] predicted)
        {
            var e = new double[1][] { expected };
            var p = new double[1][] { predicted };
            var loss = new AbsoluteLoss(e);
            loss.Mean = true;
            return loss.Loss(p);
        }

        public static double MeanAbsoluteError(double[][] expected, double[][] predicted)
        {
            var loss = new AbsoluteLoss(expected);
            loss.Mean = true;
            return loss.Loss(predicted);
        }

        public static double MeanSquareError(double[] expected, double[] predicted)
        {
            // We need to write its own version because Accord.Math.Optimization.Losses.SquareLosss()
            // don't return a correct value when it is called with root = true.
            double error = 0.0;
            for (var i = 0; i < expected.Length; ++i)
            {
                var d = expected[i] - predicted[i];
                error += d * d;
            }
            return error / expected.Length;
        }

        public static double Auc(double[] expected, double[] predicted)
        {
            var roc = new ReceiverOperatingCharacteristic(expected, predicted);
            roc.Compute(predicted);
            return roc.Area;
        }

        public static double RootMeanSquareError(double[] expected, double[] predicted)
        {
            return Math.Sqrt(MeanSquareError(expected, predicted));
        }

        public static double RSquared(double[] expected, double[] predicted)
        {
            return new RSquaredLoss(expected.Length, expected).Loss(predicted);
        }

        public static double AdjustedRSquared(double[] expected, double[] predicted)
        {
            var loss = new RSquaredLoss(expected.Length, expected);
            loss.Adjust = true;
            return loss.Loss(predicted);
        }
    }
}
