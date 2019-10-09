using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.DataMaps.Extensions;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class GroupByTest
    {
        [Fact]
        public void TestGroupBy()
        {
            var dm = DataMap.FromDictionary(new Hashtable()
            {
                { "cat", new int[] { 1, 2, 1, 1, 3 } },
                { "value1", new string[] { "a", "b", "c", "d", "e" } },
                { "value2", new double[] { 10, 20, 30, 40, 50 } },
                { "value3", new double[] { 1, 2, 3, 4, 5 } }
            });

            var g = new GroupBy(dm, new string[] { "cat" }, new string[] { "value1", "value3" });

            var s1 = g.GetSubset(1);

            Assert.Equal(3, s1.MaxRowCount);
            Assert.Equal(3, s1.MinRowCount);
            Assert.Equal(new string[] { "value1", "value3" }, s1.ColumnNames);

            Assert.Equal(new string[] { "a", "c", "d" }, s1["value1"]);
            Assert.Equal(new double[] { 1, 3, 4 }, s1["value3"]);
        }

        [Fact]
        public void TestGroupByUpdate()
        {
            var dm = DataMap.FromDictionary(new Hashtable()
            {
                { "cat", new int[] { 1, 2, 1, 1, 3 } },
                { "value1", new string[] { "a", "b", "c", "d", "e" } },
                { "value2", new double[] { 10, 20, 30, 40, 50 } },
                { "value3", new double[] { 1, 2, 3, 4, 5 } }
            });

            var g = new GroupBy(dm, new string[] { "cat" }, new string[] { "value1", "value3" });

            var s1 = g.GetSubset(1);

            s1["value3"].ApplyFill<double>((x, i) => x * 100);

            Assert.Equal(new double[] { 100, 2, 300, 400, 5 }, dm["value3"]);
        }
    }
} 