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
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository teamRepository, ICoachRepository coachRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _coachRepository = coachRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var teams = _teamRepository.GetAllWithPlayersCountriesAndCoach();

            return View(_mapper.Map<IList<TeamViewModel>>(teams));
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            List<SelectListItem> coaches = new SelectList(_coachRepository.GetAll(), "Id", "FullName").ToList();
            coaches.Insert(0, (new SelectListItem { Text = "Without coach", Value = "0" }));
            ViewData["Coaches"] = coaches;

            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {
                _teamRepository.Add(_mapper.Map<Team>(teamViewModel));
                _teamRepository.Save();
            }

            return RedirectToAction(nameof(Create));
        }

        // GET: Teams/Edit/{id}
        public IActionResult Edit(int Id)
        {
            List<SelectListItem> coaches = new SelectList(_coachRepository.GetAll(), "Id", "FullName").ToList();
            coaches.Insert(0, (new SelectListItem { Text = "Without coach", Value = "0" }));
            ViewData["Coaches"] = coaches;

            var team = _teamRepository.GetById(Id);

            return View(_mapper.Map<TeamViewModel>(team));
        }

        // POST: Teams/Edit/{id}
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
            if (teamViewModel != null)
            {
                _teamRepository.Delete(_mapper.Map<Team>(teamViewModel));
                _teamRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
