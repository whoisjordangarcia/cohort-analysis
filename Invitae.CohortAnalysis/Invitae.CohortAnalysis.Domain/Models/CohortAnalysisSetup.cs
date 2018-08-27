using System;
namespace Invitae.CohortAnalysis.Domain.Models
{
    public class CohortAnalysisSetup
    {
        public string CustomerFilePath { get; set; }
        public string OrderFilePath { get; set; }
        public string TimeZone { get; set; }
    }
}
