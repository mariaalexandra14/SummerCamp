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
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IMapper _mapper;

        public CompetitionController(ICompetitionRepository competitionRepository, ICompetitionTeamRepository competitionTeamRepository, ICompetitionMatchRepository competitionMatchRepository, ITeamRepository teamRepository, IMapper mapper, ISponsorRepository sponsorRepository)
        {
            _competitionRepository = competitionRepository;
            _competitionTeamRepository = competitionTeamRepository;
            _competitionMatchRepository = competitionMatchRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
            _sponsorRepository = sponsorRepository;
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
            ViewData["Sponsors"] = new SelectList(_sponsorRepository.GetAll(), "Id", "Name");

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
            if (competitionViewModel.StartDate > competitionViewModel.EndDate)
            {
                Response.StatusCode = 400;
                return Json("End date cannot be before start date.");
            }

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
        }


        // GET: Competitions/Edit/{id}
        public IActionResult Edit(int id)
        {
            ViewData["Sponsors"] = new SelectList(_sponsorRepository.GetAll(), "Id", "Name");

            var competiton = _competitionRepository.GetById(id);
            List<int> selectedTeamsIds = new List<int>();

            var competitionTeams = _competitionTeamRepository.GetTeamsFromCompetition(id);
            foreach (var competitionTeam in competitionTeams)
            {
                selectedTeamsIds.Add(competitionTeam.Id);
            }

            var competitionViewModel = _mapper.Map<CompetitionViewModel>(competiton);
            competitionViewModel.SelectedTeamsIds = selectedTeamsIds;

            var allTeams = _teamRepository.GetAll();
            competitionViewModel.AvailableTeams = _mapper.Map<IEnumerable<TeamViewModel>>(allTeams);


            return View(competitionViewModel);
        }

        // PUT: Competitions/Edit/{id}
        [HttpPut]
        public JsonResult Edit([FromBody] CompetitionViewModel competitionViewModel)
        {
            var competitionTeams = _competitionTeamRepository.Get(ct => ct.CompetitionId == competitionViewModel.Id);

            var notSelectedTeams = competitionTeams.Where(ct => !competitionViewModel.SelectedTeamsIds.Contains(ct.TeamId ?? 0)).ToList();

            _competitionTeamRepository.RemoveRange(notSelectedTeams);

            foreach (var teamId in competitionViewModel.SelectedTeamsIds)
            {
                if (!competitionTeams.Select(c => c.TeamId).Contains(teamId))
                {
                    var competitionTeamViewModel = new CompetitionTeamViewModel()
                    {
                        TotalPoints = 0,
                        TeamId = teamId
                    };
                    competitionViewModel.CompetitionTeams.Add(competitionTeamViewModel);
                }
            }

            _competitionRepository.Update(_mapper.Map<Competition>(competitionViewModel));

            _competitionRepository.Save();

            return Json(competitionViewModel);
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
            var competitionTeams = _competitionRepository.GetTeamsFromCompetion(id).OrderByDescending(x => x.TotalPoints);
            var competitionMatches = _competitionMatchRepository.GetCompetitionMatches(id);
            var competition = _competitionRepository.GetById(id);
            if (competition == null) return NotFound();

            var competitionOverviewModel = new CompetitionOverviewModel()
            {
                Competition = _mapper.Map<CompetitionViewModel>(competition),
                CompetitionTeams = _mapper.Map<IList<CompetitionTeamViewModel>>(competitionTeams),
            };

            var competitionMatchesModel = new CompetitionMatchesModel()
            {
                CompetitionId = id,
                CompetitionMatches = _mapper.Map<List<CompetitionMatchViewModel>>(competitionMatches),
            };


            ViewData["CompetitionMatches"] = competitionMatchesModel;
            ViewData["CompetitionTeams"] = competitionOverviewModel;

            return View(_mapper.Map<CompetitionViewModel>(competition));
        }

    }
}