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
        string MOCK_FOLDER_PATH = "../../../../Invitae.CohortAnalysis.Services.Test/MockData";

        [Fact]
        public void LoadRecordsFromPath_ReturnsRecordsFromCsvFile()
        {
            //arrange
            CsvService csvService = new CsvService();
            OrderService service = new OrderService(csvService);

            //act
            IEnumerable<Order> result = service.LoadRecordsFromPath($"{MOCK_FOLDER_PATH}/orders_mock.csv");
            List<Order> resultList = result.ToList();

            //assert
            Assert.Equal(4, resultList.Count);
            Assert.Equal(1709, resultList[0].Id);
            Assert.Equal(36, resultList[0].OrderNumber);
            Assert.Equal(344, resultList[0].UserId);
            Assert.Equal(new DateTime(2014, 10, 28, 00, 20, 01, DateTimeKind.Local), resultList[0].Created);

            Assert.Equal(1426, resultList[3].Id);
            Assert.Equal(2, resultList[3].OrderNumber);
            Assert.Equal(1225, resultList[3].UserId);
            Assert.Equal(new DateTime(2014, 10, 15, 18, 33, 38, DateTimeKind.Local), resultList[3].Created);
        }
    }
}
