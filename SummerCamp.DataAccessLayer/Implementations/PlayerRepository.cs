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
            return context.Set<Player>()
                .Include(p => p.Team)
                .Include(p => p.Country)
                .ToList();
        }

        public bool IsShirtNumberUnique(int? teamId, int? shirtNumber)
        {
            return context.Players.Where(x => x.TeamId == teamId && x.ShirtNumber == shirtNumber).Count() == 0;
        }

    }
}
