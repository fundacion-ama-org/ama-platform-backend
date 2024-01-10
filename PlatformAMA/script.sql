IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE TABLE [ActivityTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_ActivityTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE TABLE [IdentificationTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_IdentificationTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE TABLE [Persons] (
        [Id] int NOT NULL IDENTITY,
        [IdentificationTypeId] int NOT NULL,
        [FirstName] nvarchar(50) NOT NULL,
        [SecondName] nvarchar(max) NULL,
        [LastName] nvarchar(50) NOT NULL,
        [SecondLastName] nvarchar(max) NULL,
        [FullName] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Persons_IdentificationTypes_IdentificationTypeId] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [IdentificationTypes] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE TABLE [Donors] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Donors] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Donors_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE TABLE [Volunteers] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [IsActive] bit NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [Address] nvarchar(100) NOT NULL,
        [Available] bit NOT NULL,
        [ActivityTypeId] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        CONSTRAINT [PK_Volunteers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Volunteers_ActivityTypes_ActivityTypeId] FOREIGN KEY ([ActivityTypeId]) REFERENCES [ActivityTypes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Volunteers_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE INDEX [IX_Donors_PersonId] ON [Donors] ([PersonId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE INDEX [IX_Persons_IdentificationTypeId] ON [Persons] ([IdentificationTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE INDEX [IX_Volunteers_ActivityTypeId] ON [Volunteers] ([ActivityTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    CREATE INDEX [IX_Volunteers_PersonId] ON [Volunteers] ([PersonId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240106212635_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240106212635_Initial', N'3.1.32');
END;

GO

-- ----------
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_Initial')
BEGIN
    CREATE TABLE [Brigades] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(max) NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
[UpdatedAt] datetime2 NOT NULL,
[CreatedBy] datetime2 NOT NULL,
[UpdatedBy] datetime2 NOT NULL,        
CONSTRAINT [PK_ Brigades] PRIMARY KEY ([Id]),
CONSTRAINT [FK_ Brigades_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;

GO


IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_Initial')
BEGIN
    CREATE TABLE [BeneficiaryTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [CreatedBy] datetime2 NOT NULL,
        [UpdatedBy] datetime2 NOT NULL,
        CONSTRAINT [PK_BeneficiaryTypes] PRIMARY KEY ([Id])
    );
END;

GO

BEGIN
    CREATE TABLE [Beneficiaries] (
        [Id] int NOT NULL IDENTITY,
        [PersonId] int NOT NULL,
        [IsActive] bit NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [UpdatedAt] datetime2 NOT NULL,
        [CreatedBy] datetime2 NOT NULL,
        [UpdatedBy] datetime2 NOT NULL,
        CONSTRAINT [PK_ Beneficiaries] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ Beneficiaries _BeneficiaryTypes_ BeneficiaryTypesId] FOREIGN KEY ([BeneficiaryTypes]) REFERENCES [BeneficiaryTypes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ Beneficiaries _Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE CASCADE
    );
END;

GO

-- IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_Initial')
--BEGIN
--    CREATE TABLE [BrigadeBeneficiaries] (
--        [Id] int NOT NULL IDENTITY,
--        [BrigadeId] int NOT NULL,
--        [BeneficiaryId] int NOT NULL,
--        [BeneficiaryTypeId] int NOT NULL,
--        [Description] int NOT NULL,
--        [IsActive] bit NOT NULL,
--        [CreatedAt] datetime2 NOT NULL,
--        [UpdatedAt] datetime2 NOT NULL,
--        [CreatedBy] datetime2 NOT NULL,
--        [UpdatedBy] datetime2 NOT NULL,
--        CONSTRAINT [PK_ BrigadeBeneficiaries] PRIMARY KEY ([Id]),
--        CONSTRAINT [FK_ BrigadeBeneficiaries _BeneficiaryTypes_ BeneficiaryTypesId] FOREIGN KEY ([BeneficiaryTypes]) REFERENCES [BeneficiaryTypes] ([Id]) ON DELETE CASCADE, 
--        CONSTRAINT [FK_ BrigadeBeneficiaries _Beneficiaries_ BeneficiaryId] FOREIGN KEY ([BeneficiaryId]) REFERENCES [Beneficiaries] ([Id]) ON DELETE CASCADE
-- );
--END;

--GO
----------

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_Initial')
BEGIN
    CREATE INDEX [IX_Beneficiaries_BeneficiaryTypeId] ON [Beneficiaries] ([BeneficiaryTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_Initial')
BEGIN
    CREATE INDEX [IX_Beneficiaries_PersonId] ON [Beneficiaries] ([PersonId]);
END;

GO

