using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Invitae.CohortAnalysis.Data.CsvMap;
using Invitae.CohortAnalysis.Domain.Models;
using Invitae.CohortAnalysis.Interfaces;

namespace Invitae.CohortAnalysis.Services
{
    public class CustomerService : ICustomerService
    {
        public List<Customer> LoadData()
        {
            try
            {
                using (var streamReader = new StreamReader(_settings.CustomersCsvPath))
                {
                    var reader = new CsvReader(streamReader);
                    reader.Configuration.RegisterClassMap<CustomerMap>();
                    return reader.GetRecords<Customer>().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed retrieving 'Customer'", e);
            }
        }
    }
}
