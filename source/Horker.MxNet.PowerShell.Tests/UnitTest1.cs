using MxNet;
using System;
using Xunit;

namespace Horker.MxNet.PowerShell.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestTo2DArray()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6 };
            var ndarray = new NDArray(data);
            ndarray = ndarray.Reshape(2, 3);
            var a = ndarray.To2DArray<int>();

            Assert.Equal(2, a.GetLength(0));
            Assert.Equal(3, a.GetLength(1));

            Assert.Equal(new[,] { { 1, 2, 3 }, { 4, 5, 6 } }, a);
        }
    }
}
