using System;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Horker.MXNet.Tests
{
    [TestClass]
    public class NDArrayTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Operator.LoadSymbolCreators();
        }

        [TestMethod]
        public void TestCreate()
        {
            var data = new float[]{ 1, 2, 3, 4 };
            var a = NDArray.FromArray(data);

            CollectionAssert.AreEqual(a.Shape.Dimensions, new int[] { 4 });
            Assert.AreEqual(a.Size, 4);

            var arrayData = a.ToArray<float>();

            CollectionAssert.AreEqual(data, arrayData);
        }

        [TestMethod]
        public void TestOnes()
        {
            var a = NDArray.Ones(new int[] { 2, 3 });

            CollectionAssert.AreEqual(a.Shape.Dimensions, new int[] { 2, 3 });
            Assert.AreEqual(a.Size, 6);

            var arrayData = a.ToArray<float>();

            CollectionAssert.AreEqual(new float[] { 1, 1, 1, 1, 1, 1 }, arrayData);
        }
    }
}
