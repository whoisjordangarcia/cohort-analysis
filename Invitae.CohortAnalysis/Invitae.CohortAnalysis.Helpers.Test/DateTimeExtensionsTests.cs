using System;
using Xunit;

namespace Invitae.CohortAnalysis.Helpers.Test
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void StartOfWeek_WhenStartOfWeekIsSunday()
        {
            //arrange
            var datetime = new DateTime(2015, 05, 20);

            //act
            DateTime result = datetime.StartOfWeek(DayOfWeek.Sunday);

            //assert
            Assert.Equal(new DateTime(2015, 05, 17), result);
        }

        [Fact]
        public void StartOfWeek_WhenStartOfWeekIsMonday()
        {
            //arrange
            var datetime = new DateTime(2015, 05, 20);

            //act
            DateTime result = datetime.StartOfWeek(DayOfWeek.Monday);

            //assert
            Assert.Equal(new DateTime(2015, 05, 18), result);
        }
    }
}
