using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        IList<Player> GetAllWithTeamAndCountry();
        bool IsShirtNumberUnique(int? teamId, int? shirtNumber);
    }
}
