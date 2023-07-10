using System.ComponentModel.DataAnnotations;

namespace SummerCamp.Models
{
    public class CoachViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Va rugam sa adaugati un nume.")]
        public required string FullName { get; set; }

        public int? Age { get; set; }

        public string? Picture { get; set; }

        public int? CountryId { get; set; }
        public CountryViewModel Country { get; set; }
        public TeamViewModel Team { get; set; }

        public int? TeamId { get; set; }
    }
}
