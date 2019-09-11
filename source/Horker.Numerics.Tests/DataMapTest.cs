using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
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

            Assert.Equal(t1, d["int"].AsArray<int>());
            Assert.Equal(t2, d["string"].AsArray<string>());

            Assert.Equal(t1, d.GetAs<int>("int"));
            Assert.Equal(t2, d.GetAs<string>("string"));
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

            var result = d.FilterRows(i => i % 2 == 0);

            Assert.Equal(3, result.RowCount);
            Assert.Equal(new string[] { "foo", "bar" }, result.ColumnNames.ToArray());

            var c1 = result.GetAs<float>("foo");
            Assert.Equal(new float[] { 1, 3, 5 }, c1);

            var c2 = result.GetAs<string>("bar");
            Assert.Equal(new string[] { "a", "c", "e" }, c2);
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
        public void TestAsArrayKeepArrayInstance()
        {
            var t1 = new float[] { 1, 2, 3 };

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].AsArray<float>();

            Assert.True(ReferenceEquals(t1, t2));
        }

        [Fact]
        public void TestAsListKeepListInstance()
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
        public void TestConvert()
        {
            var t1 = new List<float>(new float[] { 1, 0, 0 });

            var d = new DataMap();
            d.Add("foo", t1);

            var t2 = d["foo"].Convert<bool>();

            Assert.Equal(new bool[] { true, false, false }, t2.ToArray());
        }
    }
}
