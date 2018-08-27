using System;
using System.Collections.Generic;
using Invitae.CohortAnalysis.Domain.Models;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Loads the records from file path.
        /// Defaults path loads from appsettings.json
        /// </summary>
        /// <returns>The records from csv file.</returns>
        /// <param name="absolutePath">Absolute path.</param>
        IEnumerable<Order> LoadRecordsFromPath(string absolutePath);
    }
}
