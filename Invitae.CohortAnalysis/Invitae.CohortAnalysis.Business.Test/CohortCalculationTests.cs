using System;
using System.Collections.Generic;
using System.Linq;
using Invitae.CohortAnalysis.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Business.Test
{
    public class CohortCalculationTests
    {
        [Fact]
        public void MapFirstTimeCount_WhenCreatingMap()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings {
                LifeCycleRange = 7
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            FirstTimeCount result = calculationLogic.MapFirstTimeCount(10, 20);

            //assert
            Assert.Equal("50%", result.Percentage);
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void MapOrderersCount_WhenCreatingMap()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            OrderersCount result = calculationLogic.MapOrderersCount(30, 100);

            //assert
            Assert.Equal("30%", result.Percentage);
            Assert.Equal(30, result.Count);
        }

        [Fact]
        public void MapBucket_WhenCreatingMap()
        {
            //arrange
            List<CohortMember> cohortMembers = new List<CohortMember>() {
                new CohortMember {
                    CustomerId = 1,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 01),
                    CohortDate = new DateTime(2015, 01, 01),
                    CohortIdentifier = new DateTime(2015, 01, 01),
                    CohortPeriod = 1.0,
                },
                new CohortMember {
                    CustomerId = 2,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 01),
                    CohortDate = new DateTime(2015, 01, 01),
                    CohortIdentifier = new DateTime(2015, 01, 01),
                    CohortPeriod = 2.0,
                },
                new CohortMember {
                    CustomerId = 1,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 02),
                    CohortDate = new DateTime(2015, 01, 02),
                    CohortIdentifier = new DateTime(2015, 01, 02),
                    CohortPeriod = 2.0,
                },
            };

            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);


            IEnumerable<IGrouping<double?, CohortMember>> bucket =
                cohortMembers.GroupBy(cm => cm.CohortPeriod);

            //act
            Bucket result = calculationLogic.MapBucket(bucket.First(), 3);

            //assert
            Assert.Equal("1", result.BucketName);
            Assert.Equal("33.33%", result.OrderersCount.Percentage);
            Assert.Equal(1, result.OrderersCount.Count);
            Assert.Equal("33.33%", result.FirstTimeCount.Percentage);
            Assert.Equal(1, result.FirstTimeCount.Count);
        }

        [Fact]
        public void MapCohortGroup_WhenCreatingMap()
        {
            //arrange
            List<CohortMember> cohortMembers = new List<CohortMember>() {
                new CohortMember {
                    CustomerId = 1,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 01),
                    CohortDate = new DateTime(2015, 01, 01),
                    CohortIdentifier = new DateTime(2015, 01, 01),
                    CohortPeriod = 1.0,
                },
                new CohortMember {
                    CustomerId = 2,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 01),
                    CohortDate = new DateTime(2015, 01, 01),
                    CohortIdentifier = new DateTime(2015, 01, 01),
                    CohortPeriod = 2.0,
                },
                new CohortMember {
                    CustomerId = 1,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 02),
                    CohortDate = new DateTime(2015, 01, 02),
                    CohortIdentifier = new DateTime(2015, 01, 02),
                    CohortPeriod = 2.0,
                },
            };

            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);


            IEnumerable<IGrouping<DateTime, CohortMember>> bucket =
                cohortMembers.GroupBy(cm => cm.CohortIdentifier);

            //act
            CohortGroup result = calculationLogic.MapCohortGroup(bucket.First());

            //assert
            Assert.Equal("01/01/15 - 01/01/15", result.CohortRange);
            Assert.Equal(2, result.Customers);
            Assert.Equal(2, result.Buckets.Count());
        }

        [Fact]
        public void FormatCohortRange_ReadsDateFormatFromSettings() {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "yyyy-MM-dd"
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            string result = 
                calculationLogic.FormatCohortRange(
                    new DateTime(2015, 05, 01), 
                    new DateTime(2015, 05, 05)
                );

            //assert
            Assert.Equal("2015-05-01 - 2015-05-05", result);
        }   

        [Fact]
        public void MapCohortGroups_ReturnsCorrectResults() {
            //arrange
            List<CohortMember> cohortMembers = new List<CohortMember>() {
                new CohortMember {
                    CustomerId = 1,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 01),
                    CohortDate = new DateTime(2015, 01, 01),
                    CohortIdentifier = new DateTime(2015, 01, 01),
                    CohortPeriod = 1.0,
                },
                new CohortMember {
                    CustomerId = 2,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 01),
                    CohortDate = new DateTime(2015, 01, 01),
                    CohortIdentifier = new DateTime(2015, 01, 01),
                    CohortPeriod = 2.0,
                },
                new CohortMember {
                    CustomerId = 1,
                    OrderNumber = 0,
                    TransactionDate = new DateTime(2015, 01, 02),
                    CohortDate = new DateTime(2015, 01, 02),
                    CohortIdentifier = new DateTime(2015, 01, 02),
                    CohortPeriod = 2.0,
                },
            };

            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            IEnumerable<CohortGroup> result = calculationLogic.MapCohortGroups(cohortMembers);

            //assert
            Assert.Equal(2, result.Count());
            Assert.Equal("01/02/15 - 01/02/15", result.First().CohortRange);
            Assert.Equal(1, result.First().Customers);
            Assert.Equal("2", result.First().Buckets.First().BucketName);
        }

        [Fact]
        public void MapCohortMember_ReturnsCorrectResults() {
            //arrange
            Order mockOrder = new Order
            {
                Id = 1,
                OrderNumber = 123,
                UserId = 1,
                Created = new DateTime(2015, 06, 23)
            };

            Customer mockCustomer = new Customer
            {
                Id = 1,
                Created = new DateTime(2015, 01, 01)
            };

            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            CohortMember result = calculationLogic
                .MapCohortMember(mockOrder, mockCustomer);

            //assert
            Assert.Equal(1, result.CustomerId);
            Assert.Equal(123, result.OrderNumber);
            Assert.Equal(new DateTime(2015,06,23), result.TransactionDate);
            Assert.Equal(new DateTime(2015, 01, 01), result.CohortDate);
            Assert.Equal(new DateTime(2014, 12, 28), result.CohortIdentifier);
            Assert.Equal(26, result.CohortPeriod);
        }

        [Fact]
        public void CalculateLifeCycleStage_CyclePeriod() {
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            double result = calculationLogic
                .CalculateLifeCycleStage(
                    new DateTime(2015, 01, 01), 
                    new DateTime(2015, 01, 30),
                    7);

            //assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void GenerateCohortMembersBasedOnCustomerSignup_Returns_CohortMembers() {
            //assert
            List<Order> mockOrders = new List<Order>
            {
                new Order {
                    Id = 1,
                    OrderNumber = 123,
                    UserId = 1,
                    Created = new DateTime(2015, 06, 23)
                },
                new Order {
                    Id = 2,
                    OrderNumber = 0,
                    UserId = 2,
                    Created = new DateTime(2015, 06, 01)
                },
                new Order {
                    Id = 2,
                    OrderNumber = 0,
                    UserId = 1,
                    Created = new DateTime(2015, 06, 01)
                },
                new Order {
                    Id = 3,
                    OrderNumber = 0,
                    UserId = 3,
                    Created = new DateTime(2015, 06, 01)
                }
            };


            List<Customer> mockCustomers = new List<Customer>
            {
                new Customer 
                {
                    Id = 1,
                    Created = new DateTime(2015, 01, 01)
                },
                new Customer
                {
                    Id = 2,
                    Created = new DateTime(2015, 01, 01)
                },
                new Customer
                {
                    Id = 3,
                    Created = new DateTime(2015, 01, 01)
                },
            };

            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
            });

            CohortCalculationLogic calculationLogic =
                new CohortCalculationLogic(mockSettings.Object);

            //act
            IEnumerable<CohortMember> result = calculationLogic
                .GenerateCohortMembersBasedOnCustomerSignup(
                    mockOrders,
                    mockCustomers
                );

            //assert
            Assert.Equal(4, result.Count());
            Assert.Equal(1, result.First().CustomerId);
            Assert.Equal(new DateTime(2015, 01, 01), 
                         result.First().CohortDate);

        }
    }
}
