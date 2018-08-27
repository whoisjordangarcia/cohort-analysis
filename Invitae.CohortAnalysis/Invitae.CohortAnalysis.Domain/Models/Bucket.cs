using System;
namespace Invitae.CohortAnalysis.Domain.Models
{
    public class Bucket
    {
        public string BucketName { get; set; }
        public OrderersCount OrderersCount { get; set; }
        public FirstTimeCount FirstTimeCount { get; set; }
    }
}
