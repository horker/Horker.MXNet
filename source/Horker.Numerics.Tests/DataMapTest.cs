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
    public class DataMapTest
    {
        [Fact]
        public void TestCreate()
        {
            var t1 = new int[] { 1, 2, 3, 4, 5, 6 };
            var t2 = new string[] { "a", "b", "c" };

            var d = new DataMap();

            d.Add("int", t1);
            d.Add("string", t2);

            Assert.Equal(2, d.ColumnCount);
            Assert.Equal(6, d.RowCount);

            Assert.Equal(t1, d["int"].UnderlyingList);
            Assert.Equal(t2, d["string"].UnderlyingList);

            Assert.Equal(t1, d.GetAs<int>("int"));
            Assert.Equal(t2, d.GetAs<string>("string"));
        }

        [Fact]
        public void TestFrom2DArray()
        {
            var data = new double[5, 3]
            {
                { 0, 1, 2 },
                { 10, 11, 12 },
                { 20, 21, 22 },
                { 30, 31, 32 },
                { 40, 41, 42 }
            };

            var d = DataMap.From2DArray(data);

            Assert.Equal(new[] { "Column0", "Column1", "Column2" }, d.ColumnNames);

            Assert.Equal(new double[] { 1, 11, 21, 31, 41 }, d["Column1"].Values);
        }

        [Fact]
        public void TestSelectColumns()
        {
            var d = DataMap.FromDictionary(new Dictionary<string, IList>()
            {
                { "foo", new float[]{ 1,2,3,4,5 } },
                { "bar", new string[]{ "a", "b", "c", "d", "e" } },
                { "baz", Enumerable.Range(0, 5).ToArray() }
            });

            d.SetOrder(new string[] { "foo", "bar", "baz" });

            DataMap d2 = d.SelectColumns("bar", "baz");

            Assert.Equal(new string[] { "bar", "baz" }, d2.ColumnNames.ToArray());
        }

        [Fact]
        public void TestFilter()
        {
            var d = DataMap.FromDictionary(new Dictionary<string, IList>()
            {
                { "foo", new float[]{ 1,2,3,4,5 } },
                { "bar", new string[]{ "a", "b", "c", "d", "e" } },
            });

            d.SetOrder(new string[] { "foo", "bar" });

            var result = d.Filter(d["foo"].Apply("(x, i) => i % 2 == 0", typeof(bool)));

            Assert.Equal(3, result.RowCount);
            Assert.Equal(new string[] { "foo", "bar" }, result.ColumnNames.ToArray());

            var c1 = result.GetAs<float>("foo");
            Assert.Equal(new float[] { 1, 3, 5 }, c1);

            var c2 = result.GetAs<string>("bar");
            Assert.Equal(new string[] { "a", "c", "e" }, c2);
        }

        [Fact]
        public void TestTopRows()
        {
            var d = DataMap.FromDictionary(new OrderedDictionary()
            {
                { "foo", new float[]{ 1, 2, 3, 4, 5 } },
                { "bar", new string[]{ "a", "b", "c", "d", "e" } },
                { "baz", new int[]{ 10, 20 } }
            });

            var d2 = d.TopRows(3);

            Assert.Equal(3, d2.MaxRowCount);
            Assert.Equal(2, d2.MinRowCount);
            Assert.IsType<FilteredListView<float>>(d2["foo"].UnderlyingList);

            Assert.Equal(new float[] { 1, 2, 3 }, d2["foo"].UnderlyingList);
            Assert.Equal(new int[] { 10, 20 }, d2["baz"].UnderlyingList);
        }

        [Fact]
        public void TestBottomRows()
        {
            var d = DataMap.FromDictionary(new OrderedDictionary()
            {
                { "foo", new float[]{ 1, 2, 3, 4, 5 } },
                { "bar", new string[]{ "a", "b", "c", "d" } },
                { "baz", new int[]{ 10, 20 } }
            });

            var d2 = d.BottomRows(3);

            Assert.Equal(3, d2.MaxRowCount);
            Assert.Equal(0, d2.MinRowCount);
            Assert.IsType<FilteredListView<float>>(d2["foo"].UnderlyingList);

            Assert.Equal(new float[] { 3, 4, 5 }, d2["foo"].UnderlyingList);
            Assert.Equal(new string[] { "c", "d" }, d2["bar"].UnderlyingList);
            Assert.Equal(new int[0], d2["baz"].UnderlyingList);
        }

        [Fact]
        public void TestConcatenate()
        {
            var d1 = DataMap.FromDictionary(new Dictionary<string, IList>()
            {
                { "foo", new float[]{ 1,2,3,4,5 } },
                { "bar", new string[]{ "a", "b", "c", "d", "e" } },
            });

            var d2 = DataMap.FromDictionary(new Dictionary<string, IList>()
            {
                { "bar", new string[]{ "x", "y", "z" } },
                { "baz", new bool[]{ true, false, false } }
            });

            var result = DataMap.Concatenate(d1, d2);

            Assert.Equal(5, result.RowCount);
            Assert.Equal(new string[] { "foo", "bar", "bar_1", "baz" }, result.ColumnNames.ToArray());

            var c1 = result.GetAs<float>("foo");
            Assert.Equal(new float[] { 1, 2, 3, 4, 5 }, c1);

            var c2 = result.GetAs<string>("bar");
            Assert.Equal(new string[] { "a", "b", "c", "d", "e" }, c2);

            var c3 = result.GetAs<string>("bar_1");
            Assert.Equal(new string[] { "x", "y", "z" }, c3);

            var c4 = result.GetAs<bool>("baz");
            Assert.Equal(new bool[] { true, false, false }, c4);
        }

        [Fact]
        public void TestAsArrayKeepsArrayInstance()
        {
            var t1 = new float[] { 1, 2, 3 };

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].AsArray<float>();

            Assert.True(ReferenceEquals(t1, t2));
        }

        [Fact]
        public void TestAsListKeepsListInstance()
        {
            var t1 = new List<int>(new[] { 1, 2, 3 });

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].AsList<int>();

            Assert.True(ReferenceEquals(t1, t2));

            var t3 = d["foo"].ToList<int>();

            Assert.False(ReferenceEquals(t1, t3));
        }

        [Fact]
        public void TestSlice()
        {
            var d1 = DataMap.FromDictionary(new Dictionary<string, IList>()
            {
                { "foo", new float[]{ 1,2,3,4,5 } },
                { "bar", new string[]{ "a", "b" } }
            });

            var d2 = d1.Slice(1, 2);

            Assert.Equal(new[] { "foo", "bar" }, d2.ColumnNames);
            Assert.Equal(2, d2["foo"].Count);
            Assert.Equal(new float[] { 2, 3 }, d2["foo"].AsList<float>());
            Assert.Equal(1, d2["bar"].Count);
            Assert.Equal(new string[] { "b" }, d2["bar"].AsList<string>());
        }

        [Fact]
        public void TestToArrayCopyData()
        {
            var t1 = new float[] { 1, 2, 3 };

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].ToArray<float>();

            Assert.False(ReferenceEquals(t1, t2));
        }

        [Fact]
        public void TestToListCopyData()
        {
            var t1 = new List<int>(new[] { 1, 2, 3 });

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].ToList<int>();

            Assert.False(ReferenceEquals(t1, t2));
        }

        [Fact]
        public void TestToJagged()
        {
            var t1 = DataMap.FromDictionary(new OrderedDictionary()
            {
                {"a", new int[]{1,2,3,4,5 } },
                {"b", new int[]{100,200,300,400 } }
            });

            var a = t1.ToJagged<int>();

            Assert.Equal(1, a.Rank);
            Assert.Equal(5, a.Length);

            Assert.Equal(new int[] { 1, 100 }, a[0]);
            Assert.Equal(new int[] { 2, 200 }, a[1]);
            Assert.Equal(new int[] { 3, 300 }, a[2]);
            Assert.Equal(new int[] { 4, 400 }, a[3]);
            Assert.Equal(new int[] { 5, 0 }, a[4]);
        }

        [Fact]
        public void TestConvert()
        {
            var t1 = new List<float>(new float[] { 1, 0, 0 });

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].ToArray<bool>();

            Assert.Equal(new bool[] { true, false, false }, t2);
        }

        [Fact]
        public void TestOneHost()
        {
            var t1 = new double[] { 20, 10, 30 };

            var d = new DataMap();
            d["xxx"] = t1;

            d.OneHotEncoding("xxx", OneHotType.OneHot);

            Assert.Equal(new[] { "xxx_10", "xxx_20", "xxx_30" }, d.ColumnNames);

            Assert.Equal(new double[] { 1, 0, 0 }, d["xxx_20"].UnderlyingList);
            Assert.Equal(new double[] { 0, 1, 0 }, d["xxx_10"].UnderlyingList);
            Assert.Equal(new double[] { 0, 0, 1 }, d["xxx_30"].UnderlyingList);
        }
    }
}
