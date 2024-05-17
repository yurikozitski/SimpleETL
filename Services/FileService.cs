using CsvHelper;
using SimpleETL.Entities;
using System.Globalization;


namespace Simple_ETL.Services
{
    public static class FileService
    {
        public static async Task WriteDataToCSV(IEnumerable<Trip> data)
        {
            using (var writer = new StreamWriter("duplicates.csv"))
            using (var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                await csv.WriteRecordsAsync(data);
            }
        }
    }
}
