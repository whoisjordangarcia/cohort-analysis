using System;
using Xunit;

namespace Invitae.CohortAnalysis.Helpers.Test
{
    public class BucketUtilsTests 
    {
        [Theory]
        [InlineData(0, 7, "0 - 6")]
        [InlineData(1, 7, "7 - 13")]
        [InlineData(2, 7, "14 - 20")]
        public void BucketRange(int index, int bucketRange, string expected) 
        {
            //arrange

            //act
            String result = BucketUtils.BucketRange(index, bucketRange);

            //assert
            Assert.Equal(expected, result);
        }
    }
}
