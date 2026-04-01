USE EmployeeManagementSystemDB;
GO

CREATE TABLE EmployeeCapabilities (
    EmployeeId INT,
    CapabilityId INT,
    PRIMARY KEY(EmployeeId, CapabilityId),
    FOREIGN KEY(EmployeeId) REFERENCES Employees(Id),
    FOREIGN KEY(CapabilityId) REFERENCES Capabilities(Id)
);
