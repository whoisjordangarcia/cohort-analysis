using System;
using System.Collections.Generic;
using System.Linq;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class OrderServiceTests
    {
        [Fact]
        public void LoadRecordsFromPath_GivenAbsolutePathIsEmpty_Then_ReturnRecordsFromDefaultSettings()
        {

            Settings settings = new Settings { OrderDefaultFileName = $"../../../../Invitae.CohortAnalysis.Services.Test/MockData/orders_mock.csv" };
            Mock<IOptions<Settings>> mockSettings = new Mock<IOptions<Settings>>();
            mockSettings.Setup(ap => ap.Value).Returns(settings);
            CsvService csvService = new CsvService();

            OrderService service = new OrderService(mockSettings.Object, csvService);

            IEnumerable<Order> result = service.LoadRecordsFromPath(null);
            List<Order> resultList = result.ToList();

            Assert.Equal(4, resultList.Count);
            Assert.Equal(1709, resultList[0].Id);
            Assert.Equal(36, resultList[0].OrderNumber);
            Assert.Equal(344, resultList[0].UserId);
            Assert.Equal(new DateTime(2014, 10, 28, 00, 20, 01), resultList[0].Created);

            Assert.Equal(1426, resultList[3].Id);
            Assert.Equal(2, resultList[3].OrderNumber);
            Assert.Equal(1225, resultList[3].UserId);
            Assert.Equal(new DateTime(2014, 10, 15, 18, 33, 38), resultList[3].Created);
        }
    }
}
