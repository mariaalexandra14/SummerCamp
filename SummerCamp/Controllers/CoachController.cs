using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class CoachController : Controller
    {
        private readonly ICoachRepository _coachRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public CoachController(ICoachRepository coachRepository, ICountryRepository countryRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _coachRepository = coachRepository;
            _countryRepository = countryRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var coaches = _coachRepository.GetAllWithCountries();

            return View(_mapper.Map<IList<CoachViewModel>>(coaches));
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            List<SelectListItem> countries = new SelectList(_countryRepository.GetAll(), "Id", "Name").ToList();
            countries.Insert(0, (new SelectListItem { Text = "Unknown", Value = "0" }));
            ViewData["Countries"] = countries;

            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoachViewModel coachViewModel, [FromForm] IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets\uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                coachViewModel.Picture = fileName;
            }
            else
            {
                coachViewModel.Picture = "default.jpg";
            }

            _coachRepository.Add(_mapper.Map<Coach>(coachViewModel));
            _coachRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Coaches/Edit/{id}
        public IActionResult Edit(int Id)
        {
            List<SelectListItem> countries = new SelectList(_countryRepository.GetAll(), "Id", "Name").ToList();
            countries.Insert(0, (new SelectListItem { Text = "Unknown", Value = "0" }));
            ViewData["Countries"] = countries;

            var coach = _coachRepository.GetById(Id);

            return View(_mapper.Map<CoachViewModel>(coach));
        }

        // POST: Coaches/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoachViewModel coachViewModel)
        {
            _coachRepository.Update(_mapper.Map<Coach>(coachViewModel));

            _coachRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Coaches/Delete/5
        public IActionResult Delete(int id)
        {
            var coach = _coachRepository.GetWithCountryAndTeam(id);

            if (coach == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<CoachViewModel>(coach));
        }

        // POST: Coaches/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoachViewModel coachViewModel)
        {
            if (coachViewModel != null)
            {
                _coachRepository.Delete(_mapper.Map<Coach>(coachViewModel));
                _coachRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
