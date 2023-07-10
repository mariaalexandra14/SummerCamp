namespace SummerCamp.Models
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string? NickName { get; set; }

        public string? Name { get; set; }

        public ICollection<PlayerViewModel>? Players { get; set; }
    }
}
