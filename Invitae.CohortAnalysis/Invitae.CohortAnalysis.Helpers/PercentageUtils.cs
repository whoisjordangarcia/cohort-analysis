using System;
namespace Invitae.CohortAnalysis.Helpers
{
    public static class PercentageUtils
    {
        public static double CalculatePercentage(int current, int maximum) {
            return ((double)current / (double)maximum) * 100;
        }

        public static string FormatToPercentage(int current, int maximum) {
            return String.Format("{0:0.##}%", CalculatePercentage(current, maximum));
        }
    }
}
