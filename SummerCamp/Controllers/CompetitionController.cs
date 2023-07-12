using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{

	[Route("[controller]/{action}")]
	public class CompetitionController : Controller
	{
		private readonly ICompetitionRepository _competitionRepository;
		private readonly ICompetitionTeamRepository _competitionTeamRepository;
		private readonly ICompetitionMatchRepository _competitionMatchRepository;
		private readonly ITeamRepository _teamRepository;
		private readonly IMapper _mapper;

		public CompetitionController(ICompetitionRepository competitionRepository, ICompetitionTeamRepository competitionTeamRepository, ICompetitionMatchRepository competitionMatchRepository, ITeamRepository teamRepository, IMapper mapper)
		{
			_competitionRepository = competitionRepository;
			_competitionTeamRepository = competitionTeamRepository;
			_competitionMatchRepository = competitionMatchRepository;
			_teamRepository = teamRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var competitions = _competitionRepository.GetAll();

			return View(_mapper.Map<IList<CompetitionViewModel>>(competitions));
		}

		// GET: Competitions/Create
		public IActionResult Create()
		{
			ViewData["Teams"] = new SelectList(_teamRepository.GetAll(), "Id", "Name");

			var teams = _teamRepository.GetAll();
			var result = new CompetitionViewModel()
			{
				AvailableTeams = _mapper.Map<IList<TeamViewModel>>(teams)
			};

			return View(result);
		}

		// POST: Competitions/Create

		[HttpPost]
		public JsonResult Create([FromBody] CompetitionViewModel competitionViewModel)
		{
			foreach (var teamId in competitionViewModel.SelectedTeamsIds)
			{
				var competitionTeamViewModel = new CompetitionTeamViewModel()
				{
					TotalPoints = 0,
					TeamId = teamId
				};
				competitionViewModel.CompetitionTeams.Add(competitionTeamViewModel);
			}

			_competitionRepository.Add(_mapper.Map<Competition>(competitionViewModel));
			_competitionRepository.Save();
			return Json(competitionViewModel);


			//return RedirectToAction(nameof(Index));

			//JsonResponseViewModel model = new JsonResponseViewModel();

			//try
			//{


			//    _competitionRepository.Add(_mapper.Map<Competition>(competitionViewModel));
			//    _competitionRepository.Save();


			//    int competitionId = _competitionRepository.GetLastInsertedId();
			//    competitionViewModel.Id = competitionId;

			//    foreach (var teamId in competitionViewModel.SelectedTeamsIds)
			//    {
			//        var competitionTeamViewModel = new CompetitionTeamViewModel()
			//        {
			//            TotalPoints = 0,
			//            TeamId = teamId,
			//            CompetitionId = competitionId
			//        };

			//        _competitionTeamRepository.Add(_mapper.Map<CompetitionTeam>(competitionTeamViewModel));
			//        _competitionTeamRepository.Save();
			//    }
			//    model.ResponseCode = 1;
			//    model.ResponseMessage = JsonConvert.SerializeObject(competitionViewModel);

			//    return Json(model);
			//}
			//catch (Exception ex)
			//{
			//    model.ResponseCode = 0;
			//    model.ResponseMessage = ex.Message;
			//    return Json(model);
			//}
		}


		// GET: Competitions/Edit/{id}
		public IActionResult Edit(int Id)
		{
			var competiton = _competitionRepository.GetById(Id);

			return View(_mapper.Map<CompetitionViewModel>(competiton));
		}

		// POST: Competitions/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(CompetitionViewModel competitionViewModel)
		{
			_competitionRepository.Update(_mapper.Map<Competition>(competitionViewModel));

			_competitionRepository.Save();

			return RedirectToAction(nameof(Index));
		}

		// GET: Competitions/Delete/5
		public IActionResult Delete(int id)
		{
			var competiton = _competitionRepository.GetById(id);

			if (competiton == null)
			{
				return NotFound();
			}


			return View(_mapper.Map<CompetitionViewModel>(competiton));
		}

		// POST: Competitions/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(CompetitionViewModel competitionViewModel)
		{
			if (competitionViewModel != null)
			{
				_competitionRepository.Delete(_mapper.Map<Competition>(competitionViewModel));
				_competitionRepository.Save();
				return RedirectToAction(nameof(Index));
			}
			return View();
		}

		// GET: Competitions/Edit/{id}
		public IActionResult Details(int id)
		{
			var competitionTeams = _competitionRepository.GetTeamsFromCompetion(id);
			var competitionMatches = _competitionMatchRepository.GetCompetitionMatches(id);
			var competition = _competitionRepository.GetById(id);
			if (competition == null) return NotFound();

			var competitionOverviewModel = new CompetitionOverviewModel()
			{
				Competition = _mapper.Map<CompetitionViewModel>(competition),
				CompetitionTeams = _mapper.Map<IList<CompetitionTeamViewModel>>(competitionTeams),
			};

			ViewData["CompetitionMatches"] = _mapper.Map<IList<CompetitionMatchViewModel>>(competitionMatches);
			ViewData["CompetitionTeams"] = competitionOverviewModel;

			return View(_mapper.Map<CompetitionViewModel>(competition));
		}

	}
}