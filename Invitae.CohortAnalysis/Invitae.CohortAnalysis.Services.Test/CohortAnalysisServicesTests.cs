using System;
using System.Collections.Generic;
using Invitae.CohortAnalysis.Business;
using Invitae.CohortAnalysis.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class CohortAnalaysisServiceTests 
    {
    //    [Fact]
    //    public void ValidateSetup_GivenEmptySetupData_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service = new 
    //            CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service.ValidateSetup(null);

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void ValidateSetup_GivenCustomerFilePathIsEmpty_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service = 
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service
    //            .ValidateSetup(new CohortAnalysisSetup {
    //                CustomerFilePath = null,
    //                OrderFilePath = "MockData/orders_mock.csv",
    //                TimeZone = "America/New_York"
    //            });

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void ValidateSetupGivenCustomerFilePathDoesNotExist_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service
    //            .ValidateSetup(new CohortAnalysisSetup
    //            {
    //                CustomerFilePath = null,
    //                OrderFilePath = "MockData/orders_mock.csv",
    //                TimeZone = "America/New_York"
    //            });

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void ValidateSetup_GivenOrderFilePathIsEmpty_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service
    //            .SetupCohortAnalysis(new CohortAnalysisSetup
    //            {
    //                OrderFilePath = "does-not-exist",
    //                CustomerFilePath = "MockData/orders_mock.csv",
    //                TimeZone = "America/New_York"
    //            });

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void ValidateSetup_GivenOrderFilePathDoesNotExist_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service
    //            .SetupCohortAnalysis(new CohortAnalysisSetup
    //            {
    //                OrderFilePath = "does-not-exist",
    //                CustomerFilePath = "MockData/customers_mock.csv",
    //                TimeZone = "America/New_York"
    //            });

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void ValidateSetup_GivenTimeZoneIsEmpty_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service
    //            .SetupCohortAnalysis(new CohortAnalysisSetup
    //            {
    //                OrderFilePath = "MockData/orders_mock.csv",
    //                CustomerFilePath = "MockData/customers_mock.csv",
    //                TimeZone = null
    //            });

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void ValidateSetup_GivenInvalidTimezone_Then_ThrowException()
    //    {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service
    //            .ValidateSetup(new CohortAnalysisSetup
    //            {
    //                OrderFilePath = "MockData/orders_mock.csv",
    //                CustomerFilePath = "MockData/customers_mock.csv",
    //                TimeZone = "USA"
    //            });

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }
    //    [Fact]
    //    public void ValidateSetup_GivenSetupIsValid_Then_ReturnTrue()
    //    {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        bool result = service
    //            .ValidateSetup(new CohortAnalysisSetup
    //            {
    //                OrderFilePath = "MockData/orders_mock.csv",
    //                CustomerFilePath = "MockData/customers_mock.csv",
    //                TimeZone = "America/New_York"
    //            });

    //        //assert
    //        Assert.Equal(true, result);
    //    }

    //    [Fact]
    //    public void RunAnalysis_GivenNoCohortAnalysisSetup_Then_ThrowException() {
    //        //arrange
    //        CohortAnalysisService service =
    //            new CohortAnalysisService(null, null, null, null, null);

    //        //act
    //        Action act = () => service.RunAnalysis(null);

    //        //assert
    //        Assert.Throws<Exception>(act);
    //    }

    //    [Fact]
    //    public void RunAnalysis_GivenAValidData_Then_ReturnCohortGroups()
    //    {
    //        var customerData = new List<Customer> {
    //            new Customer {
    //                Id = 1,
    //                Created = new DateTime(2015, 01, 01),
    //            },
    //            new Customer {
    //                Id = 2,
    //                Created = new DateTime(2015, 02, 02),
    //            }
    //        };

    //        var orderData = new List<Order> {
    //            new Order {
    //                Id = 101,
    //                OrderNumber = 101,
    //                UserId = 1,
    //                Created = new DateTime(2015, 05, 05),
    //            },
    //            new Order {
    //                Id = 103,
    //                OrderNumber = 0,
    //                UserId = 1,
    //                Created = new DateTime(2015, 01, 01)
    //            }
    //        };

    //        //arrange
    //        Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
    //        mockSettings.Setup(ap => ap.Value.LifeCycleRange).Returns(7);
    //        mockSettings.Setup(ap => ap.Value.CohortDateFormat).Returns("MM/dd/yy");

    //        CsvService csvService = new CsvService();

    //        Mock<CustomerService> mockCustomerService = new Mock<CustomerService>();
    //        mockCustomerService
    //            .Setup(ap => ap.LoadRecordsFromPath(It.IsAny<string>()))
    //            .Returns(customerData);

    //        Mock<OrderService> mockOrderService = new Mock<OrderService>();
    //        mockOrderService
    //            .Setup(ap => ap.LoadRecordsFromPath(It.IsAny<string>()))
    //            .Returns(orderData);

    //        Mock<CohortCalculationLogic> cohortCalculationLogic = new Mock<CohortCalculationLogic>();
    //        cohortCalculationLogic
    //            .Setup(ap => ap
    //                .GenerateCohortMembersBasedOnCustomerSignup(
    //                    It.IsAny<IEnumerable<Order>>(),
    //                    It.IsAny<IEnumerable<Customer>>()
    //            ))
    //            .Returns(new List<CohortMember> {
    //            new CohortMember {
    //                CustomerId = 1,
    //                OrderNumber = 101,
    //                TransactionDate = new DateTime(2015, 05, 05),
    //                CohortDate = new DateTime(2015, 01, 01),
    //                CohortIdentifier = ""
    //            }
    //        });

    //        CohortAnalysisService service =
    //            new CohortAnalysisService(
    //                mockSettings.Object,
    //                mockCustomerService.Object,
    //                mockOrderService.Object,
    //                cohortCalculationLogic.Object,
    //                null
    //            );

    //        //act
    //        IEnumerable<CohortGroup> cohortsGroups = service.RunAnalysis(new CohortAnalysisSetup
    //        {
    //            OrderFilePath = "MockData/orders_mock.csv",
    //            CustomerFilePath = "MockData/customers_mock.csv",
    //            TimeZone = "America/New_York"
    //        });

    //        //assert

    //    }

    //    public void SaveAnalysisIntoCsvFile() {

    //        throw new NotImplementedException();
    //    }
    }
}
