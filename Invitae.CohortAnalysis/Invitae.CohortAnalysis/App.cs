using System;
using Invitae.CohortAnalysis.Interfaces;

namespace Invitae.CohortAnalysis.ConsoleApplication
{
    public class App
    {
        private readonly ICohortAnalysisService _cohortAnalysisService;

        public App(ICohortAnalysisService cohortAnalysisService) {
            _cohortAnalysisService = cohortAnalysisService;
        }


        public void Run()
        {
            Console.WriteLine("Invitae.CohortAnalysis Started...");
            _cohortAnalysisService.RunAnalysis();
        }
    }
}
