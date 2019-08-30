using System;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;
using Xunit;

namespace Horker.MXNet.Tests
{
    public class NDArrayTest
    {
        public NDArrayTest()
        {
            Operator.LoadSymbolCreators();
        }

        [Fact]
        public void TestCreate()
        {
            var data = new float[]{ 1, 2, 3, 4 };
            var a = NDArray.FromArray(data);

            Assert.Equal(new int[] { 4 }, a.Shape.Dimensions);
            Assert.Equal(4, a.Size);

            var arrayData = a.ToArray<float>();

            Assert.Equal(data, arrayData);
        }

        [Fact]
        public void TestOnes()
        {
            var a = NDArray.Ones(new int[] { 2, 3 });

            Assert.Equal(new int[] { 2, 3 }, a.Shape.Dimensions);
            Assert.Equal(6, a.Size);

            var arrayData = a.ToArray<float>();

            Assert.Equal(new float[] { 1, 1, 1, 1, 1, 1 }, arrayData);
        }

        [Fact]
        public void TestReduceSum()
        {
            var a = NDArray.FromArray(new double[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });
            var b = a.Sum();

            var result = b.ToArray<double>();
            Assert.Equal(new int[] { 1 }, b.Shape.Dimensions);
            Assert.Equal(new double[] { 6 * 7 / 2 }, result);
        }

        [Fact]
        public void TestAdd()
        {
            var a = NDArray.FromArray(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });
            var b = NDArray.FromArray(new int[] { 1, 2, 3 }, new int[] { 3, 1 });

            var c = a + b;

            var result = c.ToArray<int>();
            Assert.Equal(new int[] { 2, 3, 5, 6, 8, 9 }, result);
        }

        [Fact]
        public void TestRDiv()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3 }, new int[] { 3 });
            var b = NDArray.FromArray(new float[] { 4, 5, 6 }, new int[] { 3 });

            var c = a / 3;

            var result = c.ToArray<float>();
            Assert.Equal(1.0 / 3, result[0], 5);
            Assert.Equal(2.0 / 3, result[1], 5);
            Assert.Equal(3.0 / 3, result[2], 5);

            c = 3 / a;

            result = c.ToArray<float>();
            Assert.Equal(3.0 / 1, result[0], 5);
            Assert.Equal(3.0 / 2, result[1], 5);
            Assert.Equal(3.0 / 3, result[2], 5);
        }

        [Fact]
        public void TestIntexGetter()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4 }, new int[] { 2, 2 });

            Assert.Equal(3, a[1, 0]);
        }

        [Fact]
        public void TestIntexGetter2()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4 }, new int[] { 2, 2 });

            var b = a[new[] { 0, 0 }, new[] { 2, 1 }];

            Assert.Equal(new int[] { 2, 1 }, b.Shape.Dimensions);

            var result = b.ToArray<float>();
            Assert.Equal(new float[] { 1, 3 }, result);
        }

        [Fact]
        public void TestTo2DArray()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });

            var b = a.To2DArray<float>();

            Assert.Equal(2, b.Rank);
            Assert.Equal(3, b.GetLength(0));
            Assert.Equal(2, b.GetLength(1));

            Assert.Equal(1, b[0, 0]);
            Assert.Equal(2, b[0, 1]);
            Assert.Equal(3, b[1, 0]);
            Assert.Equal(4, b[1, 1]);
            Assert.Equal(5, b[2, 0]);
            Assert.Equal(6, b[2, 1]);
        }

        [Fact]
        public void TestTo2DJagged()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6 }, new int[] { 3, 2 });

            var b = a.To2DJagged<float>();

            Assert.Equal(1, b.Rank);
            Assert.Equal(3, b.Length);

            Assert.Equal(1, b[0][0]);
            Assert.Equal(2, b[0][1]);
            Assert.Equal(3, b[1][0]);
            Assert.Equal(4, b[1][1]);
            Assert.Equal(5, b[2][0]);
            Assert.Equal(6, b[2][1]);
        }
    }
}
