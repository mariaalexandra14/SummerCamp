using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICompetitionRepository : IGenericRepository<Competition>
    {
        public IList<Team> GetAllTeams();
        int GetLastInsertedId();
        IList<CompetitionTeam> GetTeamsFromCompetion(int id);
        IList<Competition> GetAllWithCompetitionTeams();
        Competition? GetCompetitionWithTeamsAndMatches(int id);
        Competition? GetNextCompetition();
    }
}
