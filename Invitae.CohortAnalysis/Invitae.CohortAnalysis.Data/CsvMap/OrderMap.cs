using System.Globalization;
using CsvHelper.Configuration;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Data.CsvMap
{

    public class OrderMap : ClassMap<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Invitae.CohortAnalysis.Data.CsvMap.OrderMap"/> class.
        /// Maps column names to Order object
        /// </summary>
        public OrderMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.OrderNumber).Name("order_number");
            Map(m => m.UserId).Name("user_id");
            Map(m => m.Created)
                .Name("created")
                // This enforces mapping date to UTC
                .TypeConverterOption.DateTimeStyles(DateTimeStyles.AdjustToUniversal);
        }
    }
}
