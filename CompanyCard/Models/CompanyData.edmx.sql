
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/13/2019 03:13:44
-- Generated from EDMX file: C:\Users\Kaushal\source\repos\CompanyCard\CompanyCard\Models\CompanyData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CompanyDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CompanyEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_CompanyEmployee];
GO
IF OBJECT_ID(N'[dbo].[FK_Logins_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logins] DROP CONSTRAINT [FK_Logins_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Shifts_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shifts] DROP CONSTRAINT [FK_Shifts_Employees];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[Shifts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Shifts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [CompanyId] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(max)  NOT NULL,
    [CompanyAddress] nvarchar(max)  NOT NULL,
    [ContactNo] nvarchar(max)  NOT NULL,
    [Website] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [EmployeeName] nvarchar(max)  NOT NULL,
    [EmployeePhoneNo] nvarchar(max)  NOT NULL,
    [EmployeeAddress] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [CompanyCompanyId] int  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [LoginsId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Admin] nvarchar(max)  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'Shifts'
CREATE TABLE [dbo].[Shifts] (
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [workedHours] float  NOT NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CompanyId] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([CompanyId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [LoginsId] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([LoginsId] ASC);
GO

-- Creating primary key on [StartTime], [EndTime], [workedHours], [EmployeeId] in table 'Shifts'
ALTER TABLE [dbo].[Shifts]
ADD CONSTRAINT [PK_Shifts]
    PRIMARY KEY CLUSTERED ([StartTime], [EndTime], [workedHours], [EmployeeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyCompanyId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_CompanyEmployee]
    FOREIGN KEY ([CompanyCompanyId])
    REFERENCES [dbo].[Companies]
        ([CompanyId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyEmployee'
CREATE INDEX [IX_FK_CompanyEmployee]
ON [dbo].[Employees]
    ([CompanyCompanyId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [FK_Logins_Employees]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Logins_Employees'
CREATE INDEX [IX_FK_Logins_Employees]
ON [dbo].[Logins]
    ([EmployeeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'Shifts'
ALTER TABLE [dbo].[Shifts]
ADD CONSTRAINT [FK_Shifts_Employees]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employees]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Shifts_Employees'
CREATE INDEX [IX_FK_Shifts_Employees]
ON [dbo].[Shifts]
    ([EmployeeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------