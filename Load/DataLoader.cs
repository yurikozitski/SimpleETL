using Microsoft.Data.SqlClient;
using SimpleETL.Entities;
using System.Data;

namespace Simple_ETL.Load
{
    public static class DataLoader
    {
        public static void LoadData(string connectionString, IEnumerable<Trip> data)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("tpep_pickup_datetime", typeof(DateTime));
            dataTable.Columns.Add("tpep_dropoff_datetime", typeof(DateTime));
            dataTable.Columns.Add("passenger_count", typeof(int));
            dataTable.Columns.Add("trip_distance", typeof(decimal));
            dataTable.Columns.Add("store_and_fwd_flag", typeof(string));
            dataTable.Columns.Add("PULocationID", typeof(int));
            dataTable.Columns.Add("DOLocationID", typeof(int));
            dataTable.Columns.Add("fare_amount", typeof(decimal));
            dataTable.Columns.Add("tip_amount", typeof(decimal));

            int rowId = 1;

            foreach (var row in data)
            {
                dataTable.Rows.Add(
                    rowId,
                    row.TpepPickupDatetime,
                    row.TpepDropoffDatetime,
                    row.PassengerCount,
                    row.TripDistance,
                    row.StoreAndFwdFlag,
                    row.PULocationID,
                    row.DOLocationID,
                    row.FareAmount,
                    row.TipAmount
                );
                rowId++;
            }

            using SqlConnection connection = new SqlConnection(connectionString);
            
            connection.Open();

            using SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
                
            bulkCopy.DestinationTableName = "trips";
            bulkCopy.WriteToServer(dataTable);
               
            Console.WriteLine($"Data of {dataTable.Rows.Count} inserted successfully!");
        }
    }
}
