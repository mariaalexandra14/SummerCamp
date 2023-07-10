using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(SummerCampDbContext context) : base(context)
        {
        }

        public IList<Player> GetAllWithTeamAndCountry()
        {
            return _context.Set<Player>()
                .Include(p => p.Team)
                .Include(p => p.Country)
                .ToList();
        }

    }
}
