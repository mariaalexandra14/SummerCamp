using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CompetitionMatchRepository : GenericRepository<CompetitionMatch>, ICompetitionMatchRepository
    {
        public CompetitionMatchRepository(SummerCampDbContext context) : base(context)
        {
        }

        public IList<CompetitionMatch> GetCompetitionMatches(int competitonId)
        {
            return context.Set<CompetitionMatch>()
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Where(x => x.CompetitionId == competitonId)
                .ToList();

        }
    }
}
