using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(SummerCampDbContext context) : base(context)
        {
        }

        public IList<Team> GetAllWithPlayersCountriesAndCoach()
        {
            return context.Set<Team>()
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .ThenInclude(p => p.Country)
                .ToList();
        }

        public Team? GetTeamWithPlayersCountriesAndCoach(int id)
        {
            return context.Set<Team>()
                .Include(t => t.Coach)
                .Include(t => t.Players)
                .ThenInclude(p => p.Country)
                .FirstOrDefault(t => t.Id == id);
        }

    }
}
