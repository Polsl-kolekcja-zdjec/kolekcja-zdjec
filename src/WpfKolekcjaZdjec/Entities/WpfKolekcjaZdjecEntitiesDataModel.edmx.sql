
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/28/2011 12:38:09
-- Generated from EDMX file: D:\Projects\Kolekcja-zdjec\src\WpfKolekcjaZdjec\Entities\WpfKolekcjaZdjecEntitiesDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WpfKolekcjaZdjecDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TagTag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TagSet] DROP CONSTRAINT [FK_TagTag];
GO
IF OBJECT_ID(N'[dbo].[FK_TagPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotoSet] DROP CONSTRAINT [FK_TagPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_SavedReportReportsHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReportsHistorySet] DROP CONSTRAINT [FK_SavedReportReportsHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_PhotoArchive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArchiveSet] DROP CONSTRAINT [FK_PhotoArchive];
GO
IF OBJECT_ID(N'[dbo].[FK_ArchivePhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotoSet] DROP CONSTRAINT [FK_ArchivePhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_BaseAttributesPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotoSet] DROP CONSTRAINT [FK_BaseAttributesPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_HddDirectory_inherits_Archive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArchiveSet_HddDirectory] DROP CONSTRAINT [FK_HddDirectory_inherits_Archive];
GO
IF OBJECT_ID(N'[dbo].[FK_Pendrive_inherits_Archive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArchiveSet_Pendrive] DROP CONSTRAINT [FK_Pendrive_inherits_Archive];
GO
IF OBJECT_ID(N'[dbo].[FK_CardReader_inherits_Archive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArchiveSet_CardReader] DROP CONSTRAINT [FK_CardReader_inherits_Archive];
GO
IF OBJECT_ID(N'[dbo].[FK_ReadOnlyArchive_inherits_Archive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArchiveSet_ReadOnlyArchive] DROP CONSTRAINT [FK_ReadOnlyArchive_inherits_Archive];
GO
IF OBJECT_ID(N'[dbo].[FK_DigitalCamera_inherits_Archive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArchiveSet_DigitalCamera] DROP CONSTRAINT [FK_DigitalCamera_inherits_Archive];
GO
IF OBJECT_ID(N'[dbo].[FK_StandardAttributes_inherits_BaseAttributes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BaseAttributesSet_StandardAttributes] DROP CONSTRAINT [FK_StandardAttributes_inherits_BaseAttributes];
GO
IF OBJECT_ID(N'[dbo].[FK_ExifParameters_inherits_BaseAttributes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BaseAttributesSet_ExifParameters] DROP CONSTRAINT [FK_ExifParameters_inherits_BaseAttributes];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TagSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TagSet];
GO
IF OBJECT_ID(N'[dbo].[PhotoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotoSet];
GO
IF OBJECT_ID(N'[dbo].[SavedReportSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SavedReportSet];
GO
IF OBJECT_ID(N'[dbo].[ReportsHistorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReportsHistorySet];
GO
IF OBJECT_ID(N'[dbo].[ArchiveSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArchiveSet];
GO
IF OBJECT_ID(N'[dbo].[BaseAttributesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseAttributesSet];
GO
IF OBJECT_ID(N'[dbo].[ArchiveSet_HddDirectory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArchiveSet_HddDirectory];
GO
IF OBJECT_ID(N'[dbo].[ArchiveSet_Pendrive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArchiveSet_Pendrive];
GO
IF OBJECT_ID(N'[dbo].[ArchiveSet_CardReader]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArchiveSet_CardReader];
GO
IF OBJECT_ID(N'[dbo].[ArchiveSet_ReadOnlyArchive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArchiveSet_ReadOnlyArchive];
GO
IF OBJECT_ID(N'[dbo].[ArchiveSet_DigitalCamera]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArchiveSet_DigitalCamera];
GO
IF OBJECT_ID(N'[dbo].[BaseAttributesSet_StandardAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseAttributesSet_StandardAttributes];
GO
IF OBJECT_ID(N'[dbo].[BaseAttributesSet_ExifParameters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseAttributesSet_ExifParameters];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TagSet'
CREATE TABLE [dbo].[TagSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IconPath] nvarchar(max)  NULL,
    [Tag_ParentTag_Hierarchy_Id] int  NOT NULL
);
GO

-- Creating table 'PhotoSet'
CREATE TABLE [dbo].[PhotoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Filename] varbinary(max)  NOT NULL,
    [FilePath] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NULL,
    [ThumbnailPath] varbinary(max)  NOT NULL,
    [Tag_Id] int  NOT NULL,
    [Archives_Id] int  NOT NULL,
    [Attributes_Id] int  NOT NULL
);
GO

-- Creating table 'SavedReportSet'
CREATE TABLE [dbo].[SavedReportSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [Query] nvarchar(max)  NOT NULL,
    [IsUserDefined] bit  NOT NULL
);
GO

-- Creating table 'ReportsHistorySet'
CREATE TABLE [dbo].[ReportsHistorySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [UseDate] datetime  NOT NULL,
    [SavedReport_Id] int  NOT NULL
);
GO

-- Creating table 'ArchiveSet'
CREATE TABLE [dbo].[ArchiveSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VolumeName] nvarchar(max)  NOT NULL,
    [Photo_Id] int  NOT NULL
);
GO

-- Creating table 'BaseAttributesSet'
CREATE TABLE [dbo].[BaseAttributesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [BitsPerPixel] smallint  NOT NULL,
    [Hash] nvarchar(max)  NOT NULL,
    [Height] bigint  NOT NULL,
    [Width] bigint  NOT NULL,
    [Rate] smallint  NOT NULL
);
GO

-- Creating table 'ArchiveSet_HddDirectory'
CREATE TABLE [dbo].[ArchiveSet_HddDirectory] (
    [Capacity] bigint  NOT NULL,
    [IsExternal] bit  NOT NULL,
    [DeviceId] nvarchar(max)  NOT NULL,
    [HddLetter] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ArchiveSet_Pendrive'
CREATE TABLE [dbo].[ArchiveSet_Pendrive] (
    [Capacity] bigint  NOT NULL,
    [HddLetter] nvarchar(max)  NOT NULL,
    [DeviceId] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ArchiveSet_CardReader'
CREATE TABLE [dbo].[ArchiveSet_CardReader] (
    [Capacity] bigint  NOT NULL,
    [HddLetter] nvarchar(max)  NOT NULL,
    [DeviceId] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ArchiveSet_ReadOnlyArchive'
CREATE TABLE [dbo].[ArchiveSet_ReadOnlyArchive] (
    [Capacity] bigint  NOT NULL,
    [DiscLetter] nvarchar(max)  NOT NULL,
    [DiscType] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ArchiveSet_DigitalCamera'
CREATE TABLE [dbo].[ArchiveSet_DigitalCamera] (
    [DeviceId] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'BaseAttributesSet_StandardAttributes'
CREATE TABLE [dbo].[BaseAttributesSet_StandardAttributes] (
    [AdditionalParametersXML] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'BaseAttributesSet_ExifParameters'
CREATE TABLE [dbo].[BaseAttributesSet_ExifParameters] (
    [AdditionalParametersXML] nvarchar(max)  NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TagSet'
ALTER TABLE [dbo].[TagSet]
ADD CONSTRAINT [PK_TagSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [PK_PhotoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SavedReportSet'
ALTER TABLE [dbo].[SavedReportSet]
ADD CONSTRAINT [PK_SavedReportSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReportsHistorySet'
ALTER TABLE [dbo].[ReportsHistorySet]
ADD CONSTRAINT [PK_ReportsHistorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArchiveSet'
ALTER TABLE [dbo].[ArchiveSet]
ADD CONSTRAINT [PK_ArchiveSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BaseAttributesSet'
ALTER TABLE [dbo].[BaseAttributesSet]
ADD CONSTRAINT [PK_BaseAttributesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArchiveSet_HddDirectory'
ALTER TABLE [dbo].[ArchiveSet_HddDirectory]
ADD CONSTRAINT [PK_ArchiveSet_HddDirectory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArchiveSet_Pendrive'
ALTER TABLE [dbo].[ArchiveSet_Pendrive]
ADD CONSTRAINT [PK_ArchiveSet_Pendrive]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArchiveSet_CardReader'
ALTER TABLE [dbo].[ArchiveSet_CardReader]
ADD CONSTRAINT [PK_ArchiveSet_CardReader]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArchiveSet_ReadOnlyArchive'
ALTER TABLE [dbo].[ArchiveSet_ReadOnlyArchive]
ADD CONSTRAINT [PK_ArchiveSet_ReadOnlyArchive]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArchiveSet_DigitalCamera'
ALTER TABLE [dbo].[ArchiveSet_DigitalCamera]
ADD CONSTRAINT [PK_ArchiveSet_DigitalCamera]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BaseAttributesSet_StandardAttributes'
ALTER TABLE [dbo].[BaseAttributesSet_StandardAttributes]
ADD CONSTRAINT [PK_BaseAttributesSet_StandardAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BaseAttributesSet_ExifParameters'
ALTER TABLE [dbo].[BaseAttributesSet_ExifParameters]
ADD CONSTRAINT [PK_BaseAttributesSet_ExifParameters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Tag_ParentTag_Hierarchy_Id] in table 'TagSet'
ALTER TABLE [dbo].[TagSet]
ADD CONSTRAINT [FK_TagTag]
    FOREIGN KEY ([Tag_ParentTag_Hierarchy_Id])
    REFERENCES [dbo].[TagSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TagTag'
CREATE INDEX [IX_FK_TagTag]
ON [dbo].[TagSet]
    ([Tag_ParentTag_Hierarchy_Id]);
GO

-- Creating foreign key on [Tag_Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [FK_TagPhoto]
    FOREIGN KEY ([Tag_Id])
    REFERENCES [dbo].[TagSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TagPhoto'
CREATE INDEX [IX_FK_TagPhoto]
ON [dbo].[PhotoSet]
    ([Tag_Id]);
GO

-- Creating foreign key on [SavedReport_Id] in table 'ReportsHistorySet'
ALTER TABLE [dbo].[ReportsHistorySet]
ADD CONSTRAINT [FK_SavedReportReportsHistory]
    FOREIGN KEY ([SavedReport_Id])
    REFERENCES [dbo].[SavedReportSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SavedReportReportsHistory'
CREATE INDEX [IX_FK_SavedReportReportsHistory]
ON [dbo].[ReportsHistorySet]
    ([SavedReport_Id]);
GO

-- Creating foreign key on [Photo_Id] in table 'ArchiveSet'
ALTER TABLE [dbo].[ArchiveSet]
ADD CONSTRAINT [FK_PhotoArchive]
    FOREIGN KEY ([Photo_Id])
    REFERENCES [dbo].[PhotoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhotoArchive'
CREATE INDEX [IX_FK_PhotoArchive]
ON [dbo].[ArchiveSet]
    ([Photo_Id]);
GO

-- Creating foreign key on [Archives_Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [FK_ArchivePhoto]
    FOREIGN KEY ([Archives_Id])
    REFERENCES [dbo].[ArchiveSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArchivePhoto'
CREATE INDEX [IX_FK_ArchivePhoto]
ON [dbo].[PhotoSet]
    ([Archives_Id]);
GO

-- Creating foreign key on [Attributes_Id] in table 'PhotoSet'
ALTER TABLE [dbo].[PhotoSet]
ADD CONSTRAINT [FK_BaseAttributesPhoto]
    FOREIGN KEY ([Attributes_Id])
    REFERENCES [dbo].[BaseAttributesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BaseAttributesPhoto'
CREATE INDEX [IX_FK_BaseAttributesPhoto]
ON [dbo].[PhotoSet]
    ([Attributes_Id]);
GO

-- Creating foreign key on [Id] in table 'ArchiveSet_HddDirectory'
ALTER TABLE [dbo].[ArchiveSet_HddDirectory]
ADD CONSTRAINT [FK_HddDirectory_inherits_Archive]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ArchiveSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ArchiveSet_Pendrive'
ALTER TABLE [dbo].[ArchiveSet_Pendrive]
ADD CONSTRAINT [FK_Pendrive_inherits_Archive]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ArchiveSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ArchiveSet_CardReader'
ALTER TABLE [dbo].[ArchiveSet_CardReader]
ADD CONSTRAINT [FK_CardReader_inherits_Archive]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ArchiveSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ArchiveSet_ReadOnlyArchive'
ALTER TABLE [dbo].[ArchiveSet_ReadOnlyArchive]
ADD CONSTRAINT [FK_ReadOnlyArchive_inherits_Archive]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ArchiveSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ArchiveSet_DigitalCamera'
ALTER TABLE [dbo].[ArchiveSet_DigitalCamera]
ADD CONSTRAINT [FK_DigitalCamera_inherits_Archive]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ArchiveSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'BaseAttributesSet_StandardAttributes'
ALTER TABLE [dbo].[BaseAttributesSet_StandardAttributes]
ADD CONSTRAINT [FK_StandardAttributes_inherits_BaseAttributes]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[BaseAttributesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'BaseAttributesSet_ExifParameters'
ALTER TABLE [dbo].[BaseAttributesSet_ExifParameters]
ADD CONSTRAINT [FK_ExifParameters_inherits_BaseAttributes]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[BaseAttributesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------