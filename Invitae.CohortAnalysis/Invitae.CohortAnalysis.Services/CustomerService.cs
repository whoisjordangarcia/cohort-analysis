using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Invitae.CohortAnalysis.Data.CsvMap;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;
using Microsoft.Extensions.Options;

namespace Invitae.CohortAnalysis.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICsvService _csvService;

        public CustomerService(ICsvService csvService)
        {
            _csvService = csvService;
        }

        public IEnumerable<Customer> LoadRecordsFromPath(string absolutePath)
        {
            return _csvService
                .RetrieveRecords<CustomerMap, Customer>(absolutePath);
        }
    }
}
