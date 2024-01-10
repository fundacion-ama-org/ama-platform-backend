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


-- Migración para la entidad Donaciones
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_Donations')
BEGIN
    -- Create Donations table
    CREATE TABLE [Donations] (
        [Id_donation] int NOT NULL IDENTITY,
        [Donation_name] nvarchar(100) NOT NULL,
        [Donation_type] nvarchar(50) NOT NULL,
        [Value] decimal(18, 2) NOT NULL,
        [Total] decimal(18, 2) NOT NULL,
        [Donor] nvarchar(100) NOT NULL,
        [Donation_date] datetime2 NOT NULL,
        CONSTRAINT [PK_Donations] PRIMARY KEY ([Id_donation])
    );

    -- Insert migration data
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240105221726_Donations', N'3.1.32');
END;

GO

-- Migración para la entidad ConsultaDonaciones
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_ConsultaDonaciones')
BEGIN
    -- Create ConsultaDonaciones table
    CREATE TABLE [ConsultaDonaciones] (
        [Donation_name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_ConsultaDonaciones] PRIMARY KEY ([Donation_name])
    );

    -- Insert migration data
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240105221726_ConsultaDonaciones', N'3.1.32');
END;

GO

-- Migración para la entidad ModificarDonaciones
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_ModificarDonaciones')
BEGIN
    -- Create ModificarDonaciones table
    CREATE TABLE [ModificarDonaciones] (
        -- Define attributes that can be modified
        [Id_donation] int NOT NULL,
        [Price] decimal(18, 2) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Donor] nvarchar(100) NOT NULL,
        [Donation_type] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_ModificarDonaciones] PRIMARY KEY ([Id_donation])
    );

    -- Insert migration data
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240105221726_ModificarDonaciones', N'3.1.32');
END;

GO

-- Migración para la entidad CrearDonante
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240105221726_CrearDonante')
BEGIN
    -- Create CrearDonante table
    CREATE TABLE [CrearDonante] (
        [Id_donor] int NOT NULL IDENTITY,
        [Donor_name] nvarchar(100) NOT NULL,
        [Donation_type] nvarchar(50) NOT NULL,
        [Donation_value] decimal(18, 2) NOT NULL,
        [Donor_cellphone] nvarchar(20) NOT NULL,
        [Volunteer] bit NOT NULL,
        CONSTRAINT [PK_CrearDonante] PRIMARY KEY ([Id_donor])
    );

    -- Insert migration data
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240105221726_CrearDonante', N'3.1.32');
END;

GO
