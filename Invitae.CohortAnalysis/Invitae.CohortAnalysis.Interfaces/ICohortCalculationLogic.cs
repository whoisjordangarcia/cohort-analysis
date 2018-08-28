using System;
using System.Collections.Generic;
using System.Linq;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICohortCalculationLogic
    {
        /// <summary>
        /// Formats the cohort range.
        /// </summary>
        /// <returns>The cohort range.</returns>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        string FormatCohortRange(DateTime start, DateTime end);

        /// <summary>
        /// Calculates the life cycle stage used for a cohort member
        /// </summary>
        /// <returns>The life cycle stage.</returns>
        /// <param name="cohortDate">Cohort date.</param>
        /// <param name="purchaseDate">Purchase date.</param>
        /// <param name="lifecycleRange">Lifecycle range.</param>
        double CalculateLifeCycleStage(DateTime cohortDate,
                                       DateTime purchaseDate,
                                       int lifecycleRange);

        /// <summary>
        /// Generates the cohort members based on customer signup.
        /// </summary>
        /// <returns>The cohort members based on customer signup.</returns>
        /// <param name="orders">Order list</param>
        /// <param name="customers">Customer list</param>
        IEnumerable<CohortMember> GenerateCohortMembersBasedOnCustomerSignup(
            IEnumerable<Order> orders, IEnumerable<Customer> customers);
            
        /// <summary>
        /// Maps the cohort groups.
        /// </summary>
        /// <returns>The cohort groups.</returns>
        /// <param name="cohortMembers">Cohort members.</param>
        IEnumerable<CohortGroup> MapCohortGroups(
            IEnumerable<CohortMember> cohortMembers);

        /// <summary>
        /// Maps the cohort member.
        /// </summary>
        /// <returns>The cohort member.</returns>
        /// <param name="order">Order data</param>
        /// <param name="customer">Customer data</param>
        CohortMember MapCohortMember(Order order, Customer customer);

        /// <summary>
        /// Maps the the cohort life cycle bucket
        /// </summary>
        /// <returns>The bucket.</returns>
        /// <param name="bucket">Bucket.</param>
        /// <param name="groupCount">Group total amount</param>
        Bucket MapBucket(IGrouping<double,
                         CohortMember> bucket,
                        int groupCount);

        /// <summary>
        /// Maps Orderers data
        /// </summary>
        /// <returns>The orderers count.</returns>
        /// <param name="orderersCount">Orderers count.</param>
        /// <param name="groupCount">Group count.</param>
        OrderersCount MapOrderersCount(int orderersCount, int groupCount);

        /// <summary>
        /// Maps First time purchases data
        /// </summary>
        /// <returns>The first time count.</returns>
        /// <param name="firstTimeCount">First time count.</param>
        /// <param name="groupCount">Group count.</param>
        FirstTimeCount MapFirstTimeCount(int firstTimeCount, int groupCount);
    }
}
