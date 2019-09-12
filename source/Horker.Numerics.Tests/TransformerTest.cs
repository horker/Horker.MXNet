using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Transformers;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class TransformerTest
    {
        [Fact]
        public void TestOneHost()
        {
            var t1 = new string[] { "xxx", "yyy", "zzz" };
            var t2 = new string[] { "zzz", "yyy", "xxx", "aaa" };

            var trans = new OneHotSeriesTransformer(OneHotType.OneHot, "test_{0}");

            trans.Fit(t1);
            var r = trans.TransformToDataMap(t2);

            Assert.Equal(new[] { "test_xxx", "test_yyy", "test_zzz" }, r.ColumnNames);

            Assert.Equal(new int[] { 0, 0, 1, 0 }, r["test_xxx"]);
            Assert.Equal(new int[] { 0, 1, 0, 0 }, r["test_yyy"]);
            Assert.Equal(new int[] { 1, 0, 0, 0 }, r["test_zzz"]);
        }
    }
}
