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
        public void TestSelect()
        {
            var t1 = new Series(new int[] { 1, 2, 3 });

            var t2 = t1.Select("(x, i) => x * 2");

            Assert.IsType<Series>(t2);
            Assert.Equal(new int[] { 2, 4, 6 }, t2.AsArray<int>());

            var t3 = t1.Select("(x, i) => x * i", typeof(double));

            Assert.IsType<Series>(t3);
            Assert.Equal(new double[] { 0, 2, 6 }, t3.AsArray<double>());
        }

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
    }
}
