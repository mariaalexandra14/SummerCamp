namespace SummerCamp.Models
{
    public class CompetitionMatchViewModel
    {
        public int Id { get; set; }

        public int CompetitionId { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public TeamViewModel AwayTeam { get; set; }

        public CompetitionViewModel Competition { get; set; }

        public TeamViewModel HomeTeam { get; set; }
    }
}
