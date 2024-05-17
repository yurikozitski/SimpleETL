using Simple_ETL.Load;
using Simple_ETL.Services;
using Simple_ETL.Transform;
using SimpleETL.Extract;

var extractedData = await DataExtractor.ExtractDataAsync("sample-cab-data.csv");
var transformedData = await DataTransformer.TransformDataAsync(extractedData);

string fullConnectionString = "Server=(localdb)\\mssqllocaldb;Database=trips_db;Trusted_Connection=True;";
string shortConnectionString = "Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;";

DataBaseService.ExecuteQuery(
    shortConnectionString, 
    File.ReadAllText("create_database.sql"),
    "Database created successfully!");

DataBaseService.ExecuteQuery(
    fullConnectionString,
    File.ReadAllText("create_table.sql"),
    "Table 'trips' created successfully!");

DataLoader.LoadData(fullConnectionString, transformedData);

