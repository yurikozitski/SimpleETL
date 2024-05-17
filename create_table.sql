CREATE TABLE trips
(
    Id INT PRIMARY KEY,
    tpep_pickup_datetime DATETIME,
    tpep_dropoff_datetime DATETIME,
    passenger_count INT,
    trip_distance DECIMAL(18, 2),
    store_and_fwd_flag NVARCHAR(10),
    PULocationID INT,
    DOLocationID INT,
    fare_amount DECIMAL(18, 2),
    tip_amount DECIMAL(18, 2),
);
