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
    public class KFoldTest
    {
        [Fact]
        public void TestKFold()
        {
            var d = DataMap.FromDictionary(new OrderedDictionary()
            {
                { "x", new int[] { 1, 2, 3, 4, 5, 6, 7 } },
                { "y", new string[] { "a", "b", "c", "d" } }
            });

            var splitter = new KFoldSplitter(d, 3);

            var folds = splitter.EnumerateFolds().ToArray();

            Assert.Equal(3, folds.Length);

            Assert.Equal(4, folds[0].Training.MaxRowCount);
            Assert.Equal(1, folds[0].Training.MinRowCount);
            Assert.Equal(3, folds[0].Validation.MaxRowCount);
            Assert.Equal(3, folds[0].Validation.MinRowCount);

            Assert.Equal(4, folds[1].Training.MaxRowCount);
            Assert.Equal(3, folds[1].Training.MinRowCount);
            Assert.Equal(3, folds[1].Validation.MaxRowCount);
            Assert.Equal(1, folds[1].Validation.MinRowCount);

            Assert.Equal(6, folds[2].Training.MaxRowCount);
            Assert.Equal(4, folds[2].Training.MinRowCount);
            Assert.Equal(1, folds[2].Validation.MaxRowCount);
            Assert.Equal(0, folds[2].Validation.MinRowCount);

            Assert.Equal(new int[] { 1, 2, 3, 4, 5, 6 }, folds[2].Training["x"].UnderlyingList);
            Assert.Equal(new string[] { "a", "b", "c", "d" }, folds[2].Training["y"].UnderlyingList);

            Assert.Equal(new int[] { 7 }, folds[2].Validation["x"].UnderlyingList);
            Assert.Equal(new string[] {}, folds[2].Validation["y"].UnderlyingList);
        }
    }
}
