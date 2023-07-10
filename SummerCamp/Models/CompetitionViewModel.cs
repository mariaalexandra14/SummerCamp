namespace SummerCamp.Models
{
    public class CompetitionViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? NumberOfTeams { get; set; }

        public string? Address { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? SponsorId { get; set; }

        public SponsorViewModel? Sponsor { get; set; }
    }
}
