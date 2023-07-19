using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ICompetitionMatchRepository _competitionMatchRepository;
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository teamRepository, ICoachRepository coachRepository, IMapper mapper, IPlayerRepository playerRepository, ISponsorRepository sponsorRepository, ICompetitionMatchRepository competitionMatchRepository)
        {
            _teamRepository = teamRepository;
            _coachRepository = coachRepository;
            _mapper = mapper;
            _playerRepository = playerRepository;
            _sponsorRepository = sponsorRepository;
            _competitionMatchRepository = competitionMatchRepository;
        }

        public IActionResult Index()
        {
            var teams = _teamRepository.GetAllWithPlayersCountriesAndCoach();

            return View(_mapper.Map<IList<TeamViewModel>>(teams));
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            List<SelectListItem> coaches = new SelectList(_coachRepository.GetUnassignedCoaches(), "Id", "FullName").ToList();
            coaches.Insert(0, (new SelectListItem { Text = "Without coach", Value = "0" }));
            ViewData["Coaches"] = coaches;

            var sponsors = _sponsorRepository.GetAll();
            ViewData["Sponsors"] = new SelectList(sponsors, "Id", "Name");

            var result = new TeamViewModel()
            {
                AvailableSponsors = _mapper.Map<IList<SponsorViewModel>>(sponsors)
            };

            return View(result);
        }

        // POST: Teams/Create
        [HttpPost]
        public JsonResult Create([FromBody] TeamViewModel teamViewModel)
        {
            foreach (var sponsorId in teamViewModel.SelectedSponsorsIds)
            {
                var teamSponsorViewModel = new TeamSponsorViewModel()
                {
                    SponsorId = sponsorId,
                };
                teamViewModel.TeamsSponsors.Add(teamSponsorViewModel);
            }

            _teamRepository.Add(_mapper.Map<Team>(teamViewModel));
            _teamRepository.Save();

            return Json(teamViewModel);
        }

        // GET: Teams/Edit/{id}
        public IActionResult Edit(int Id)
        {
            List<SelectListItem> coaches = new SelectList(_coachRepository.GetUnassignedCoaches(), "Id", "FullName").ToList();
            coaches.Insert(0, (new SelectListItem { Text = "Without coach", Value = "0" }));
            ViewData["Coaches"] = coaches;

            var team = _teamRepository.GetById(Id);

            return View(_mapper.Map<TeamViewModel>(team));
        }

        // PUT: Teams/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeamViewModel teamViewModel)
        {
            _teamRepository.Update(_mapper.Map<Team>(teamViewModel));

            _teamRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Delete/5
        public IActionResult Delete(int id)
        {
            var team = _teamRepository.GetById(id);

            if (team == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TeamViewModel>(team));
        }

        // POST: Teams/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(TeamViewModel teamViewModel)
        {
            int teamId = teamViewModel.Id;
            _competitionMatchRepository.DeleteTeamMatch(teamId);

            if (teamViewModel != null)
            {

                _teamRepository.Delete(_mapper.Map<Team>(teamViewModel));
                _teamRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Competitions/Edit/{id}
        public IActionResult Details(int id)
        {
            var team = _teamRepository.GetTeamWithPlayersCountriesAndCoach(id);

            return View(_mapper.Map<TeamViewModel>(team));
        }
        public IActionResult DeletePlayerFromTeam(int playerId)
        {
            var player = _playerRepository.GetById(playerId);

            if (player != null)
            {
                player.TeamId = null;
                player.Team = null;

                _playerRepository.Update(player);
                _playerRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
