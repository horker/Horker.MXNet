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

        [TestMethod]
        public void TestReduceSum()
        {
            var a = NDArray.FromArray(new double[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });
            var b = a.Sum();

            var result = b.ToArray<double>();
            CollectionAssert.AreEqual(new int[] { 1 }, b.Shape.Dimensions);
            CollectionAssert.AreEqual(new double[] { 6 * 7 / 2 }, result);
        }

        [TestMethod]
        public void TestAdd()
        {
            var a = NDArray.FromArray(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });
            var b = NDArray.FromArray(new int[] { 1, 2, 3 }, new int[] { 3, 1 });

            var c = a + b;

            var result = c.ToArray<int>();
            CollectionAssert.AreEqual(new int[] { 2, 3, 5, 6, 8, 9 }, result);
        }

        [TestMethod]
        public void TestRDiv()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3 }, new int[] { 3 });
            var b = NDArray.FromArray(new float[] { 4, 5, 6 }, new int[] { 3 });

            var c = a / 3;

            var result = c.ToArray<float>();
            Assert.AreEqual(1.0 / 3, result[0], 1e-5);
            Assert.AreEqual(2.0 / 3, result[1], 1e-5);
            Assert.AreEqual(3.0 / 3, result[2], 1e-5);

            c = 3 / a;

            result = c.ToArray<float>();
            Assert.AreEqual(3.0 / 1, result[0], 1e-5);
            Assert.AreEqual(3.0 / 2, result[1], 1e-5);
            Assert.AreEqual(3.0 / 3, result[2], 1e-5);
        }
    }
}
