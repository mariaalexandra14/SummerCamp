using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class CompetitionMatchController : Controller
    {
        private readonly ICompetitionMatchRepository _competitionMatchRepository;
        private readonly ICompetitionTeamRepository _competitionTeamRepository;
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMapper _mapper;

        public CompetitionMatchController(ICompetitionMatchRepository competitionMatchRepository, IMapper mapper, ICompetitionTeamRepository competitionTeamRepository, ICompetitionRepository competitionRepository)
        {
            _competitionMatchRepository = competitionMatchRepository;
            _competitionTeamRepository = competitionTeamRepository;
            _competitionRepository = competitionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var competitionMatches = _competitionMatchRepository.GetAll();
            return View(_mapper.Map<IList<CompetitionMatchViewModel>>(competitionMatches));
        }

        // GET: Coaches/Create
        public ActionResult Create(int competitionId)
        {
            ViewData["AwayTeams"] = GetTeamsFromCompetition(competitionId);

            ViewData["HomeTeams"] = GetTeamsFromCompetition(competitionId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompetitionMatchViewModel competitionMatch)
        {
            if (competitionMatch.HomeTeamId == competitionMatch.AwayTeamId)
            {
                ModelState.AddModelError("Error", "Home team cannot be the same as away team");

                ViewData["AwayTeams"] = GetTeamsFromCompetition(competitionMatch.CompetitionId);

                ViewData["HomeTeams"] = GetTeamsFromCompetition(competitionMatch.CompetitionId);

                return View(competitionMatch);
            }
            else
            {
                _competitionTeamRepository.UpdateScore(_mapper.Map<CompetitionMatch>(competitionMatch), competitionMatch.HomeTeamGoals, competitionMatch.AwayTeamGoals);
                _competitionTeamRepository.Save();

                _competitionMatchRepository.Add(_mapper.Map<CompetitionMatch>(competitionMatch));
                _competitionMatchRepository.Save();
            }

            return RedirectToAction("Details", "Competition", new { @id = competitionMatch.CompetitionId });
        }


        // GET: Coaches/Edit/{id}
        public IActionResult Edit(int id)
        {
            var competitionMatch = _competitionMatchRepository.GetById(id);

            ViewData["AwayTeams"] = GetTeamsFromCompetition(competitionMatch.CompetitionId);

            ViewData["HomeTeams"] = GetTeamsFromCompetition(competitionMatch.CompetitionId);

            return View(_mapper.Map<CompetitionMatchViewModel>(competitionMatch));
        }

        // POST: Coaches/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompetitionMatchViewModel competitionMatch)
        {
            if (competitionMatch.HomeTeamId == competitionMatch.AwayTeamId)
            {
                ModelState.AddModelError("Error", "Home team cannot be the same as away team");

                ViewData["AwayTeams"] = GetTeamsFromCompetition(competitionMatch.CompetitionId);

                ViewData["HomeTeams"] = GetTeamsFromCompetition(competitionMatch.CompetitionId);

                return View(competitionMatch);
            }
            else
            {

                // din context comp match
                var oldCompetitionMatch = _competitionMatchRepository.GetById(competitionMatch.Id);

                _competitionTeamRepository.UpdateScore(oldCompetitionMatch, competitionMatch.HomeTeamGoals, competitionMatch.AwayTeamGoals);
                _competitionTeamRepository.Save();

                _competitionMatchRepository.Update(_mapper.Map<CompetitionMatch>(competitionMatch));
                _competitionMatchRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Coaches/Delete/5
        public IActionResult Delete(int id)
        {
            var competitionMatch = _competitionMatchRepository.GetById(id);

            if (competitionMatch == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<CompetitionMatchViewModel>(competitionMatch));
        }

        // POST: Coaches/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CompetitionMatchViewModel competitionMatchViewModel)
        {
            if (competitionMatchViewModel != null)
            {
                _competitionMatchRepository.Delete(_mapper.Map<CompetitionMatch>(competitionMatchViewModel));
                _competitionMatchRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private SelectList GetTeamsFromCompetition(int competitionId)
        {
            return new SelectList(_competitionTeamRepository.GetTeamsFromCompetition(competitionId), "Id", "Name");
        }
    }
}
