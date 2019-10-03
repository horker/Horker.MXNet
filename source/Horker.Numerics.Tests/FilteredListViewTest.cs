using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.Transformers;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class FilteredListViewTest
    {
        [Fact]
        public void TestIndexAccess()
        {
            var values = new int[] { 1, 2, 3, 4, 5, 6 };
            var filter = new bool[] { true, false, true, false, true, false };

            var l = new FilteredListView(values, filter);

            Assert.Equal(3, l.Count);
            Assert.Equal(1, l[0]);
            Assert.Equal(3, l[1]);
            Assert.Equal(5, l[2]);
        }

        [Fact]
        public void TestForeach()
        {
            var values = new int[] { 1, 2, 3, 4, 5, 6 };
            var filter = new bool[] { true, false, true, false, true, false };

            var l = new FilteredListView(values, filter);

            var items = new List<int>();

            foreach (var value in l)
                items.Add((int)value);

            Assert.Equal(3, items.Count);
            Assert.Equal(1, items[0]);
            Assert.Equal(3, items[1]);
            Assert.Equal(5, items[2]);
        }
    }
}
