using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICompetitionTeamRepository : IGenericRepository<CompetitionTeam>
    {
        IList<Team> GetTeamsFromCompetition(int competitionId);
        bool RemoveRange(List<CompetitionTeam> competitionTeams);

    }
}
