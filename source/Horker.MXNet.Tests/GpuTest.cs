using System;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;
using Xunit;

namespace Horker.MXNet.Tests
{
    public class GpuTest
    {
        public GpuTest()
        {
            Operator.LoadSymbolCreators();
        }

        [Fact]
        public void TestGpuValues()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4 }, new int[] { 2, 2 }, Context.Gpu(0));

            Assert.Equal(a.DType, DType.Float32);
            Assert.Equal(a.Context, Context.Gpu(0));
            Assert.Equal(new int[] { 2, 2 }, a.Shape.Dimensions);

            var values = a.ToArray<float>();

            Assert.Equal(new float[] { 1, 2, 3, 4 }, values);
        }

        [Fact]
        public void TestGpuAdd()
        {
            var a = NDArray.FromArray(new int[] { 1, 2, 3 }, null, Context.Gpu(0));
            var b = NDArray.FromArray(new int[] { 4, 5, 6 }, null, Context.Gpu(0));
            var result = Op.BroadcastAdd(a, b);

            var values = result.ToArray<int>();
            Assert.Equal(new int[] { 5, 7, 9 }, values);
        }
    }
}
