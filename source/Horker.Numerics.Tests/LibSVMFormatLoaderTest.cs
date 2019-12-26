using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Horker.Numerics.Utilities;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class LibSVMFormatLoaderTest
    {
        [Fact]
        public void TestLoad()
        {
            var text = @"1 1:10 2:20 3:30
-1 2:22 9:99";

            double[] labels;
            double[,] data;
            using (var s = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(text)), Encoding.UTF8))
            {
                (labels, data) = LibSVMFormatLoader.Load<double>(s);
            }

            Assert.Equal(new double[] { 1, -1 }, labels);

            Assert.Equal(2, data.GetLength(0));
            Assert.Equal(9, data.GetLength(1));
            Assert.Equal(10.0, data[0, 0]);
            Assert.Equal(20.0, data[0, 1]);
            Assert.Equal(30.0, data[0, 2]);
            Assert.Equal(0.0, data[0, 8]);

            Assert.Equal(0.0, data[1, 0]);
            Assert.Equal(22.0, data[1, 1]);
            Assert.Equal(0.0, data[1, 4]);
            Assert.Equal(99.0, data[1, 8]);
        }
    }
}
