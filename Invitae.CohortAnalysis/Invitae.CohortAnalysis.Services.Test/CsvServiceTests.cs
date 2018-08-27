using System;
using System.Collections.Generic;
using System.Linq;
using Invitae.CohortAnalysis.Data.CsvMap;
using Invitae.CohortAnalysis.Domain.Models;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class CsvServiceTests
    {
        [Fact]
        public void RetrieveRecords_GivenAFilePath_Then_ReturnListOfRecords()
        {
            string mockPath = $"../../../../Invitae.CohortAnalysis.Services.Test/MockData/customers_mock.csv";
            CsvService service = new CsvService();

            IEnumerable<Customer> result = service.RetrieveRecords<CustomerMap, Customer>(mockPath);
            List<Customer> resultList = result.ToList();

            Assert.Equal(5, resultList.Count);
            Assert.Equal(35410, resultList[0].Id);
            Assert.Equal(new DateTime(2015, 07, 03, 22, 01, 11), resultList[0].Created);
            Assert.Equal(1, resultList[4].Id);
            Assert.Equal(new DateTime(2018, 05, 11, 11, 11, 11), resultList[4].Created);
        }

        [Fact]
        public void SaveRecords() {

        }
    }
}
