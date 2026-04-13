USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DeviceManagement')
BEGIN
    CREATE DATABASE DeviceManagement;
END
GO

USE DeviceManagement;
GO

-- Users table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Users' AND type = 'U')
BEGIN
    CREATE TABLE Users (
        Id       INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name     NVARCHAR(100) NOT NULL,
        Role     NVARCHAR(100) NOT NULL,
        Location NVARCHAR(150) NOT NULL
    );
END
GO

-- Devices table (Type: 0 = Phone, 1 = Tablet)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Devices' AND type = 'U')
BEGIN
    CREATE TABLE Devices (
        Id              INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Name            NVARCHAR(150) NOT NULL,
        Manufacturer    NVARCHAR(100) NOT NULL,
        Type            INT           NOT NULL DEFAULT 0,
        OperatingSystem NVARCHAR(50)  NOT NULL,
        OsVersion       NVARCHAR(50)  NOT NULL,
        Processor       NVARCHAR(100) NOT NULL,
        RamAmount       NVARCHAR(20)  NOT NULL,
        Description     NVARCHAR(500) NULL,
        UserId          INT           NULL,

        CONSTRAINT FK_Devices_Users FOREIGN KEY (UserId)
            REFERENCES Users(Id)
            ON DELETE SET NULL
            ON UPDATE CASCADE
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = N'FK_Devices_Users')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'Devices') AND name = N'UserId')
    BEGIN
        ALTER TABLE Devices ADD UserId INT NULL;
    END

    ALTER TABLE Devices
    ADD CONSTRAINT FK_Devices_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE SET NULL
        ON UPDATE CASCADE;
END
GO
