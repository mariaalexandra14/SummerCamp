using System.ComponentModel.DataAnnotations;

namespace SummerCamp.Models
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? Address { get; set; }

        [EnumDataType(typeof(Position))]
        public Position? Position { get; set; }

        public int? ShirtNumber
        { get; set; }
        public int? TeamId { get; set; }
        public int? CountryId { get; set; }
        public string? Picture { get; set; }

        public TeamViewModel Team { get; set; }

        public CountryViewModel Country { get; set; }
    }
}
