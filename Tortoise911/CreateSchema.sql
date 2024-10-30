-- Create the database using default settings
CREATE DATABASE [Tortoise911]
GO

-- Create Table
USE Tortoise911;

CREATE TABLE TortoiseALI (
    LineNumber int,
    StreetNumber int,
    Street Direction varchar(255),
    StreetName varchar(255),
    StreetSuffix varchar(255),
    City varchar(255),
    ZipCode int,
    ZipPlusFour int,
    PRIMARY KEY (LineNumber)
);

GO
