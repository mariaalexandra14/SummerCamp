namespace SummerCamp.Models
{
    public class CompetitionTeamViewModel
    {
        public int Id { get; set; }

        public int? CompetitionId { get; set; }

        public int? TeamId { get; set; }

        public int TotalPoints { get; set; }

        public CompetitionViewModel? Competition { get; set; }

        public TeamViewModel? Team { get; set; }
    }
}
