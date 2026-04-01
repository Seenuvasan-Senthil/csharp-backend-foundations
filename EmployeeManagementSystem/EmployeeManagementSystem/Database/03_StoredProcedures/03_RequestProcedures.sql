USE EmployeeManagementSystemDB;
GO

CREATE PROCEDURE sp_CreateRequest
    @EmployeeId INT,
    @Description NVARCHAR(500)
AS
BEGIN
    INSERT INTO Requests(EmployeeId, Description, Status)
    VALUES(@EmployeeId, @Description, 'Pending');

    SELECT SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE sp_GetAllRequests
AS
BEGIN
    SELECT * FROM Requests;
END
GO

CREATE PROCEDURE sp_ApproveRequest
    @Id INT
AS
BEGIN
    UPDATE Requests SET Status = 'Approved'
    WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_RejectRequest
    @Id INT
AS
BEGIN
    UPDATE Requests SET Status = 'Rejected'
    WHERE Id = @Id;
END
GO
