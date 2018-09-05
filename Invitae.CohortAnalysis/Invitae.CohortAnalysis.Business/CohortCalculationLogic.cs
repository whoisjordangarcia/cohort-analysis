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
            return from c in customers
                join o in orders
                on c.Id equals o.UserId
                into a from b in a.DefaultIfEmpty(new Order())
                select this.MapCohortMember(b, c);
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
                OrderNumber = order?.OrderNumber,
                TransactionDate = order?.Created,
                CohortDate = cohortDate,
                CohortIdentifier = cohortDate.StartOfWeek(DayOfWeek.Sunday),
                CohortPeriod = order != null ? 
                    this.CalculateLifeCycleStage(cohortDate, 
                                                 order.Created,
                                                 _settings.LifeCycleRange) : -1,
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
            int totalUniqueCustomersWithinCohort = group
                .Select(o => o.CustomerId)
                .Distinct()
                .Count();
    
            return new CohortGroup
            {
                CohortRange = FormatCohortRange(
                    group.Min(g => g.CohortDate), 
                    group.Max(g => g.CohortDate)
                ),
                Customers = totalUniqueCustomersWithinCohort,
                Buckets = group
                    .Where(item => item.CohortPeriod > 0)
                    .OrderBy(item => item.CohortPeriod)
                    .GroupBy(item => item.CohortPeriod)
                    .Select(bucket => this.MapBucket(bucket, totalUniqueCustomersWithinCohort))
                    .ToList(),
            };
        }

        public Bucket MapBucket(IGrouping<double?, CohortMember> bucket, 
                                int numberOfCustomersWithinGroup)
        {

            var totalAmountOfOrders = bucket
                .Count(o => o.OrderNumber != null);

            var totalAmountOfFirstTimePurchasers = bucket
                .Where(o => o.OrderNumber != null)
                .Select(o => o.CustomerId).Distinct().Count();

            return new Bucket
            {
                BucketName = bucket.Key.ToString(),
                OrderersCount = 
                    MapOrderersCount(totalAmountOfOrders, numberOfCustomersWithinGroup),
                FirstTimeCount = 
                    MapFirstTimeCount(totalAmountOfFirstTimePurchasers, numberOfCustomersWithinGroup),
            };
        }

        public OrderersCount MapOrderersCount(int orderersCount, 
                                                      int numberOfCustomersWithinGroup) {
            return new OrderersCount
            {
                Percentage = PercentageUtils
                    .FormatToPercentage(orderersCount, numberOfCustomersWithinGroup),
                Count = orderersCount,
            };
        }

        public FirstTimeCount MapFirstTimeCount(int firstTimeCount, 
                                                        int numberOfCustomersWithinGroup) {
            return new FirstTimeCount
            {
                Percentage = PercentageUtils
                    .FormatToPercentage(firstTimeCount, numberOfCustomersWithinGroup),
                Count = firstTimeCount,
            };
        }
    };
}
