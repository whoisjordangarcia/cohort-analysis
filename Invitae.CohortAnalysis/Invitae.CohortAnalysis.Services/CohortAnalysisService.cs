using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Helpers;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;

namespace Invitae.CohortAnalysis.Services
{
    public class CohortAnalysisService : ICohortAnalysisService
    {
        private readonly Settings _settings;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly ICohortCalculationLogic _cohortCalculationLogic;
        private readonly ICsvService _csvService;

        private CohortAnalysisSetup _cohortAnalysisSetup;
        private IEnumerable<Customer> _customerData;
        private IEnumerable<Order> _orderData;

        public CohortAnalysisService(IOptions<Settings> settings,
                                 ICustomerService customerService, 
                                 IOrderService orderService, 
                                 ICohortCalculationLogic cohortCalculationLogic,
                                 ICsvService csvService)
        {
            _settings = settings.Value;
            _customerService = customerService;
            _orderService = orderService;
            _cohortCalculationLogic = cohortCalculationLogic;
            _csvService = csvService;
        }

        private void LoadData(string customerFilePath, string orderFilePath)
        {
            Task<IEnumerable<Customer>> customerTask = Task.Factory
                .StartNew(() =>
                    _customerService.LoadRecordsFromPath(customerFilePath));

            Task<IEnumerable<Order>> orderTask = Task.Factory
                .StartNew(() =>
                    _orderService.LoadRecordsFromPath(orderFilePath));

            Task.WaitAll(customerTask, orderTask);

            _orderData = orderTask.Result;

            if (!_orderData.Any())
            {
                throw new Exception($"No data found from '{orderFilePath}'.");
            };

            _customerData = customerTask.Result;

            if (!_customerData.Any())
            {
                throw new Exception($"No data found from '{customerFilePath}.");
            }
        }

        private void ConvertDataDatesWithTimeZone(string timeZone)
        {

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);

            _customerData = _customerData
                .Select(customer =>
                {
                    customer.Created = TimeZoneInfo
                        .ConvertTime(customer.Created, timeZoneInfo);
                    return customer;
                });

            _orderData = _orderData
                .Select(order =>
                {
                    order.Created = TimeZoneInfo.ConvertTime(order.Created, timeZoneInfo);
                    return order;
                });
        }

        private List<string> GenerateHeaders(IEnumerable<CohortGroup> cohortGroups)
        {
            List<string> headers = new List<string> {
                "Cohort",
                "Customers",
            };

            var maximumBuckets = cohortGroups
                .Max(item => item.Buckets.Count());

            for (int i = 0; i < maximumBuckets; i++)
            {
                headers
                    .Add($"{BucketUtils.BucketRange(i, _settings.LifeCycleRange)} days");
            }

            return headers;
        }

        private List<List<string>> GenerateCohortResults(IEnumerable<CohortGroup> cohortGroups)
        {
            List<List<string>> cohortResults = new List<List<string>>();

            foreach (var cohortGroup in cohortGroups)
            {
                List<string> columnData = new List<string>
                {
                    cohortGroup.CohortRange,
                    $"{cohortGroup.Customers} customers",
                };

                foreach (var bucket in cohortGroup.Buckets)
                {
                    var orderersString = 
                            $"{bucket.OrderersCount.Percentage} " +
                            $"orderers ({bucket.OrderersCount.Count})\n";

                    var firstTimePurchaseString = 
                            $"{bucket.FirstTimeCount.Percentage} " +
                            $"1st time ({bucket.FirstTimeCount.Count})";

                    columnData.Add(
                        $"{orderersString}{firstTimePurchaseString}");
                }

                cohortResults.Add(columnData);
            }

            return cohortResults;
        }
       
        public bool ValidateSetup(CohortAnalysisSetup cohortAnalysisSetup) {

            if(cohortAnalysisSetup == null) {
                throw new Exception("Setup data was not provided");
            }

            if(string.IsNullOrEmpty(cohortAnalysisSetup.CustomerFilePath)) {
                throw new Exception("Customer file path was not provided");
            }

            if(!File.Exists(cohortAnalysisSetup.CustomerFilePath)) {
                throw new Exception(
                    $"Customer file path: {cohortAnalysisSetup.CustomerFilePath} " +
                    "does not exist");
            }

            if(string.IsNullOrEmpty(cohortAnalysisSetup.OrderFilePath)) {
                throw new Exception("Order file path was not provided");
            }

            if (!File.Exists(cohortAnalysisSetup.OrderFilePath))
            {
                throw new Exception(
                    $"Order file path: {cohortAnalysisSetup.OrderFilePath} " +
                    "does not exist");
            }

            if (string.IsNullOrEmpty(cohortAnalysisSetup.TimeZone)) {
                throw new Exception("Timezone was not provided");
            }

            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(cohortAnalysisSetup.TimeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                throw new Exception("Timezone is not valid " +
                                    $"{cohortAnalysisSetup.TimeZone}");
            }

            return true;
        }

        public IEnumerable<CohortGroup> RunAnalysis(CohortAnalysisSetup cohortAnalysisSetup)
        {
            if(cohortAnalysisSetup == null) {
                throw new Exception("Can not run analysis without a setting up.");
            }

            this.ValidateSetup(cohortAnalysisSetup);

            this.LoadData(
                cohortAnalysisSetup.CustomerFilePath,
                cohortAnalysisSetup.OrderFilePath
            );

            this.ConvertDataDatesWithTimeZone(cohortAnalysisSetup.TimeZone);

            IEnumerable<CohortMember> cohortMembers = _cohortCalculationLogic
                .GenerateCohortMembersBasedOnCustomerSignup(_orderData, _customerData);

            return _cohortCalculationLogic
                .MapCohortGroups(cohortMembers)
                .ToList();
        }

        public bool SaveAnalysisIntoCsvFile(string filePath, IEnumerable<CohortGroup> cohortGroups)
        {
            if(string.IsNullOrEmpty(filePath)) {
                throw new Exception("file path not provided.");
            }

            List<string> headers = this.GenerateHeaders(cohortGroups);
            List<List<string>> cohortResults = this.GenerateCohortResults(cohortGroups);

            return _csvService.SaveRecords(
                filePath, headers, cohortResults);
        }
    }
}
