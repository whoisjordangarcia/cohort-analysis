using System;
using System.Collections.Generic;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;

namespace Invitae.CohortAnalysis.ConsoleApplication
{
    public class App
    {
        private readonly Settings _settings;
        private readonly ICohortAnalysisService _cohortAnalysisService;

        public App(IOptions<Settings> settings, 
                   ICohortAnalysisService cohortAnalysisService) {
            _cohortAnalysisService = cohortAnalysisService;
            _settings = settings.Value;
        }

        private static string getUserInput()
        {
            return Console.ReadLine();
        }

        private string RetrieveCustomerFileNameFromUser()
        {
            string defaultFileName = "customers.csv";

            Console.WriteLine("Please provide customer data filename within " +
                              "the DataFiles folder.");
            Console.WriteLine($"Note: Empty name will use default {defaultFileName}");

            string customerFileName = getUserInput();

            if (string.IsNullOrEmpty(customerFileName))
            {
                Console.WriteLine("No filename provide. " +
                                  $"Defaulting to {defaultFileName}");
                customerFileName = defaultFileName;
            }

            return customerFileName;
        }

        private string RetrieveOrderFileNameFromUser() {
            string defaultFileName = "orders.csv";

            Console.WriteLine("Please provide order data filename within the " +
                              "DataFiles folder.");
            Console.WriteLine("Note: Empty name will use default " +
                              $"{defaultFileName}");

            string orderFilePath = getUserInput();

            if (string.IsNullOrEmpty(orderFilePath))
            {
                Console.WriteLine("No file path provide. " +
                                  $"Defaulting to {defaultFileName}");
                orderFilePath = defaultFileName;
            }
            return orderFilePath;
        }

        private string RetrieveTimeZoneFromUser() {
            Console.WriteLine("Please provide your local timezone. eg. " +
                  "'America/New_York', " +
                  "'America/Los_Angeles'");

           string timezone = getUserInput();

            if (string.IsNullOrEmpty(timezone))
            {
                Console.WriteLine("No timezone provided. " +
                                  "Defaulting to 'America/New_York'");
                timezone = "America/New_York";
            }

            return timezone;
        }

        private string RetrieveOutputFileNameFromUser() {
            Console.WriteLine("Please provide output filename within the " +
                  "OutputResults folder.");

            Console.WriteLine("Please provide output path to save Cohort Analysis.");
            Console.WriteLine("Example: 'CohortResults.csv'");

           return getUserInput();
        }

        public void Run()
        {
            string orderFileName = null;
            string customerFileName = null;
            string outputFileName = null;
            string timezone = null;

            Console.WriteLine("Invitae.CohortAnalysis Initiated...");

            customerFileName = this.RetrieveCustomerFileNameFromUser();

            orderFileName = this.RetrieveOrderFileNameFromUser();

            timezone = this.RetrieveTimeZoneFromUser();

            var cohortAnalysisSetup = new CohortAnalysisSetup
            {
                CustomerFilePath = $"{_settings.DataFilesFolderPath}/{customerFileName}",
                OrderFilePath = $"{_settings.DataFilesFolderPath}/{orderFileName}",
                TimeZone = timezone,
            };

            _cohortAnalysisService.ValidateSetup(cohortAnalysisSetup);

            Console.WriteLine("Running Cohort Analysis....");

            IEnumerable<CohortGroup> cohortAnalysisData = _cohortAnalysisService.RunAnalysis(cohortAnalysisSetup);

            Console.WriteLine("Cohort Analysis Completed...");

            outputFileName = this.RetrieveOutputFileNameFromUser();

            bool didAnalysisSave = _cohortAnalysisService
                .SaveAnalysisIntoCsvFile(
                    $"{_settings.OutputResultsFolderPath}/{outputFileName}", 
                    cohortAnalysisData);

            if(didAnalysisSave) {
                Console.WriteLine("Cohort Analysis Completed...");
            } else {
                Console.WriteLine("Could not save file to given folder path");
            }
        }
    }
}
