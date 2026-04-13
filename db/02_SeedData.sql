USE DeviceManagement;
GO

-- Users
IF NOT EXISTS (SELECT 1 FROM Users WHERE Name = N'Alice Johnson' AND Role = N'Developer')
    INSERT INTO Users (Name, Role, Location) VALUES (N'Alice Johnson', N'Developer', N'New York');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Name = N'Bob Smith' AND Role = N'QA Engineer')
    INSERT INTO Users (Name, Role, Location) VALUES (N'Bob Smith', N'QA Engineer', N'London');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Name = N'Carol White' AND Role = N'Project Manager')
    INSERT INTO Users (Name, Role, Location) VALUES (N'Carol White', N'Project Manager', N'Berlin');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Name = N'David Brown' AND Role = N'DevOps Engineer')
    INSERT INTO Users (Name, Role, Location) VALUES (N'David Brown', N'DevOps Engineer', N'Amsterdam');

IF NOT EXISTS (SELECT 1 FROM Users WHERE Name = N'Eva Martinez' AND Role = N'Designer')
    INSERT INTO Users (Name, Role, Location) VALUES (N'Eva Martinez', N'Designer', N'Barcelona');
GO

-- Devices (Type: 0 = Phone, 1 = Tablet)
DECLARE @AliceId INT = (SELECT Id FROM Users WHERE Name = N'Alice Johnson' AND Role = N'Developer');
DECLARE @BobId   INT = (SELECT Id FROM Users WHERE Name = N'Bob Smith'     AND Role = N'QA Engineer');
DECLARE @CarolId INT = (SELECT Id FROM Users WHERE Name = N'Carol White'   AND Role = N'Project Manager');
DECLARE @DavidId INT = (SELECT Id FROM Users WHERE Name = N'David Brown'   AND Role = N'DevOps Engineer');
DECLARE @EvaId   INT = (SELECT Id FROM Users WHERE Name = N'Eva Martinez'  AND Role = N'Designer');

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'iPhone 15 Pro' AND Manufacturer = N'Apple')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'iPhone 15 Pro', N'Apple', 0, N'iOS', N'17.4', N'Apple A17 Pro', N'8GB',
            N'High-performance Apple smartphone with a titanium frame and ProMotion display.', @AliceId);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'iPhone 14' AND Manufacturer = N'Apple')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'iPhone 14', N'Apple', 0, N'iOS', N'17.0', N'Apple A15 Bionic', N'6GB',
            N'Reliable Apple smartphone ideal for everyday business tasks.', @BobId);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'Samsung Galaxy S24 Ultra' AND Manufacturer = N'Samsung')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'Samsung Galaxy S24 Ultra', N'Samsung', 0, N'Android', N'14', N'Snapdragon 8 Gen 3', N'12GB',
            N'Flagship Samsung smartphone with built-in S Pen and a large 6.8-inch display.', @CarolId);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'Samsung Galaxy A54' AND Manufacturer = N'Samsung')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'Samsung Galaxy A54', N'Samsung', 0, N'Android', N'13', N'Exynos 1380', N'8GB',
            N'Mid-range Samsung device suitable for general corporate use.', NULL);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'Google Pixel 8 Pro' AND Manufacturer = N'Google')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'Google Pixel 8 Pro', N'Google', 0, N'Android', N'14', N'Google Tensor G3', N'12GB',
            N'Google flagship phone with advanced AI camera features and guaranteed OS updates.', @DavidId);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'iPad Pro 12.9 (6th Gen)' AND Manufacturer = N'Apple')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'iPad Pro 12.9 (6th Gen)', N'Apple', 1, N'iPadOS', N'17.4', N'Apple M2', N'16GB',
            N'Professional Apple tablet with M2 chip, great for creative and productivity work.', @EvaId);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'Samsung Galaxy Tab S9+' AND Manufacturer = N'Samsung')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'Samsung Galaxy Tab S9+', N'Samsung', 1, N'Android', N'13', N'Snapdragon 8 Gen 2', N'12GB',
            N'Premium Samsung tablet with a large AMOLED display, great for presentations.', NULL);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'Microsoft Surface Pro 9' AND Manufacturer = N'Microsoft')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'Microsoft Surface Pro 9', N'Microsoft', 1, N'Windows', N'11', N'Intel Core i7-1255U', N'16GB',
            N'Versatile Windows tablet that works as a full laptop replacement.', NULL);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'Lenovo Tab P12 Pro' AND Manufacturer = N'Lenovo')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'Lenovo Tab P12 Pro', N'Lenovo', 1, N'Android', N'12', N'Snapdragon 870', N'8GB',
            N'High-resolution Android tablet suited for media and light productivity.', NULL);

IF NOT EXISTS (SELECT 1 FROM Devices WHERE Name = N'OnePlus 12' AND Manufacturer = N'OnePlus')
    INSERT INTO Devices (Name, Manufacturer, Type, OperatingSystem, OsVersion, Processor, RamAmount, Description, UserId)
    VALUES (N'OnePlus 12', N'OnePlus', 0, N'Android', N'14', N'Snapdragon 8 Gen 3', N'16GB',
            N'Fast-charging flagship Android phone with a Hasselblad-tuned camera.', NULL);
GO
