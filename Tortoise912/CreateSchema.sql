-- Create the database using default settings
CREATE DATABASE [Tortoise912]
GO

-- Create Table
USE Tortoise912;

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
