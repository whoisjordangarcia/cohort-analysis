using System;
namespace Invitae.CohortAnalysis.Domain.Models
{
    public class CohortMember
    {
        public int CustomerId { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime CohortDate { get; set; }
        public DateTime CohortIdentifier { get; set; } 
        public double? CohortPeriod { get; set; }
    }
}
