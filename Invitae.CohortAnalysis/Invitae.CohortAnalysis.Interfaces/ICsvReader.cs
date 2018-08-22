using System;
using System.Collections.Generic;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICsvReader<T>
    {
        List<T> GetAllRecordsFromCsv();
    }
}
