using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CoachRepository : GenericRepository<Coach>, ICoachRepository
    {
        public CoachRepository(SummerCampDbContext context) : base(context)
        {
        }

        public IList<Coach> GetAllWithCountriesAndTeam()
        {
            return _context.Set<Coach>().Include(c => c.Country).Include(c => c.Team).ToList();
        }
    }
}
