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
                { "value3", new double[] { 1, 2, 3, 4, 5 } },
                { "value4", new int[] { 99, 999 } }
            });

            var g = new GroupBy(dm, new string[] { "cat" }, new string[] { "value1", "value3", "value4" });

            var s1 = g.GetSubset(1);

            Assert.Equal(3, s1.MaxRowCount);
            Assert.Equal(1, s1.MinRowCount);
            Assert.Equal(new string[] { "value1", "value3", "value4" }, s1.ColumnNames);

            Assert.Equal(new string[] { "a", "c", "d" }, s1["value1"].UnderlyingList);
            Assert.Equal(new double[] { 1, 3, 4 }, s1["value3"].UnderlyingList);
            Assert.Equal(new int[] { 99 }, s1["value4"].UnderlyingList);

            var s2 = g.GetSubset(3);
            Assert.Equal(new string[] { "value1", "value3", "value4" }, s2.ColumnNames);

            Assert.Equal(1, s2.MaxRowCount);
            Assert.Equal(0, s2.MinRowCount);

            Assert.Equal(new string[] { "e" }, s2["value1"].UnderlyingList);
            Assert.Equal(new double[] { 5 }, s2["value3"].UnderlyingList);
            Assert.Empty(s2["value4"].UnderlyingList);
        }

        [Fact]
        public void TestMultipleColumns()
        {
            var dm = DataMap.FromDictionary(new Hashtable()
            {
                { "cat1", new int[] { 1, 2, 1, 1, 3 } },
                { "cat2", new string[] { "a", "b", "c", "a", "a" } },
                { "value1", new double[] { 1, 2, 3, 4, 5 } }
            });

            var g = new GroupBy(dm, new string[] { "cat1", "cat2" });

            var s1 = g.GetSubset(1, "a");

            Assert.Equal(new double[] { 1, 4 }, s1["value1"].UnderlyingList);
        }


        [Fact]
        public void TestUpdateSubset()
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

            Assert.Equal(new double[] { 100, 2, 300, 400, 5 }, dm["value3"].UnderlyingList);
        }

        [Fact]
        public void TestMethodCache()
        {
            var dm = DataMap.FromDictionary(new Hashtable()
            {
                { "cat", new int[] { 1, 2, 1, 1, 3 } },
                { "value1", new string[] { "a", "b", "c", "d", "e" } },
                { "value2", new double[] { 10, 20, 30, 40, 50 } },
                { "value3", new double[] { 1, 2, 3, 4, 5 } }
            });

            var result = new List<int>();
            foreach (var g in dm.GroupBy(new string[] { "cat" }))
            {
                var count = g["value2"].CountIf("(x, i) => x <= 30");
                result.Add(count);
            }

            Assert.Equal(new int[] { 2, 1, 0 }, result);
        }

        [Fact]
        public void TestSummarizePerGroup()
        {
            var dm = DataMap.FromDictionary(new Hashtable()
            {
                { "cat", new int[] { 1, 2, 1, 1, 3 } },
                { "value1", new string[] { "a", "b", "c", "d", "e" } },
                { "value2", new double[] { 10, 20, 30, 40, 50 } },
                { "value3", new double[] { 1, 2, 3, 4, 5 } },
            });

            var g = new GroupBy(dm, new string[] { "cat" }, new string[] { "cat", "value1", "value2", "value3" });

            var s = g.Summarize(new Dictionary<string, string>() {
                { "max_value2", "g => g[\"value2\"].Max()" },
                { "min_value3", "g => g[\"value3\"].Min()" }
            });

            Assert.Equal(new[] { "cat", "max_value2", "min_value3" }, s.ColumnNames.ToArray());
            Assert.Equal(new int[] { 1, 2, 3 }, s["cat"].ToArray<int>());
            Assert.Equal(new double[] { 40, 20, 50 }, s["max_value2"].ToArray<double>());
            Assert.Equal(new double[] { 1, 2, 5 }, s["min_value3"].ToArray<double>());
        }

        [Fact]
        public void TestSummarizePerColumn()
        {
            var dm = DataMap.FromDictionary(new Hashtable()
            {
                { "cat", new int[] { 1, 2, 1, 1, 3 } },
                { "value1", new string[] { "a", "b", "c", "d", "e" } },
                { "value2", new double[] { 10, 20, 30, 40, 50 } },
                { "value3", new double[] { 1, 2, 3, 4, 5 } },
            });

            var g = new GroupBy(dm, new string[] { "cat" }, new string[] { "cat", "value1", "value2", "value3" });

            var s = g.Summarize(new[] { "value2", "value3" }, new Dictionary<string, string>() {
                { "max", "Max" },
                { "min", "c => c.Min()" }
            });

            Assert.Equal(new[] { "cat", "max/value2", "min/value2", "max/value3", "min/value3" }, s.ColumnNames.ToArray());
            Assert.Equal(new int[] { 1, 2, 3 }, s["cat"].ToArray<int>());
            Assert.Equal(new double[] { 40, 20, 50 }, s["max/value2"].ToArray<double>());
            Assert.Equal(new double[] { 1, 2, 5 }, s["min/value3"].ToArray<double>());
        }
    }
} 