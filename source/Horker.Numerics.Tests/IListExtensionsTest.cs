using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps;
using Horker.Numerics.DataMaps.Extensions;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class IListExtensionsTest
    {
        [Fact]
        public void TestGetDataTpe()
        {
            var t1 = IListExtensions.GetDataType(new int[0]);
            Assert.Equal(typeof(int), t1);

            var t2 = IListExtensions.GetDataType(new List<double>());
            Assert.Equal(typeof(double), t2);

            var t3 = IListExtensions.GetDataType(new ArrayList());
            Assert.Equal(typeof(object), t3);
        }
    }
}
