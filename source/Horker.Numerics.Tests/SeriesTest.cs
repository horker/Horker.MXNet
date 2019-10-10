using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class SeriesTest
    {
        [Fact]
        public void TestCumulativeSum()
        {
            var t1 = new Series(new decimal[] { 1, 2, 3 });

            var t2 = t1.CumulativeSum();

            Assert.IsType<Series>(t2);
            Assert.Equal(new decimal[] { 1, 3, 6 }, t2.AsArray<decimal>());

            t1.CumulativeSumFill();

            Assert.Equal(new decimal[] { 1, 3, 6 }, t1.AsArray<decimal>());

            var t3 = new Series(new string[] { "a", "b", "c" });

            Assert.Throws<InvalidOperationException>(() => {
                t3.CumulativeSum();
            });
        }

        [Fact]
        public void TestFillNaN()
        {
            var t1 = new Series(new double[] { 1, double.NaN, 2, double.NaN });

            var t2 = t1.FillNaN(99);

            Assert.IsType<Series>(t2);
            Assert.Equal(new double[] { 1, 99, 2, 99 }, t2.AsArray<double>());

            t1.FillNaNFill(999);

            Assert.Equal(new double[] { 1, 999, 2, 999}, t1.AsArray<double>());
        }

        [Fact]
        public void TestApply()
        {
            var t1 = new Series(new double[] { 1, 2, 3, 4 });
            var t2 = t1.Apply("(x, i) => (double)series[series.Count - 1 - i]");

            Assert.Equal(new double[] { 4, 3, 2, 1 }, t2);
        }

        [Fact]
        public void TestApplyFill()
        {
            var t1 = new Series(new double[] { 1, 2, 3, 4 });

            t1.ApplyFill("(x, i) => x * i + 1");
            Assert.Equal(new double[] { 1, 3, 7, 13 }, t1.AsArray());
        }

        public static int Sum = 0;

        [Fact]
        public void TestReduce()
        {
            var t1 = new Series(new double[] { 1, 2, 3, 4 });

            var sum = t1.Reduce("(x, i, sum) => sum + x", 0);
            Assert.Equal(10.0, sum);
        }

        [Fact]
        public void TestCountIf()
        {
            var t1 = new Series(new double[] { 1, 2, 3, 4 });

            var count = t1.CountIf("(x, i) => x >= 2");
            Assert.Equal(3, count);
        }

        [Fact]
        public void TestRollingApply()
        {
            var t1 = new Series(new double[] { 1, 2, 3, 4, 5 });

            var t2 = t1.RollingApply("(values, i) => values[values.Length - 1] ", 3);
            Assert.Equal(new double[] { 1, 2, 3, 4, 5 }, t2);

            var t3 = t1.RollingApply("(values, i) => values.Average()", 3);
            Assert.Equal(new double[] { 1, 1.5, 2, 3, 4 }, t3);
        }

        [Fact]
        public void TestCountNaN()
        {
            var t1 = new Series(new string[] { "a", " ", "", null, "xxx" });

            var c = t1.CountNaN();

            Assert.Equal(3, c);
        }
    }
}
