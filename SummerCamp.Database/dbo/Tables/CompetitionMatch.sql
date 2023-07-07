﻿CREATE TABLE [dbo].[CompetitionMatch] (
    [Id]            INT NOT NULL,
    [CompetitionId] INT NOT NULL,
    [HomeTeamId]    INT NOT NULL,
    [AwayTeamId]    INT NOT NULL,
    [HomeTeamGoals] INT NOT NULL,
    [AwayTeamGoals] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team] ([Id]),
    FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]),
    FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team] ([Id])
);

