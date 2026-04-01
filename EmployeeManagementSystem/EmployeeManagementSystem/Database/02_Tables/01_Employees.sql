USE EmployeeManagementSystemDB;
GO

CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Department NVARCHAR(100) NOT NULL,
    Salary DECIMAL(18,2) NOT NULL,
    ManagerId INT NULL
);
