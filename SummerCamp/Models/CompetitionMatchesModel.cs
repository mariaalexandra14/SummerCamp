namespace SummerCamp.Models
{
	public class CompetitionMatchesModel
	{
		public List<CompetitionMatchViewModel> CompetitionMatches { get; set; } = new List<CompetitionMatchViewModel>();
		public int CompetitionId { get; set; }
	}
}
