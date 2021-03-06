﻿using System;
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

            var trans = new OneHotTransformer<int>(OneHotType.OneHot, "test_{0}");

            trans.Fit(t1);
            var r = trans.TransformToDataMap(t2);

            Assert.Equal(new[] { "test_xxx", "test_yyy", "test_zzz" }, r.ColumnNames);

            Assert.Equal(new int[] { 0, 0, 1, 0 }, r["test_xxx"].UnderlyingList);
            Assert.Equal(new int[] { 0, 1, 0, 0 }, r["test_yyy"].UnderlyingList);
            Assert.Equal(new int[] { 1, 0, 0, 0 }, r["test_zzz"].UnderlyingList);
        }

        [Fact]
        public void TestDummyEncoding()
        {
            var t1 = new double[] { 10, 11, 11, 10, 12, 13, 13 };
            var t2 = new double[] { 13, 13, 11, 12, 999 };

            var trans = new LabelEncodingTransformer<double>(true, -100);

            trans.Fit(t1);
            var r = trans.Transform(t2);

            Assert.Equal(new double[] { 3, 3, 1, 2, -100 }, r.UnderlyingList);
        }
    }
}
