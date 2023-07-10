CREATE TABLE [dbo].[Coach] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FullName]  VARCHAR (255) NULL,
    [Age]       INT           NULL,
    [Picture]   VARCHAR (255) NULL,
    [CountryId] INT           NULL,
    [TeamId]    INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
);






GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Coach_TeamId]
    ON [dbo].[Coach]([TeamId] ASC) WHERE ([TeamId] IS NOT NULL);

