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
        private readonly ICsvService _csvService;

        public OrderService(IOptions<Settings> settings, ICsvService csvService)
        {
            _settings = settings.Value;
            _csvService = csvService;
        }

        public IEnumerable<Order> LoadRecordsFromPath(string absolutePath)
        {
            return _csvService
                .RetrieveRecords<OrderMap, Order>(absolutePath);
        }
    }
}
