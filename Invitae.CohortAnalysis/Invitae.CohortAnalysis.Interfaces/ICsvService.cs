using System;
using System.Collections.Generic;
using CsvHelper.Configuration;

namespace Invitae.CohortAnalysis.Interfaces
{
    public interface ICsvService
    {
        /// <summary>
        /// Retrieves records from given path.
        /// </summary>
        /// <returns>Records from csv file</returns>
        /// <param name="absolutePath">Absolute path.</param>
        /// <typeparam name="TMapper">CSVMap type parameter.</typeparam>
        /// <typeparam name="T">Result type paramete.r</typeparam>
        IEnumerable<T> RetrieveRecords<TMapper, T>(string absolutePath) 
            where TMapper : ClassMap<T> where T : class;

        /// <summary>
        /// Saves records into csv file.
        /// </summary>
        /// <returns><c>true</c>, if records was saved, <c>false</c> otherwise.</returns>
        /// <param name="absolutePath">Absolute path.</param>
        /// <param name="fieldHeaders">Field headers.</param>
        /// <param name="fieldValues">Field values.</param>
        bool SaveRecords(string absolutePath, List<string> headers, List<List<string>> dataRows);
    }
}
