using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICompetitionTeamRepository : IGenericRepository<CompetitionTeam>
    {
        IList<Team> GetTeamsFromCompetition(int competitionId);
        void UpdateScore(CompetitionMatch competitionMatch, int homeTeamScore, int awayTeamScore);
        bool RemoveRange(List<CompetitionTeam> competitionTeams);

    }
}
