using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICompetitionMatchRepository : IGenericRepository<CompetitionMatch>
    {
        IList<CompetitionMatch> GetCompetitionMatches(int competitonId);
        IList<CompetitionTeam> GetCompetitionTeams(int competitonId);
        void DeleteTeamMatch(int teamId);
        void UpdateTeamScore();
    }
}
