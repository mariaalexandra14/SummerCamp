using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public class CompetitionTeamRepository : GenericRepository<CompetitionTeam>, ICompetitionTeamRepository
    {
        public CompetitionTeamRepository(SummerCampDbContext context) : base(context)
        {
        }

        public IList<Team> GetTeamsFromCompetition(int competitionId)
        {
            return context.CompetitionTeams
                .Where(ct => ct.CompetitionId == competitionId)
                .Select(ct => ct.Team!)
                .ToList();
        }

        public bool RemoveRange(List<CompetitionTeam> competitionTeams)
        {
            context.CompetitionTeams.RemoveRange(competitionTeams);
            return context.SaveChanges() > 0;
        }
    }
}
