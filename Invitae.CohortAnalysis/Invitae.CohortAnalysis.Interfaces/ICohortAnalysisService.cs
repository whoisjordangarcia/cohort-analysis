using System;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICohortAnalysisService
    {
        /// <summary>
        /// Setups the cohort analysis
        /// </summary>
        /// <param name="cohortAnalysisSetup">Cohort analysis setup data</param>
        void SetupCohortAnalysis(CohortAnalysisSetup cohortAnalysisSetup);

        /// <summary>
        /// Runs the analysis.
        /// </summary>
        void RunAnalysis();

        /// <summary>
        /// Saves the analysis into a csv file.
        /// </summary>
        /// <returns><c>true</c>, if analysis into file was saved, <c>false</c> otherwise.</returns>
        /// <param name="filePath">File path.</param>
        bool SaveAnalysisIntoFile(string filePath);
    }
}
