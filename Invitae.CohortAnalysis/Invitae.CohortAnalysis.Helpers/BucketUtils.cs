using System;
namespace Invitae.CohortAnalysis.Helpers
{
    public static class BucketUtils
    {
        /// <summary>
        /// Buckets the range.
        /// </summary>
        /// <returns>The range.</returns>
        /// <param name="index">Index.</param>
        /// <param name="bucketRange">Bucket range.</param>
        public static string BucketRange(int index, int bucketRange) {
            return string.Format($"{index * bucketRange} - {((index + 1) * bucketRange) - 1}");
        }
    }
}
