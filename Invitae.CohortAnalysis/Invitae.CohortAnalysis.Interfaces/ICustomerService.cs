using System;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICustomerService : ICsvReader<Customer>
    {
    }
}
