USE EmployeeManagementSystemDB;
GO

CREATE PROCEDURE sp_AddEmployee
    @Name NVARCHAR(100),
    @Department NVARCHAR(100),
    @Salary DECIMAL(18,2),
    @ManagerId INT = NULL
AS
BEGIN
    INSERT INTO Employees(Name, Department, Salary, ManagerId)
    VALUES(@Name, @Department, @Salary, @ManagerId);

    SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SELECT * FROM Employees;
END
GO

CREATE PROCEDURE sp_GetEmployeeById
    @Id INT
AS
BEGIN
    SELECT * FROM Employees WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_UpdateSalary
    @Id INT,
    @Salary DECIMAL(18,2)
AS
BEGIN
    UPDATE Employees
    SET Salary = @Salary
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_DeleteEmployee
    @Id INT
AS
BEGIN
    DELETE FROM Employees WHERE Id = @Id;
END
GO
