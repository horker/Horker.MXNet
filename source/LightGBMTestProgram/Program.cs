using LightGBMNet.Train;
using System;
using System.Diagnostics;

namespace LightGBMTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var data = new float[][] {
                new float[] { 1f, 2f, 3f },
                new float[] { 4f, 5f, 6f }
            };

            var dataSet = new Dataset(data, 3, new CommonParameters(), new DatasetParameters());
            */

            var confFile = @"D:\data\lightgbm\LightGBM\examples\binary_classification\train.conf";
            var booster = Booster.FromFile(confFile);
        }
    }
}
