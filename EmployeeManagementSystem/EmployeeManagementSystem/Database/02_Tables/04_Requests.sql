USE EmployeeManagementSystemDB;
GO

CREATE TABLE Requests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT,
    Description NVARCHAR(500),
    Status NVARCHAR(50),
    FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
);
