using System.Globalization;
using CsvHelper.Configuration;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Data.CsvMap
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.OrderNumber).Name("order_number");
            Map(m => m.UserId).Name("user_id");
            Map(m => m.Created)
                .Name("created")
                .TypeConverterOption.DateTimeStyles(DateTimeStyles.AssumeUniversal);
        }
    }
}
