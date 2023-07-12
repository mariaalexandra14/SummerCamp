namespace SummerCamp.Models
{
    public class CompetitionOverviewModel
    {
        public CompetitionViewModel Competition { get; set; }
        public IList<CompetitionTeamViewModel> CompetitionTeams { get; set; }
    }
}
