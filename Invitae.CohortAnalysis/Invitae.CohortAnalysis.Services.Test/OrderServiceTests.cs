using System;
using System.Collections.Generic;
using Invitae.CohortAnalysis.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class OrderServiceTests
    {
        [Fact]
        public void GetAllRecordsFromCsv_GivenACsvPathIsInConfiguration_Then_ReturnListOfOrders()
        {

            Settings settings = new Settings { OrdersCsvPath = $"../../../../Invitae.CohortAnalysis.Services.Test/MockData/orders_mock.csv" };

            var mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(settings);

            var service = new OrderService(mockSettings.Object);
            var result = service.GetAllRecordsFromCsv();

            Assert.IsType<List<Order>>(result);
            Assert.Equal(4, result.Count);
            Assert.Equal(1709, result[0].Id);
            Assert.Equal(36, result[0].OrderNumber);
            Assert.Equal(344, result[0].UserId);
            Assert.Equal(new DateTime(2014, 10, 28, 00, 20, 01), result[0].Created);

            Assert.Equal(1426, result[3].Id);
            Assert.Equal(2, result[3].OrderNumber);
            Assert.Equal(1225, result[3].UserId);
            Assert.Equal(new DateTime(2014, 10, 15, 18, 33, 38), result[3].Created);
        }
    }
}
