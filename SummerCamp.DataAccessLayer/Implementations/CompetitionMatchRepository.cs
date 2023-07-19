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
            context.Set<TeamSponsor>().Where(x => x.TeamId == teamId).ExecuteDelete();
        }

        public void DeletePreviousScores()
        {
            var competitionTeams = context.Set<CompetitionTeam>().ToList();
            foreach (var competitionTeam in competitionTeams)
            {
                competitionTeam.TotalPoints = 0;
            }
        }

        public void UpdateTeamScore()
        {
            DeletePreviousScores();

            var competitionMatches = context.Set<CompetitionMatch>().ToList();

            foreach (var competitionMatch in competitionMatches)
            {
                int competitionId = competitionMatch.CompetitionId;

                var awayTeam = context.Set<CompetitionTeam>().Include(c => c.Team).FirstOrDefault(c => c.CompetitionId == competitionId && c.TeamId == competitionMatch.AwayTeamId);
                var homeTeam = context.Set<CompetitionTeam>().Include(c => c.Team).FirstOrDefault(c => c.CompetitionId == competitionId && c.TeamId == competitionMatch.HomeTeamId);

                if (competitionMatch.AwayTeamGoals == competitionMatch.HomeTeamGoals)
                {
                    awayTeam.TotalPoints += 1;
                    homeTeam.TotalPoints += 1;
                }
                else if (competitionMatch.HomeTeamGoals > competitionMatch.AwayTeamGoals)
                {
                    homeTeam.TotalPoints += 3;
                }
                else
                {
                    awayTeam.TotalPoints += 3;
                }
            }
        }
    }
}
