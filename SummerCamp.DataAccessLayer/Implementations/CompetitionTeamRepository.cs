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

        public void UpdateScore(CompetitionMatch competitionMatch, int homeTeamScore, int awayTeamScore)
        {
            int homeTeamId = competitionMatch.HomeTeamId;
            int awayTeamId = competitionMatch.AwayTeamId;
            var homeTeam = context.CompetitionTeams.Where(x => x.TeamId == homeTeamId && x.CompetitionId == competitionMatch.CompetitionId).FirstOrDefault();
            var awayTeam = context.CompetitionTeams.Where(x => x.TeamId == awayTeamId && x.CompetitionId == competitionMatch.CompetitionId).FirstOrDefault();

            if (homeTeamScore != competitionMatch.HomeTeamGoals || awayTeamScore != competitionMatch.AwayTeamGoals)
            {
                var winnerTeam = GetWinnerTeam(competitionMatch);
                if (winnerTeam == null)
                {
                    homeTeam.TotalPoints -= 1;
                    awayTeam.TotalPoints -= 1;


                }
            }

            if (competitionMatch.HomeTeamGoals == competitionMatch.AwayTeamGoals)
            {
                homeTeam.TotalPoints += homeTeamScore;
                awayTeam.TotalPoints += awayTeamScore;
            }
            else if (competitionMatch.HomeTeamGoals > competitionMatch.AwayTeamGoals)
            {
                homeTeam.TotalPoints += homeTeamScore;
            }
            else
            {
                awayTeam.TotalPoints += awayTeamScore;
            }
        }

        private Team? GetWinnerTeam(CompetitionMatch competitionMatch)
        {
            if (competitionMatch.AwayTeamGoals > competitionMatch.HomeTeamGoals)
            {
                return competitionMatch.AwayTeam;
            }
            else if (competitionMatch.HomeTeamGoals > competitionMatch.AwayTeamGoals)
            {
                return competitionMatch.HomeTeam;
            }

            return null;
        }

        public bool RemoveRange(List<CompetitionTeam> competitionTeams)
        {
            context.CompetitionTeams.RemoveRange(competitionTeams);
            return context.SaveChanges() > 0;
        }
    }
}
