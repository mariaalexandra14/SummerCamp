using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        IList<Team> GetAllWithPlayersCountriesAndCoach();
        Team? GetTeamWithPlayersCountriesAndCoach(int id);

    }
}
