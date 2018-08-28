using System;
using System.Collections.Generic;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICohortAnalysisService
    {
        /// <summary>
        /// Validates cohort analysis setup
        /// </summary>
        /// <returns>
        /// <c>true</c>, if setup was validated, <c>false</c> otherwise.</returns>
        /// <param name="cohortAnalysisSetup">Cohort analysis setup.</param>
        bool ValidateSetup(CohortAnalysisSetup cohortAnalysisSetup);

        /// <summary>
        /// Runs a Cohort Analysis.
        /// </summary>
        /// <returns>The analysis.</returns>
        /// <param name="cohortAnalysisSetup">Cohort analysis setup.</param>
        IEnumerable<CohortGroup> RunAnalysis(CohortAnalysisSetup cohortAnalysisSetup);

        /// <summary>
        /// Saves the Cohort Analysis into a csv file.
        /// </summary>
        /// <returns><c>true</c>, if analysis into file was saved, <c>false</c> otherwise.</returns>
        /// <param name="filePath">File path.</param>
        /// <param name="cohortGroups">Cohort groups.</param>
        bool SaveAnalysisIntoCsvFile(string filePath, IEnumerable<CohortGroup> cohortGroups);
    }
}
