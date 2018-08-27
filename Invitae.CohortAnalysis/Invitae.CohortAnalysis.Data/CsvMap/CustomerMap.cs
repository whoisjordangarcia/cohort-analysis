using System;
using System.Globalization;
using CsvHelper.Configuration;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Data.CsvMap
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.Created)
                .Name("created")
                .TypeConverterOption.DateTimeStyles(DateTimeStyles.AssumeUniversal);
        }
    }
}
