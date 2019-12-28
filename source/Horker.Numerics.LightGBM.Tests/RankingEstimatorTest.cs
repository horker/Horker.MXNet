using System;
using Xunit;
using Horker.Numerics.LightGBM;

namespace Horker.Numerics.LightGBM.Tests
{
    public class RankingEstimatorTest
    {
        [Fact]
        public void TestGetGroups()
        {
            var groups = new[] { 1, 1, 1, 1, 2, 2, 2, 3, 4, 4, 4, 5 };

            var g = LightGBMRankingEstimator.GetGroups(groups);

            Assert.Equal(new[] { 4, 3, 1, 3, 1 }, g);
        }
    }
}
