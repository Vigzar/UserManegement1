CREATE DATABASE UserManagement;
GO

USE UserManagement;
GO

CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);


CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);


CREATE TABLE Tasks (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Priority NVARCHAR(20) NOT NULL,
    DueDate DATETIME NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    AssignedTo INT NOT NULL,
    FOREIGN KEY (AssignedTo) REFERENCES Users(UserId)
);

CREATE TABLE TimeEntries (
    EntryId INT IDENTITY(1,1) PRIMARY KEY,
    Date DATETIME NOT NULL,
    Project NVARCHAR(100) NOT NULL,
    Hours DECIMAL(5,2) NOT NULL,
    Description NVARCHAR(500),
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE AuditLog (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    ActionType NVARCHAR(50) NOT NULL,
    Details NVARCHAR(500) NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

INSERT INTO Roles (RoleName) VALUES
('Admin'),
('Manager'),
('Employee');

INSERT INTO Users (Username, Password, RoleId) VALUES
('admin', 'admin123', 1),
('manager', 'manager123', 2),
('employee', 'employee123', 3);