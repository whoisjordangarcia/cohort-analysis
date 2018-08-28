using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Helpers;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;

namespace Invitae.CohortAnalysis.Business
{
    public class CohortCalculationLogic : ICohortCalculationLogic
    {
        private readonly Settings _settings;

        public CohortCalculationLogic(IOptions<Settings> settings) {
            _settings = settings.Value;
        }

        public IEnumerable<CohortMember> GenerateCohortMembersBasedOnCustomerSignup(
            IEnumerable<Order> orders, 
            IEnumerable<Customer> customers)
        {
            return orders
                .Join(customers,
                    args => args.UserId,
                    args => args.Id,
                      (order, customer) => this.MapCohortMember(order, customer));
        }

        public double CalculateLifeCycleStage(DateTime cohortDate, 
                                              DateTime purchaseDate, 
                                              int lifecycleRange) {
            return Math.Round(
                    (purchaseDate - cohortDate).TotalDays / lifecycleRange
                ) + 1;
        }

        public CohortMember MapCohortMember(Order order, Customer customer)
        {
            var cohortDate = customer.Created;

            return new CohortMember
            {
                CustomerId = customer.Id,
                OrderNumber = order.OrderNumber,
                TransactionDate = order.Created,
                CohortDate = cohortDate,
                CohortIdentifier = cohortDate.StartOfWeek(DayOfWeek.Sunday),
                CohortPeriod = 
                    this.CalculateLifeCycleStage(cohortDate, 
                                                 order.Created,
                                                 _settings.LifeCycleRange),
            };
        }

        public IEnumerable<CohortGroup> MapCohortGroups(IEnumerable<CohortMember>
                                                        cohortMembers)
        {
            return cohortMembers
                .GroupBy(group => group.CohortIdentifier)
                .OrderByDescending(group => group.Key)
                .Select(group => this.MapCohortGroup(group));
        }

        public string FormatCohortRange(DateTime start, DateTime end)
        {
            return 
                $"{start.ToString(_settings.CohortDateFormat)} - " +
                $"{end.ToString(_settings.CohortDateFormat)}";
        }

        public CohortGroup MapCohortGroup(IGrouping<DateTime, CohortMember> group)
        {
            return new CohortGroup
            {
                CohortRange = FormatCohortRange(
                    group.Min(g => g.CohortDate), 
                    group.Max(g => g.CohortDate)
                ),
                Customers = group.Count(),
                Buckets = group
                    .OrderBy(item => item.CohortPeriod)
                    .GroupBy(item => item.CohortPeriod)
                    .Select(bucket => this.MapBucket(bucket, group.Count()))
                    .ToList(),
            };
        }

        public Bucket MapBucket(IGrouping<double, CohortMember> bucket, int groupCount)
        {
            var firstTimePurchasesCount = bucket
                .Select(o => o.CustomerId)
                .Distinct()
                .Count();

            var orderersCount = bucket.Count() - firstTimePurchasesCount;

            return new Bucket
            {
                BucketName = bucket.Key.ToString(),
                OrderersCount = MapOrderersCount(orderersCount, groupCount),
                FirstTimeCount = MapFirstTimeCount(firstTimePurchasesCount, groupCount),
            };
        }

        public OrderersCount MapOrderersCount(int orderersCount, 
                                                      int groupCount) {
            return new OrderersCount
            {
                Percentage = PercentageUtils
                    .FormatToPercentage(orderersCount, groupCount),
                Count = orderersCount,
            };
        }

        public FirstTimeCount MapFirstTimeCount(int firstTimeCount, 
                                                        int groupCount) {
            return new FirstTimeCount
            {
                Percentage = PercentageUtils
                    .FormatToPercentage(firstTimeCount, groupCount),
                Count = firstTimeCount,
            };
        }
    };
}
