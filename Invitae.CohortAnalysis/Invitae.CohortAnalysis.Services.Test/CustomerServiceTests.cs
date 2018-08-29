using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Invitae.CohortAnalysis.Data.CsvMap;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class CustomerServiceTests
    {
        string MOCK_FOLDER_PATH = "../../../../Invitae.CohortAnalysis.Services.Test/MockData";

        [Fact]
        public void LoadRecordsFromPath_ReturnsRecordsFromCsvFile()
        {
            //arrange
            CsvService csvService = new CsvService();
            CustomerService service = new CustomerService(csvService);

            //act
            IEnumerable<Customer> result = service.LoadRecordsFromPath($"{MOCK_FOLDER_PATH}/customers_mock.csv");
            List<Customer> resultList = result.ToList();

            //assert
            Assert.Equal(5, resultList.Count);
            Assert.Equal(35410, resultList[0].Id);
            Assert.Equal(new DateTime(2015, 07, 03, 22, 01, 11, DateTimeKind.Local), resultList[0].Created);
            Assert.Equal(1, resultList[4].Id);
            Assert.Equal(new DateTime(2018, 05, 11, 11, 11, 11, DateTimeKind.Local), resultList[4].Created);
        }
    }
}
