using System;
using System.Collections.Generic;
using System.Linq;
using Horker.MXNet.Core;
using Horker.MXNet.PowerShell;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Horker.MXNet.Tests
{
    [TestClass]
    public class ToStringTest
    {
        [TestMethod]
        public void TestToStringInLongFormat()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 });

            var values = a.ToArray<float>();

            var s = a.ToStringInLongFormat<float>();

            var expected =
                "[2 x 3, Single]\r\n" +
                " 1  2  3\r\n" +
                " 4  5  6";

            Assert.AreEqual(expected, s);
        }

        [TestMethod]
        public void TestToStringInShortFormat()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6 }, new int[] { 2, 3 });

            var s = a.ToString();

            var expected = "[2 x 3, Single] 1 2 3 4 5...";

            Assert.AreEqual(expected, s);
        }

        [TestMethod]
        public void TestToStringInLongFormat2()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6, 7, 888, 9, 10, -111, 12 }, new int[] { 3, 2, 2 });

            var s = a.ToStringInLongFormat<float>();

            var expected =
                "[3 x 2 x 2, Single]\r\n" +
                "(0, _, _) =\r\n" +
                "   1    2\r\n" +
                "   3    4\r\n" +
                "(1, _, _) =\r\n" +
                "   5    6\r\n" +
                "   7  888\r\n" +
                "(2, _, _) =\r\n" +
                "   9   10\r\n" +
                "-111   12";

            Assert.AreEqual(expected, s);
        }

        [TestMethod]
        public void TestToStringInShortFormat2()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6, 7, 888, 9, 10, -111, 12 }, new int[] { 2, 2, 3 });

            var s = a.ToString();

            var expected = "[2 x 2 x 3, Single] 1 2 3 4 5...";

            Assert.AreEqual(expected, s);
        }

        [TestMethod]
        public void TestToStringInLongFormat3()
        {
            var a = NDArray.FromArray(new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }, new int[] { 3, 2, 2, 1, 1 });

            var s = a.ToStringInLongFormat<float>();

            var expected =
                "[3 x 2 x 2 x 1 x 1, Single]\r\n" +
                "(0, 0, 0, _, _) =\r\n" +
                "  1\r\n" +
                "(0, 0, 1, _, _) =\r\n" +
                "  2\r\n" +
                "(0, 1, 0, _, _) =\r\n" +
                "  3\r\n" +
                "(0, 1, 1, _, _) =\r\n" +
                "  4\r\n" +
                "(1, 0, 0, _, _) =\r\n" +
                "  5\r\n" +
                "(1, 0, 1, _, _) =\r\n" +
                "  6\r\n" +
                "(1, 1, 0, _, _) =\r\n" +
                "  7\r\n" +
                "(1, 1, 1, _, _) =\r\n" +
                "  8\r\n" +
                "(2, 0, 0, _, _) =\r\n" +
                "  9\r\n" +
                "(2, 0, 1, _, _) =\r\n" +
                " 10\r\n" +
                "(2, 1, 0, _, _) =\r\n" +
                " 11\r\n" +
                "(2, 1, 1, _, _) =\r\n" +
                " 12";

            Assert.AreEqual(expected, s);
        }
    }
}
