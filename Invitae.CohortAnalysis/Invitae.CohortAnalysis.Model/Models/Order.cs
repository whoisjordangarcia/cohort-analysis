using System;
namespace Invitae.CohortAnalysis.Model.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
