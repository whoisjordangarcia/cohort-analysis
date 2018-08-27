using System;
namespace Invitae.CohortAnalysis.Helpers
{
    public static class BucketUtils
    {
        public static string BucketRange(int index, int bucketRange) {
            return string.Format($"{index * bucketRange} - {((index + 1) * bucketRange) - 1}");
        }
    }
}
