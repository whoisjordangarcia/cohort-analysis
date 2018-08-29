using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Invitae.CohortAnalysis.Data.CsvMap;
using Invitae.CohortAnalysis.Domain.Models;
using Xunit;

namespace Invitae.CohortAnalysis.Services.Test
{
    public class CsvServiceTests
    {
        string MOCK_FOLDER_PATH = "../../../../Invitae.CohortAnalysis.Services.Test/MockData";

        [Fact]
        public void RetrieveRecords_GivenAFilePath_Then_ReturnListOfRecords()
        {
            //arrange
            string mockFilePath = $"{MOCK_FOLDER_PATH}/customers_mock.csv";
            CsvService service = new CsvService();

            //act
            IEnumerable<Customer> result = service.RetrieveRecords<CustomerMap, Customer>(mockFilePath);

            //assert
            List<Customer> resultList = result.ToList();
            Assert.Equal(5, resultList.Count);
            Assert.Equal(35410, resultList[0].Id);
            Assert.Equal(new DateTime(2015, 07, 03, 22, 01, 11, DateTimeKind.Local), resultList[0].Created);
            Assert.Equal(1, resultList[4].Id);
            Assert.Equal(new DateTime(2018, 05, 11, 11, 11, 11, DateTimeKind.Local), resultList[4].Created);
        }

        [Fact]
        public void SaveRecords_GivenValidData_Then_WritesHeadersAndDataRowsIntoCSV() {
            //arrange
            string mockFilePath = $"{MOCK_FOLDER_PATH}/save_records_test.csv";
            CsvService service = new CsvService();
            List<string> headers = new List<string> {
                "headerOne",
                "headerTwo"
            };

            List<List<string>> dataRows = new List<List<string>>{
                new List<string> {
                    "column1", "column2"
                },
                new List<string> {
                    "row2-column1", "row2-column2"
                }
            };

            //act
            service.SaveRecords(mockFilePath, headers, dataRows);


            //assert
            using (var streamReader = new StreamReader(mockFilePath))
            {
                CsvReader reader = new CsvReader(streamReader);
                IEnumerable<dynamic> records = reader.GetRecords<dynamic>();

                List<dynamic> recordList = records.ToList();

                Assert.Equal("column1", recordList[0].headerOne);
                Assert.Equal("column2", recordList[0].headerTwo);
                Assert.Equal("row2-column1", recordList[1].headerOne);
                Assert.Equal("row2-column2", recordList[1].headerTwo);
            }

        }
    }
}
