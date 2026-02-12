USE EmployeeManagementSystemDB;
GO

CREATE TABLE Capabilities (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);
