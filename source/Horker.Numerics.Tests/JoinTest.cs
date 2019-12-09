using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Transformers;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class JoinTest
    {
        [Fact]
        public void TestLeftJoin()
        {
            var d1 = DataMap.FromDictionary(new OrderedDictionary()
            {
                { "key1", new int[] { 1, 2, 3, 4 } },
                { "key2", new string[] { "a", "b", "c", "d" } },
                { "x", new int[] { 10, 20, 30, 40 } },
                { "y", new string[] { "x", "y", "z" } }
            });

            var d2 = DataMap.FromDictionary(new OrderedDictionary()
            {
                { "key1", new int[] { 99, 1, 2, 4 } },
                { "key2", new string[] { "xxx", "a", "b", "d" } },
                { "x", new double[] { 100, 200, 300, 400 } },
            });

            var result = d1.LeftJoin(d2, "key1", "key2");

            Assert.Equal(new[] { "key1", "key2", "x", "y", "key1_1", "key2_1", "x_1" }, result.ColumnNames);
            Assert.Equal(new int[] { 1, 2, 3, 4 }, result["key1"].Values);
            Assert.Equal(new string[] { "a", "b", "c", "d" }, result["key2"].Values);
            Assert.Equal(new int[] { 10, 20, 30, 40 }, result["x"].Values);
            Assert.Equal(new string[] { "x", "y", "z" }, result["y"].Values);
            Assert.Equal(new double[] { 200, 300, double.NaN, 400 }, result["x_1"].Values);
        }
    }
}
