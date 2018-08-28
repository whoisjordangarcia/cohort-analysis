using System;
using Xunit;

namespace Invitae.CohortAnalysis.Helpers.Test
{
    public class PercentageUtilsTests
    {
        [Theory]
        [InlineData(50, 100, 50)]
        [InlineData(20,50, 40)]
        [InlineData(80, 90, 88.89)]
        public void CalculatePercentage_ReturnsPercentage(int current, 
                                                          int maximum, 
                                                          double expected) {
            //arrange

            //act
            double result = PercentageUtils.CalculatePercentage(current, maximum);

            //assert
            Assert.Equal(expected.ToString(), result.ToString("#.##"));
        }

        [Theory]
        [InlineData(50, 100, "50%")]
        [InlineData(20, 50, "40%")]
        [InlineData(80, 90, "88.89%")]
        public void FormatToPercentage_ReturnsFormattedPercentage(int current, 
                                                                  int maximum, 
                                                                  string expected) {
            //arrange

            //act
            string result = PercentageUtils.FormatToPercentage(current, maximum);

            //assert
            Assert.Equal(expected, result);
        }
    }
}
