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
            var t1 = IListExtensions.GetDataType(new int[0]);
            Assert.Equal(typeof(int), t1);

            var t2 = IListExtensions.GetDataType(new List<double>());
            Assert.Equal(typeof(double), t2);

            var t3 = IListExtensions.GetDataType(new ArrayList());
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
    }
}
