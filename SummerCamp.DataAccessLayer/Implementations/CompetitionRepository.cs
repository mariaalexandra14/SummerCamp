using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
	public class CompetitionRepository : GenericRepository<Competition>, ICompetitionRepository
	{
		public CompetitionRepository(SummerCampDbContext context) : base(context)
		{
		}

		public IList<Team> GetAllTeams()
		{
			return context.Set<Team>()
				.ToList();
		}

		public int GetLastInsertedId()
		{
			return context.Set<Competition>().Max(item => item.Id);
		}

		public IList<CompetitionTeam> GetTeamsFromCompetion(int id)
		{
			return context.Set<CompetitionTeam>().Include(ct => ct.Team).Where(ct => ct.CompetitionId == id).ToList();
		}

		public IList<Competition> GetAllWithCompetitionTeams()
		{
			return context.Set<Competition>().Include(c => c.CompetitionTeams).ToList();
		}


		public Competition? GetCompetitionWithTeamsAndMatches(int id)
		{
			return context.Set<Competition>()
				.Include(c => c.CompetitionTeams)
				.ThenInclude(ct => ct.Team)
				.Include(c => c.CompetitionMatches)
				.FirstOrDefault(c => c.Id == id);
		}
	}
}
