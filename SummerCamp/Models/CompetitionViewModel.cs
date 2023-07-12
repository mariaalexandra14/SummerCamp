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

        public IEnumerable<TeamViewModel> AvailableTeams { get; set; }
        public List<int> SelectedTeamsIds { get; set; }
        public IEnumerable<TeamViewModel> Teams { get; set; }

        public List<CompetitionTeamViewModel> CompetitionTeams { get; set; } = new List<CompetitionTeamViewModel>();
        public IEnumerable<CompetitionMatchViewModel> CompetitionMatches { get; set; }

    }
}
