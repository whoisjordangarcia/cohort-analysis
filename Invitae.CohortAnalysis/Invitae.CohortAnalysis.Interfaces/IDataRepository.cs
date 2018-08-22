using System;
using System.Collections.Generic;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface IDataRepository<T>
    {
        List<T> LoadData();
    }
}
