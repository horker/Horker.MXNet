using System;
using System.Collections.Generic;
using System.Linq;
using Horker.MXNet.Operators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Horker.Numerics.Tests
{
    [TestClass]
    public class OperatorTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Operator.LoadSymbolCreators();
        }

        [TestMethod]
        public void TestAdd()
        {
            var a = NDArray<int>.Create(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });
            var b = NDArray<int>.Create(new int[] { 1, 2, 3 }, new int[] { 3, 1 });
            var c = a + b;

            var result = c.ToArray();
            CollectionAssert.AreEqual(new int[] { 2, 3, 5, 6, 8, 9 }, result);
        }
    }
}
