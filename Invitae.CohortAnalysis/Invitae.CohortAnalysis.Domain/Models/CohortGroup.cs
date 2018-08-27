using System;
using System.Collections.Generic;

namespace Invitae.CohortAnalysis.Domain.Models
{
    public class CohortGroup
    {
        public string CohortRange { get; set; }
        public int Customers { get; set; }
        public List<Bucket> Buckets { get; set; }
    }
}
