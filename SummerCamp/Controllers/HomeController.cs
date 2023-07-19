using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.Models;
using System.Diagnostics;

namespace SummerCamp.Controllers
{
    [Controller]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICompetitionTeamRepository _competitionTeamRepository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ICompetitionRepository competitionRepository, IMapper mapper, IPlayerRepository playerRepository, ITeamRepository teamRepository, ICompetitionTeamRepository competitionTeamRepository)
        {
            _logger = logger;
            _competitionRepository = competitionRepository;
            _mapper = mapper;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
            _competitionTeamRepository = competitionTeamRepository;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var competition = _mapper.Map<CompetitionViewModel>(_competitionRepository.GetNextCompetition());

            var teams = _teamRepository.GetAll();
            var teamsRanking = new List<TeamRankingViewModel>();

            foreach (var team in teams)
            {
                var teamPoints = 0;
                var competitionTeams = _competitionTeamRepository.GetAll().Where(x => x.TeamId == team.Id).ToList();
                foreach (var competitionTeam in competitionTeams)
                {
                    teamPoints += competitionTeam.TotalPoints;
                }
                var teamRanking = new TeamRankingViewModel()
                {
                    Points = teamPoints,
                    TeamViewModel = _mapper.Map<TeamViewModel>(team)
                };
                teamsRanking.Add(teamRanking);
            }
            ViewData["Teams"] = teamsRanking.OrderByDescending(x => x.Points).Take(3);

            return View(competition);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}