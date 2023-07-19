namespace SummerCamp.Models
{
    public class TeamSponsorViewModel
    {
        public int Id { get; set; }

        public int? SponsorId { get; set; }

        public int? TeamId { get; set; }

        public SponsorViewModel Sponsor { get; set; }

        public TeamViewModel Team { get; set; }
    }
}
