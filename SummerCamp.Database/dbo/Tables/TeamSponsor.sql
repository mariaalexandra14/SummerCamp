CREATE TABLE [dbo].[TeamSponsor] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [SponsorId] INT NULL,
    [TeamId]    INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SponsorId]) REFERENCES [dbo].[Sponsor] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
);

