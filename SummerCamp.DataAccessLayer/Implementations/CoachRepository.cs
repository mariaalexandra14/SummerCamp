using Microsoft.EntityFrameworkCore;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;

namespace SummerCamp.DataAccessLayer.Implementations
{
	public class CoachRepository : GenericRepository<Coach>, ICoachRepository
	{
		public CoachRepository(SummerCampDbContext context) : base(context)
		{
		}

		public IList<Coach> GetAllWithCountries()
		{
			return context.Set<Coach>().Include(c => c.Country).ToList();
		}

		public Coach? GetWithCountryAndTeam(int id)
		{
			return context.Set<Coach>().Include(c => c.Country).SingleOrDefault(c => c.Id == id);
		}

		public List<Coach> GetUnassignedCoaches()
		{
			var unassignedCoaches = context.Set<Coach>()
				.Where(coach => !context.Set<Team>().Any(team => team.CoachId == coach.Id))
				.ToList();

			return unassignedCoaches;
		}
	}
}
