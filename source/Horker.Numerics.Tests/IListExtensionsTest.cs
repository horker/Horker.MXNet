﻿using System;
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
        public void TestGetDataType()
        {
            var t1 = GenericIListExtensions.GetDataType(new int[0]);
            Assert.Equal(typeof(int), t1);

            var t2 = GenericIListExtensions.GetDataType(new List<double>());
            Assert.Equal(typeof(double), t2);

            var t3 = GenericIListExtensions.GetDataType(new ArrayList());
            Assert.Equal(typeof(object), t3);
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
        public void TestArgMaxArgMin()
        {
            var s = new double[] { 1, 2, 3, 4, 5, 4, 3, 0, 2, 1 };

            var max = s.ArgMax();
            Assert.Equal(4, max);

            var min = s.ArgMin();
            Assert.Equal(7, min);

            s = new double[] { 1, 2, 3, 4, 5 };

            max = s.ArgMax();
            Assert.Equal(4, max);

            min = s.ArgMin();
            Assert.Equal(0, min);

            s = new double[] { 10, 9, 8, 7 };

            max = s.ArgMax();
            Assert.Equal(0, max);

            min = s.ArgMin();
            Assert.Equal(3, min);

            s = new double[] { 10 };

            max = s.ArgMax();
            Assert.Equal(0, max);

            min = s.ArgMin();
            Assert.Equal(0, min);
        }

        [Fact]
        public void TestArgSort()
        {
            var s = new float[] { 4, 5, 1, 2, 3, 9 };
            var expected = new int[] { 3, 4, 0, 1, 2, 5 };

            var sorted = s.ArgSort();

            Assert.Equal(expected, sorted);
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
        public void TestSample()
        {
            var s = new int[] { 0, 1, 2, 3 };
            var sMin = s.Min();
            var sMax = s.Max();
            var counters = new int[s.Length + 3];
            var repeat = 10000;

            for (var i = 0; i < repeat; ++i)
            {
                var t = s.Sample(true, counters.Length);
                Assert.Equal(counters.Length, t.Count);
                for (var j = 0; j < t.Count; ++j)
                {
                    Assert.True(sMin <= t[j] && t[j] <= sMax);
                    counters[j] += t[j];
                }
            }

            for (var i = 0; i < counters.Length; ++i)
                Assert.True(Math.Abs(repeat * s.Average() - counters[i]) < repeat * .05);
        }

        [Fact]
        public void TestShuffle()
        {
            var s = new int[] { 0, 1, 2, 3 };
            var counters = new int[s.Length];
            var repeat = 10000;

            for (var i = 0; i < repeat; ++i)
            {
                var t = s.Shuffle();
                Assert.Equal(s.Length, t.Count);
                Assert.Equal(s, t.SortedCopy());
                for (var j = 0; j < t.Count; ++j)
                    counters[j] += t[j];
            }

            for (var i = 0; i < counters.Length; ++i)
                Assert.True(Math.Abs(repeat * s.Average() - counters[i]) < repeat * .05);
        }

        [Fact]
        public void TestShuffleFill()
        {
            var s = new int[] { 0, 1, 2, 3 };
            var t = new int[] { 0, 1, 2, 3 };
            var counters = new int[s.Length];
            var repeat = 10000;

            for (var i = 0; i < repeat; ++i)
            {
                t.ShuffleFill();
                Assert.Equal(s, t.SortedCopy());
                for (var j = 0; j < t.Length; ++j)
                    counters[j] += t[j];
            }

            for (var i = 0; i < counters.Length; ++i)
                Assert.True(Math.Abs(repeat * s.Average() - counters[i]) < repeat * .05);
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

        [Fact]
        public void TestUnique()
        {
            var s = new int[] { 1, 2, 3, 1, 2 };

            var s1 = s.Unique();

            Assert.Equal(new int[] { 1, 2, 3 }, s1);
        }

        [Fact]
        public void TestComparer()
        {
            var s = new float[] { 1, 2, 3, 4, 5 };

            var s1 = s.Le(3.0f);

            Assert.Equal(new[] { true, true, true, false, false }, s1);
        }

        [Fact]
        public void TestIsNaN()
        {
            var s1 = new float[] { 1, 2, float.NaN, 4, 5 };
            var s2 = new double[] { double.NaN, 2, 3, 4, double.NaN };
            var s3 = new int[] { -1, 0, 1, 2, 3 };
            var s4 = new[] { "a", null, "b", "c", "d" };

            var t1 = s1.IsNaN();
            var t2 = s2.IsNaN();
            var t3 = s3.IsNaN();
            var t4 = s4.IsNaN();

            Assert.Equal(new[] { false, false, true, false, false }, t1);
            Assert.Equal(new[] { true, false, false, false, true }, t2);
            Assert.Equal(new[] { false, false, false, false, false }, t3);
            Assert.Equal(new[] { false, true, false, false, false }, t4);
        }

        [Fact]
        public void TestSortBy()
        {
            var s = new [] { 1, 2, 3, 4 };
            var by = new [] { 4, 3, 1, 2 };

            var t = GenericIListExtensions.SortBy(s, by);

            Assert.Equal(new[] { 3, 4, 2, 1 }, t);

            GenericIListExtensions.SortByFill(s, by);

            Assert.Equal(new[] { 3, 4, 2, 1 }, s);
        }
    }
} 