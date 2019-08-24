using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

namespace Horker.MXNet.Tests
{
    [TestClass]
    public class GpuTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Operator.LoadSymbolCreators();
        }

        [TestMethod]
        public void TestGpuValues()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4 }, new int[] { 2, 2 }, Context.Gpu(0));

            Assert.AreEqual(a.DType, DType.Float32);
            Assert.AreEqual(a.Context, Context.Gpu(0));
            CollectionAssert.AreEqual(new int[] { 2, 2 }, a.Shape.Dimensions);

            var values = a.ToArray<float>();

            CollectionAssert.AreEqual(new float[] { 1, 2, 3, 4 }, values);
        }

        [TestMethod]
        public void TestGpuAdd()
        {
            var a = NDArray.FromArray(new int[] { 1, 2, 3 }, null, Context.Gpu(0));
            var b = NDArray.FromArray(new int[] { 4, 5, 6 }, null, Context.Gpu(0));
            var result = Op.BroadcastAdd(a, b);

            var values = result.ToArray<int>();
            CollectionAssert.AreEqual(new int[] { 5, 7, 9 }, values);
        }
    }
}
