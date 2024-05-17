using Microsoft.Data.SqlClient;

namespace Simple_ETL.Services
{
    public static class DataBaseService
    {
        public static void ExecuteQuery(string connectionString, string queryString, string message)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                using SqlCommand createDbCommand = new SqlCommand(queryString, connection);
                
                createDbCommand.ExecuteNonQuery();
                Console.WriteLine(message);          
                
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error while executing query: {ex.Message}, Query: {queryString}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Cannot execute query");
            }
        }
    }
}
