using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Horker.Numerics.DataMaps.Extensions;
using Xunit;

namespace Horker.Numerics.Tests
{
    public class TypeTraitTest
    {
        [Fact]
        public void TestDoubleIsNaN()
        {
            Assert.True(TypeTrait<double>.IsNaN(double.NaN));
            Assert.False(TypeTrait<double>.IsNaN(10.2));
        }

        [Fact]
        public void TestDateTimeIsNaN()
        {
            Assert.False(TypeTrait<DateTime>.IsNaN(DateTime.Now));
        }

        [Fact]
        public void TestStringIsNaN()
        {
            Assert.False(TypeTrait<string>.IsNaN(" xxx "));
            Assert.True(TypeTrait<string>.IsNaN(null));
            Assert.True(TypeTrait<string>.IsNaN(string.Empty));
            Assert.True(TypeTrait<string>.IsNaN(""));
            Assert.True(TypeTrait<string>.IsNaN("\t "));
        }
    }
}
