using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Coach>
    {
        IList<Coach> GetAllWithCountries();
        Coach? GetWithCountryAndTeam(int id);
        List<Coach> GetUnassignedCoaches();
    }
}
