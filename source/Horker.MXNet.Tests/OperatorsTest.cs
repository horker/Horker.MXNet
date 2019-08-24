using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Tests
{
    [TestClass]
    public class OperatorsTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Operator.LoadSymbolCreators();
        }

        [TestMethod]
        public void TestSin()
        {
            NDArray a = NDArray.FromArray(new double[] { 3, 0, Math.PI });
            var a2 = a.ToArray<double>();
            var result = Op.Sin(a);

            var values = result.ToArray<double>();
            Assert.AreEqual(Math.Sin(3), values[0], 1e-10);
            Assert.AreEqual(Math.Sin(0), values[1], 1e-10);
            Assert.AreEqual(Math.Sin(Math.PI), values[2], 1e-10);
        }

        [TestMethod]
        public void TestAdd()
        {
            NDArray a = NDArray.FromArray(new int[] { 1, 2, 3 });
            NDArray b = NDArray.FromArray(new int[] { 4, 5, 6 });
            var result = Op.BroadcastAdd(a, b);

            var values = result.ToArray<int>();
            CollectionAssert.AreEqual(new int[] { 5, 7, 9 }, values);
        }
    }
}
