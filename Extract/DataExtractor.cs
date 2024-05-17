using CsvHelper;
using CsvHelper.TypeConversion;
using SimpleETL.Entities;
using System.Globalization;

namespace SimpleETL.Extract
{
    public static class DataExtractor
    {
        public static async Task<IEnumerable<Trip>> ExtractDataAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var extractedData = new List<Trip>();
            int countOfFailedEtractions = 0;

            await csv.ReadAsync();
            csv.ReadHeader();

            while (await csv.ReadAsync())
            {
                try
                {
                    extractedData.Add(csv.GetRecord<Trip>());
                }
                catch (TypeConverterException)
                {
                    countOfFailedEtractions++;
                }
            }

            Console.WriteLine($"Total number of records: {extractedData.Count + countOfFailedEtractions}");
            Console.WriteLine($"{countOfFailedEtractions} records cannot be extacted");
            return extractedData;
        }
    }
}
