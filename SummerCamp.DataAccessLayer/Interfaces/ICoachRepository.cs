using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface ICoachRepository : IGenericRepository<Coach>
    {
        IList<Coach> GetAllWithCountriesAndTeam();
        Coach? GetWithCountryAndTeam(int id);
    }
}
