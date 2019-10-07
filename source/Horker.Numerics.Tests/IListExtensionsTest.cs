using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.DataMaps.Extensions;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class IListExtensionsTest
    {
        [Fact]
        public void TestGetDataTpe()
        {
            var t1 = GenericIListExtensions.GetDataType(new int[0]);
            Assert.Equal(typeof(int), t1);

            var t2 = GenericIListExtensions.GetDataType(new List<double>());
            Assert.Equal(typeof(double), t2);

            var t3 = GenericIListExtensions.GetDataType(new ArrayList());
            Assert.Equal(typeof(object), t3);
        }

        [Fact]
        public void TestApply()
        {
            var s = new int[] { 1, 2, 3 };

            var t1 = s.ApplyFuncString<int, int>("(x, i) => x * i + 1");
            Assert.Equal(new int[] { 1, 3, 7 }, t1);
        }

        [Fact]
        public void TestApplyFill()
        {
            var s = new int[] { 1, 2, 3 };

            s.ApplyFillFuncString("(x, i) => x * i + 1");
            Assert.Equal(new int[] { 1, 3, 7 }, s);
        }

        public static int Sum = 0;

        [Fact]
        public void TestReduce()
        {
            var s = new int[] { 1, 2, 3 };

            var sum = s.ReduceFuncString("(x, i, sum) => sum + x", 0);
            Assert.Equal(6, sum);
        }

        [Fact]
        public void TestCountIf()
        {
            var s = new int[] { 1, 2, 3 };

            var count = s.CountIfFuncString("(x, i) => x >= 2");
            Assert.Equal(2, count);
        }

        [Fact]
        public void TestRollingApply()
        {
            var s = new double[] { 1, 2, 3, 4, 5 };

            var t1 = s.RollingApplyFuncString<double, double>("(values, i) => values[values.Length - 1] ", 3);
            Assert.Equal(new double[] { 1, 2, 3, 4, 5 }, t1);

            var t2 = s.RollingApplyFuncString<double, double>("(values, i) => values.Average()", 3);
            Assert.Equal(new double[] { 1, 1.5, 2, 3, 4 }, t2);
        }

        [Fact]
        public void TestComparisons()
        {
            var s = new double[] { 1, 2, 3, 4, 5 };

            var t1 = s.Eq(new double[] { 1, 1, 3, 3, 3 });
            Assert.Equal(new bool[] { true, false, true, false, false }, t1);

            var t2 = s.Le(3.0);
            Assert.Equal(new bool[] { true, true, true, false, false }, t2);
        }

        [Fact]
        public void TestCorrelation()
        {
            var s1 = new double[] { 1, 2, 3, 4, 5 };
            var s2 = new double[] { 1, 4, 5, 4, 5 };

            var cor = s1.Correlation(s2);

            var s = new double[,] {
                { 1, 1 },
                { 2, 4 },
                { 3, 5 },
                { 4, 4 },
                { 5, 5 }
            };
            var expected = Accord.Statistics.Measures.Correlation(s);

            Assert.Equal(expected[0,1], cor, 10);
        }

        [Fact]
        public void TestQuantile()
        {
            var s = new double[] { 1, 2, 3, 4, 5, 4, 3, 2, 2, 1 };

            var q = s.Quantile(.314);

            var expected = Accord.Statistics.Measures.Quantile(s, .314);

            Assert.Equal(expected, q);
        }

        [Fact]
        public void TestSkipNaN()
        {
            var s = new double[] { double.NaN, 1, 2, double.NaN, 3, 4, 5, double.NaN };

            var mean = s.Mean();

            var expectedMean = Accord.Statistics.Measures.Mean(new double[] { 1, 2, 3, 4, 5 });

            Assert.Equal(expectedMean, mean);

            var variance = s.Variance();

            var expectedVar = Accord.Statistics.Measures.Variance(new double[] { 1, 2, 3, 4, 5 });

            Assert.Equal(expectedVar, variance);

            var s1 = new double[] { 1, 2, double.NaN, 3, 4,        999, 5,        999 };
            var s2 = new double[] { 1, 4,         99, 5, 4, double.NaN, 5, double.NaN };

            var cor = s1.Correlation(s2);

            var ss = new double[,] {
                { 1, 1 },
                { 2, 4 },
                { 3, 5 },
                { 4, 4 },
                { 5, 5 }
            };
            var expectedCor = Accord.Statistics.Measures.Correlation(ss);

            Assert.Equal(expectedCor[0,1], cor, 10);
        }
    }
} 