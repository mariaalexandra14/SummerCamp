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

        public IList<CompetitionTeam> GetCompetitionTeams(int competitonId)
        {
            return context.Set<CompetitionTeam>()
                .Include(x => x.Team)
                .Where(x => x.CompetitionId == competitonId)
                .ToList();
        }

        public void DeleteTeamMatch(int teamId)
        {
            context.Set<CompetitionMatch>().Where(x => x.AwayTeamId == teamId || x.HomeTeamId == teamId).ExecuteDelete();
            context.Set<CompetitionTeam>().Where(x => x.TeamId == teamId).ExecuteDelete();
            //context.Set<Player>().Where(x => x.TeamId == teamId).ExecuteDelete();
            context.Set<TeamSponsor>().Where(x => x.TeamId == teamId).ExecuteDelete();
        }
    }
}
