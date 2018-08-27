using System;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICohortAnalysisService
    {
        void SetupCohortAnalysis(CohortAnalysisSetup cohortAnalysisSetup);
        void RunAnalysis();
        bool SaveAnalysisIntoFile(string filePath);
    }
}
