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
    public class OrderService : IOrderService
    {
        private readonly Settings _settings;

        public OrderService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public List<Order> GetAllRecordsFromCsv()
        {
            try
            {
                using (var streamReader = new StreamReader(_settings.OrdersCsvPath))
                {
                    var reader = new CsvReader(streamReader);
                    reader.Configuration.RegisterClassMap<OrderMap>();
                    return reader.GetRecords<Order>().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed retrieving 'Order'", e);
            }
        }
    }
}
