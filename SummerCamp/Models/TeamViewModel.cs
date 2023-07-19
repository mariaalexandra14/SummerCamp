namespace SummerCamp.Models
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string? NickName { get; set; }

        public string? Name { get; set; }

        public CoachViewModel Coach { get; set; }
        public int? CoachId { get; set; }
        public IEnumerable<SponsorViewModel> AvailableSponsors { get; set; }
        public List<int> SelectedSponsorsIds { get; set; } = new List<int>();

        public ICollection<PlayerViewModel>? Players { get; set; }
        public List<TeamSponsorViewModel> TeamsSponsors { get; set; } = new List<TeamSponsorViewModel>();
    }
}
