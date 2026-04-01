USE EmployeeManagementSystemDB;
GO

CREATE PROCEDURE sp_AddCapabilityToEmployee
    @EmployeeId INT,
    @CapabilityId INT
AS
BEGIN
    INSERT INTO EmployeeCapabilities(EmployeeId, CapabilityId)
    VALUES(@EmployeeId, @CapabilityId);
END
GO

CREATE PROCEDURE sp_GetRolesByEmployeeId
    @EmployeeId INT
AS
BEGIN
    SELECT c.Name AS CapabilityName
    FROM EmployeeCapabilities ec
    JOIN Capabilities c ON ec.CapabilityId = c.Id
    WHERE ec.EmployeeId = @EmployeeId;
END
GO
