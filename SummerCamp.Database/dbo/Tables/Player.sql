CREATE TABLE [dbo].[Player] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [BirthDate]   DATE          NULL,
    [Address]     VARCHAR (255) NULL,
    [Position]    INT           NULL,
    [ShirtNumber] INT           NULL,
    [TeamId]      INT           NULL,
    [CountryId]   INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [UQ_Team_Name] UNIQUE NONCLUSTERED ([TeamId] ASC, [ShirtNumber] ASC)
);

