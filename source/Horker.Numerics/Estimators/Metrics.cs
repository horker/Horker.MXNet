using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Math.Optimization.Losses;

namespace Horker.Numerics.Estimators
{
    public static class Metrics
    {
        public static double Accuracy(double[][] expected, double[][] predicted)
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
            return new SquareLoss(expected).Loss(predicted);
        }

        public static double RootMeanSquareError(double[] expected, double[] predicted)
        {
            var loss = new SquareLoss(expected);
            loss.Root = true;
            return loss.Loss(predicted);
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
