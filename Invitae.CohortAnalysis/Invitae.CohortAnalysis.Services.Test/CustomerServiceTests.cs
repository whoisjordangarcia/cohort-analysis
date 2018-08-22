using System;
using System.Collections.Generic;
using System.IO;
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
        public void LoadDataFromCsv_GivenACsvPathIsInConfiguration_Then_ReturnListOfCustomers()
        {

            Settings settings = new Settings { CustomersCsvPath = $"../../../../Invitae.CohortAnalysis.Services.Test/MockData/customers_mock.csv" };

            var mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(settings);

            var service = new CustomerService(mockSettings.Object);
            var result = service.LoadDataFromCsv();

            Assert.IsType<List<Customer>>(result);
            Assert.Equal(result.Count, 5);
            Assert.Equal(result[0].Id, 35410);
            Assert.Equal(result[0].Created, new DateTime(2015, 07, 03, 22, 01, 11));
            Assert.Equal(result[4].Id, 1);
            Assert.Equal(result[4].Created, new DateTime(2018, 05, 11, 11, 11, 11));
        }
    }
}
