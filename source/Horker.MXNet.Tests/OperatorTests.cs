using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horker.MXNet.Core;
using Horker.MXNet.Operators;
using Xunit;

namespace Horker.MXNet.Tests
{
    public class OperatorTest
    {
        public OperatorTest()
        {
            Operator.LoadSymbolCreators();
        }

        [Fact]
        public void TestOperatorSin()
        {
            NDArray a = NDArray.FromArray(new double[] { 3, 0, Math.PI });
            var a2 = a.ToArray<double>();
            var result = Op.Sin(a);

            var values = result.ToArray<double>();
            Assert.Equal(Math.Sin(3), values[0], 10);
            Assert.Equal(Math.Sin(0), values[1], 10);
            Assert.Equal(Math.Sin(Math.PI), values[2], 10);
        }

        [Fact]
        public void TestOperatorAdd()
        {
            NDArray a = NDArray.FromArray(new int[] { 1, 2, 3 });
            NDArray b = NDArray.FromArray(new int[] { 4, 5, 6 });
            var result = Op.BroadcastAdd(a, b);

            var values = result.ToArray<int>();
            Assert.Equal(new int[] { 5, 7, 9 }, values);
        }

        [Fact]
        public void TestOperatorDot()
        {
            NDArray a = NDArray.FromArray(new double[] { 1, 2 }, new int[] { 1, 2 });
            NDArray b = NDArray.FromArray(new double[] { 4, 5 }, new int[] { 2, 1 });
            var result = Op.Dot(a, b);

            var values = result.ToArray<double>();
            Assert.Equal(new double[] { 1 * 4 + 2 * 5 }, values);
        }
    }
}
