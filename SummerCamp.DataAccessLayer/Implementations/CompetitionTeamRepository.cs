using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CompetitionTeamRepository : GenericRepository<CompetitionTeam>, ICompetitionTeamRepository
    {
        public CompetitionTeamRepository(SummerCampDbContext context) : base(context)
        {
        }

    }
}
