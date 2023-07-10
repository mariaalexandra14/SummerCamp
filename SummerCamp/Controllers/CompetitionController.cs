using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class CompetitionController : Controller
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public CompetitionController(ICompetitionRepository competitionRepository, ICountryRepository countryRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _competitionRepository = competitionRepository;
            _countryRepository = countryRepository;
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
            return View();
        }

        // POST: Competitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompetitionViewModel competitionViewModel)
        {

            _competitionRepository.Add(_mapper.Map<Competition>(competitionViewModel));
            _competitionRepository.Save();

            return RedirectToAction(nameof(Index));
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
    }
}