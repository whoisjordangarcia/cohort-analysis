using System;
namespace Invitae.CohortAnalysis.Helpers
{
    public static class PercentageUtils
    {
        /// <summary>
        /// Calculates the percentage.
        /// </summary>
        /// <returns>The percentage.</returns>
        /// <param name="current">Current.</param>
        /// <param name="maximum">Maximum.</param>
        public static double CalculatePercentage(int current, int maximum) {
            return ((double)current / (double)maximum) * 100;
        }

        /// <summary>
        /// Formats to percentage.
        /// </summary>
        /// <returns>Formatted percentage.</returns>
        /// <param name="current">Current.</param>
        /// <param name="maximum">Maximum.</param>
        public static string FormatToPercentage(int current, int maximum) {
            return String.Format("{0:0.##}%", CalculatePercentage(current, maximum));
        }
    }
}
