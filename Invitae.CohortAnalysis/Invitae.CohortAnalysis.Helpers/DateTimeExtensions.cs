using System;

namespace Invitae.CohortAnalysis.Helpers
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculates the start of week from given date
        /// </summary>
        /// <returns>The start of week.</returns>
        /// <param name="datetime">date time.</param>
        /// <param name="startOfWeek">Start of week.</param>
        public static DateTime StartOfWeek(this DateTime datetime, DayOfWeek startOfWeek)
        {
            int diff = (7 + (datetime.DayOfWeek - startOfWeek)) % 7;
            return datetime.AddDays(-1 * diff).Date;
        }
    }
}
