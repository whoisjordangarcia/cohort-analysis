using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Invitae.CohortAnalysis.Data.CsvMap;
using Invitae.CohortAnalysis.Interfaces;

namespace Invitae.CohortAnalysis.Services
{
    public class CsvService : ICsvService
    {
        public IEnumerable<T> RetrieveRecords<TMapper, T>(string absolutePath) 
            where TMapper : ClassMap<T> where T : class
        {
            try
            {
                using (var streamReader = new StreamReader(absolutePath))
                {
                    var reader = new CsvReader(streamReader);
                    reader.Configuration.RegisterClassMap<TMapper>();
                    return reader.GetRecords<T>().ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed retrieving records", e);
            }
        }

        public bool SaveRecords(string absolutePath, List<string> headers, 
                                List<List<string>> dataRows)
        {
            try {

                using (var streamWriter = new StreamWriter(absolutePath))
                {
                    var csv = new CsvWriter(streamWriter);

                    foreach (string header in headers)
                    {
                        csv.WriteField(header);
                    }
                    csv.NextRecord();

                    foreach (List<string> rowData in dataRows)
                    {
                        foreach(string columnValue in rowData) {
                            csv.WriteField(columnValue);
                        }
                        csv.NextRecord();
                    }

                }

                return true;

            } catch(Exception) {
                return false;
            }
        }
    }
}
