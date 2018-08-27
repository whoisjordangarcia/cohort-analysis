using System;
using System.Globalization;
using CsvHelper.Configuration;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Data.CsvMap
{
    public class CustomerMap : ClassMap<Customer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Invitae.CohortAnalysis.Data.CsvMap.CustomerMap"/> class.
        /// Maps column names to Customer object
        /// </summary>
        public CustomerMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.Created)
                .Name("created")
                // This enforces mapping date to UTC
                .TypeConverterOption.DateTimeStyles(DateTimeStyles.AssumeUniversal);
        }
    }
}
