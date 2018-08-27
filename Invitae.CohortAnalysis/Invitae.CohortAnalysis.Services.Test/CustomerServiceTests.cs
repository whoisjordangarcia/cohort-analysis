using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class CustomerServiceTests
    {
        [Fact]
        public void LoadRecordsFromPath_GivenAbsolutePathIsEmpty_Then_ReturnRecordsFromDefaultSettings()
        {

            Settings settings = new Settings { 
                CustomerDefaultFileName = 
                    $"../../../../Invitae.CohortAnalysis.Services.Test/MockData/customers_mock.csv" 
            };

            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(settings);
            CsvService csvService = new CsvService();

            CustomerService service = new CustomerService(mockSettings.Object, csvService);

            IEnumerable<Customer> result = service.LoadRecordsFromPath(null);
            List<Customer> resultList = result.ToList();

            Assert.Equal(5, resultList.Count);
            Assert.Equal(35410, resultList[0].Id);
            Assert.Equal(new DateTime(2015, 07, 03, 22, 01, 11), resultList[0].Created);
            Assert.Equal(1, resultList[4].Id);
            Assert.Equal(new DateTime(2018, 05, 11, 11, 11, 11), resultList[4].Created);
        }
    }
}
