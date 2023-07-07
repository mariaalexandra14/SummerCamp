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
            var coaches = _coachRepository.GetAllWithCountriesAndTeam();
            return View(_mapper.Map<IList<CoachViewModel>>(coaches));
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            ViewData["Teams"] = new SelectList(_teamRepository.GetAll(), "Id", "Name");
            ViewData["Countries"] = new SelectList(_countryRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoachViewModel coachViewModel, int countryId, int teamId, [FromForm] IFormFile photo)
        {
            //if ModelState.IsValid
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

            _coachRepository.Add(_mapper.Map<Coach>(coachViewModel));
            return RedirectToAction(nameof(Index));
        }


        // GET: Coaches/Edit/{id}
        public IActionResult Edit(int coachId)
        {
            //ViewData["Teams"] = new SelectList(_teamRepository.GetAll(), "Id", "Name");
            //ViewData["Countries"] = new SelectList(_countryRepository.GetAll(), "Id", "Name");

            var coach = _coachRepository.GetById(coachId);

            return View(_mapper.Map<CoachViewModel>(coach));
        }

        // POST: Coaches/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoachViewModel coachViewModel)
        {
            _coachRepository.Update(_mapper.Map<Coach>(coachViewModel));

            return View(coachViewModel);
        }

    }
}
