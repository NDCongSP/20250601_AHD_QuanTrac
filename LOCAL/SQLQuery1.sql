-- Step 0: Switch to master database to disconnect from RegistrationForm10
USE master;
GO

-- Step 1: Force disconnect users and drop the database
IF DB_ID('RegistrationForm10') IS NOT NULL
BEGIN
    ALTER DATABASE RegistrationForm10 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RegistrationForm10;
END
GO

-- Step 2: Recreate the database
CREATE DATABASE RegistrationForm10;
GO

-- Step 3: Use the new database
USE RegistrationForm10;
GO

-- Step 4: Create table if not exists
CREATE TABLE Users (
    UserID int IDENTITY(1,1) PRIMARY KEY,
    FullName nvarchar(100) NOT NULL UNIQUE,
    DateOfBirth date NOT NULL,
    Password nvarchar(255) NOT NULL,
    Position nvarchar(20) NOT NULL CHECK (Position IN ('Quản trị viên', 'Nhân viên', 'Vận hành bảo trì')),
    CreatedDate datetime DEFAULT GETDATE(),
    IsActive bit DEFAULT 1
);
GO

-- Step 5: Insert sample data only if not exists
IF NOT EXISTS (SELECT 1 FROM Users WHERE FullName = 'Admin System')
BEGIN
    INSERT INTO Users (FullName, DateOfBirth, Password, Position) VALUES
    ('Admin System', '1990-01-01',  'admin123', 'Quản trị viên'),
    ('Nguyễn Văn A', '1985-05-15',  'nv123', 'Nhân viên'),
    ('Trần Thị B', '1992-08-20',  'bt123', 'Vận hành bảo trì');
END
GO

-- Step 6: Stored procedures
CREATE OR ALTER PROCEDURE sp_RegisterUser
    @FullName nvarchar(100),
    @DateOfBirth date,
    @Password nvarchar(255),
    @Position nvarchar(20)
AS
BEGIN
    INSERT INTO Users (FullName, DateOfBirth, Password, Position)
    VALUES (@FullName, @DateOfBirth, @Password, @Position)
END
GO

CREATE OR ALTER PROCEDURE sp_LoginUser
    @FullName nvarchar(100),
    @Password nvarchar(255)
AS
BEGIN
    SELECT UserID, FullName, Position, IsActive
    FROM Users
    WHERE FullName = @FullName AND Password = @Password AND IsActive = 1
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllUsers
AS
BEGIN
    SELECT UserID, FullName, DateOfBirth, Position, CreatedDate, IsActive
    FROM Users
    ORDER BY CreatedDate DESC
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateUser
    @UserID int,
    @FullName nvarchar(100),
    @DateOfBirth date,
    @Position nvarchar(20),
    @IsActive bit
AS
BEGIN
    UPDATE Users
    SET FullName = @FullName,
        DateOfBirth = @DateOfBirth,
        Position = @Position,
        IsActive = @IsActive
    WHERE UserID = @UserID
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteUser
    @UserID int
AS
BEGIN
    UPDATE Users
    SET IsActive = 0
    WHERE UserID = @UserID
END
GO

CREATE OR ALTER PROCEDURE sp_CheckFullnameExists
    @FullName nvarchar(100)
AS
BEGIN
    SELECT COUNT(*) AS Count
    FROM Users
    WHERE FullName = @FullName
END
GO
