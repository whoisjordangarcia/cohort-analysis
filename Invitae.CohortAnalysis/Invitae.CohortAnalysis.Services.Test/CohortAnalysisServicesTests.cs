using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Invitae.CohortAnalysis.Business;
using Invitae.CohortAnalysis.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class CohortAnalaysisServiceTests 
    {
        string MOCK_FOLDER_PATH = "../../../../Invitae.CohortAnalysis.Services.Test/MockData";

        [Fact]
        public void ValidateSetup_GivenEmptySetupData_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new 
                CohortAnalysisService(mockSettings.Object, null, null, null, null);


            //act
            Action act = () => service.ValidateSetup(null);

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetup_GivenCustomerFilePathIsEmpty_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .ValidateSetup(new CohortAnalysisSetup {
                    CustomerFilePath = null,
                    OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock.csv",
                    TimeZone = "America/New_York"
                });

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetupGivenCustomerFilePathDoesNotExist_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .ValidateSetup(new CohortAnalysisSetup
                {
                    CustomerFilePath = "does-not-exist",
                    OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock.csv",
                    TimeZone = "America/New_York"
                });

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetup_GivenOrderFilePathIsEmpty_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .ValidateSetup(new CohortAnalysisSetup
                {
                    OrderFilePath = null,
                    CustomerFilePath = $"{MOCK_FOLDER_PATH}/orders_mock.csv",
                    TimeZone = "America/New_York"
                });

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetup_GivenOrderFilePathDoesNotExist_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .ValidateSetup(new CohortAnalysisSetup
                {
                    OrderFilePath = "does-not-exist",
                    CustomerFilePath = $"{MOCK_FOLDER_PATH}/customers_mock.csv",
                    TimeZone = "America/New_York"
                });

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetup_GivenTimeZoneIsEmpty_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .ValidateSetup(new CohortAnalysisSetup
                {
                    OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock.csv",
                    CustomerFilePath = $"{MOCK_FOLDER_PATH}/customers_mock.csv",
                    TimeZone = null
                });

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetup_GivenInvalidTimezone_Then_ThrowException()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .ValidateSetup(new CohortAnalysisSetup
                {
                    OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock.csv",
                    CustomerFilePath = $"{MOCK_FOLDER_PATH}/customers_mock.csv",
                    TimeZone = "USA"
                });

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateSetup_GivenSetupIsValid_Then_ReturnTrue()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            bool result = service
                .ValidateSetup(new CohortAnalysisSetup
                {
                    OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock.csv",
                    CustomerFilePath = $"{MOCK_FOLDER_PATH}/customers_mock.csv",
                    TimeZone = "America/New_York"
                });

            //assert
            Assert.True(result);
        }

        [Fact]
        public void RunAnalysis_GivenNoCohortAnalysisSetup_Then_ThrowException() 
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service.RunAnalysis(null);

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void RunAnalysis_GivenAValidData_Then_ReturnCohortGroups()
        {
            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy",

            });
            CsvService csvService = new CsvService();
            CustomerService customerService = new CustomerService(csvService);
            OrderService orderService = new OrderService(csvService);
            CohortCalculationLogic cohortCalculationLogic = new CohortCalculationLogic(mockSettings.Object);

            CohortAnalysisService service =
                new CohortAnalysisService(
                    mockSettings.Object,
                    customerService,
                    orderService,
                    cohortCalculationLogic,
                    csvService
                );

            //act
            IEnumerable<CohortGroup> cohortGroups = service.RunAnalysis(new CohortAnalysisSetup
            {
                OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock_snippet.csv",
                CustomerFilePath = $"{MOCK_FOLDER_PATH}/customers_mock_snippet.csv",
                TimeZone = "America/New_York"
            });

            //assert
            List<CohortGroup> cohortGroupsList = cohortGroups.ToList();
            Assert.Equal(24, cohortGroupsList.Count());
            Assert.Equal("06/28/15 - 07/01/15", cohortGroupsList.First().CohortRange);
            Assert.Equal("01/21/15 - 01/24/15", cohortGroupsList.Last().CohortRange);
        }

        [Fact]
        public void SaveAnalysisIntoCsvFile_NoFilePath_ThrowException() 
        {

            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy"
            });
            CohortAnalysisService service = new
                CohortAnalysisService(mockSettings.Object, null, null, null, null);

            //act
            Action act = () => service
                .SaveAnalysisIntoCsvFile(null, null);

            //assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void SaveAnalysisIntoCsvFile_ReturnsDataIntoCsv()
        {
            string mockFilePath = $"{MOCK_FOLDER_PATH}/save_analysis_snippet.csv";

            //arrange
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(new Settings
            {
                LifeCycleRange = 7,
                CohortDateFormat = "MM/dd/yy",
                DataFilesFolderPath = MOCK_FOLDER_PATH,
                OutputResultsFolderPath =  MOCK_FOLDER_PATH,
            });
            CsvService csvService = new CsvService();
            CustomerService customerService = new CustomerService(csvService);
            OrderService orderService = new OrderService(csvService);
            CohortCalculationLogic cohortCalculationLogic = new CohortCalculationLogic(mockSettings.Object);

            CohortAnalysisService service =
                new CohortAnalysisService(
                    mockSettings.Object,
                    customerService,
                    orderService,
                    cohortCalculationLogic,
                    csvService
                );

            //act
            IEnumerable<CohortGroup> cohortGroups = service.RunAnalysis(new CohortAnalysisSetup
            {
                OrderFilePath = $"{MOCK_FOLDER_PATH}/orders_mock_snippet.csv",
                CustomerFilePath = $"{MOCK_FOLDER_PATH}/customers_mock_snippet.csv",
                TimeZone = "America/New_York"
            });

            bool result = service
                .SaveAnalysisIntoCsvFile(mockFilePath, cohortGroups);

            //assert
            Assert.True(result);
            using (var streamReader = new StreamReader(mockFilePath))
            {
                CsvReader reader = new CsvReader(streamReader);
                IEnumerable<dynamic> records = reader.GetRecords<dynamic>();

                List<dynamic> recordList = records.ToList();

                Assert.Equal("06/28/15 - 07/01/15", recordList.First().Cohort);
                Assert.Equal("82 customers", recordList.First().Customers);
                Assert.Equal("01/21/15 - 01/24/15", recordList.Last().Cohort);
                Assert.Equal("19 customers", recordList.Last().Customers);
            }
        }
    }
}
