CREATE TABLE [dbo].[CompetitionTeam] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [CompetitionId] INT NULL,
    [TeamId]        INT NULL,
    [TotalPoints]   INT DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id]) ON DELETE SET NULL
);

