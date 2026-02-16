USE EmployeeManagementSystemDB;
GO

CREATE TABLE Capabilities (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

INSERT INTO Capabilities(Name) VALUES ('IApprover');
INSERT INTO Capabilities(Name) VALUES ('ITeamSupervisor');
INSERT INTO Capabilities(Name) VALUES ('ICodeContributer');
INSERT INTO Capabilities(Name) VALUES ('ITestExecuter');
