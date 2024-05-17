using CsvHelper;
using Simple_ETL.Services;
using SimpleETL.Entities;

namespace Simple_ETL.Transform
{
    public static class DataTransformer
    {
        public static async Task<IEnumerable<Trip>> TransformDataAsync(IEnumerable<Trip> data)
        {
            var groupedData 
                = data.GroupBy(t => new { t.TpepPickupDatetime, t.TpepDropoffDatetime, t.PassengerCount });

            var uniqueData
                = groupedData.Select(g => g.First())
                .ToList();

            var duplicateData 
                = groupedData.Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1))
                .ToList();

            Console.WriteLine($"{duplicateData.Count} records are duplicates");

            foreach (var trip in uniqueData)
            {
                if (string.Equals(trip.StoreAndFwdFlag.Trim(), "N", StringComparison.InvariantCultureIgnoreCase))
                {
                    trip.StoreAndFwdFlag = "No";
                }
                else if (string.Equals(trip.StoreAndFwdFlag.Trim(), "Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    trip.StoreAndFwdFlag = "Yes";
                }

                TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                trip.TpepPickupDatetime = TimeZoneInfo.ConvertTimeToUtc(trip.TpepPickupDatetime, est);
                trip.TpepDropoffDatetime = TimeZoneInfo.ConvertTimeToUtc(trip.TpepDropoffDatetime, est);
            }

            await FileService.WriteDataToCSV(duplicateData);

            return uniqueData;
        }
    }
}
