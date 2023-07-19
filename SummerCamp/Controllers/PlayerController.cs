using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerRepository playerRepository, ICountryRepository countryRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _countryRepository = countryRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var players = _playerRepository.GetAllWithTeamAndCountry();
            return View(_mapper.Map<IList<PlayerViewModel>>(players));
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            ViewData["Positions"] = GetPositions();

            List<SelectListItem> teams = new SelectList(_teamRepository.GetAll(), "Id", "Name").ToList();
            teams.Insert(0, (new SelectListItem { Text = "Without team", Value = "0" }));
            ViewData["Teams"] = teams;

            List<SelectListItem> countries = new SelectList(_countryRepository.GetAll(), "Id", "Name").ToList();
            countries.Insert(0, (new SelectListItem { Text = "Unknown", Value = "0" }));
            ViewData["Countries"] = countries;

            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerViewModel playerViewModel, [FromForm] IFormFile photo)
        {
            ViewData["Positions"] = GetPositions();

            List<SelectListItem> teams = new SelectList(_teamRepository.GetAll(), "Id", "Name").ToList();
            teams.Insert(0, (new SelectListItem { Text = "Without team", Value = "0" }));
            ViewData["Teams"] = teams;

            List<SelectListItem> countries = new SelectList(_countryRepository.GetAll(), "Id", "Name").ToList();
            countries.Insert(0, (new SelectListItem { Text = "Unknown", Value = "0" }));
            ViewData["Countries"] = countries;

            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets\uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                playerViewModel.Picture = fileName;
            }
            else
            {
                playerViewModel.Picture = "default.jpg";
            }
            if (playerViewModel.CountryId == 0)
            {
                playerViewModel.CountryId = null;
            }
            if (playerViewModel.TeamId == 0)
            {
                playerViewModel.TeamId = null;
            }

            if (!_playerRepository.IsShirtNumberUnique(playerViewModel.TeamId, playerViewModel.ShirtNumber))
            {
                ModelState.AddModelError("Error", "Shirt number already exist in this team.");

                return View(playerViewModel);
            }

            _playerRepository.Add(_mapper.Map<Player>(playerViewModel));
            _playerRepository.Save();

            return RedirectToAction(nameof(Index));
        }


        // GET: Coaches/Edit/{id}
        public IActionResult Edit(int Id)
        {
            ViewData["Positions"] = GetPositions();

            List<SelectListItem> teams = new SelectList(_teamRepository.GetAll(), "Id", "Name").ToList();
            teams.Insert(0, (new SelectListItem { Text = "Without team", Value = "0" }));
            ViewData["Teams"] = teams;

            List<SelectListItem> countries = new SelectList(_countryRepository.GetAll(), "Id", "Name").ToList();
            countries.Insert(0, (new SelectListItem { Text = "Unknown", Value = "0" }));
            ViewData["Countries"] = countries;

            var player = _playerRepository.GetById(Id);
            var playerViewModel = _mapper.Map<PlayerViewModel>(player);

            return View(playerViewModel);
        }

        // POST: Coaches/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerViewModel playerViewModel, [FromForm] IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets\uploads", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                playerViewModel.Picture = fileName;
            }

            _playerRepository.Update(_mapper.Map<Player>(playerViewModel));

            _playerRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Coaches/Delete/5
        public IActionResult Delete(int id)
        {
            var player = _playerRepository.GetById(id);

            if (player == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<PlayerViewModel>(player));
        }

        // POST: Coaches/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PlayerViewModel playerViewModel)
        {
            if (playerViewModel != null)
            {
                _playerRepository.Delete(_mapper.Map<Player>(playerViewModel));
                _playerRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private SelectList GetPositions()
        {
            var positions = from Position position in Enum.GetValues(typeof(Position))
                            select new
                            {
                                Id = (int)position,
                                Name = position.ToString()
                            };
            return new SelectList(positions, "Id", "Name");
        }
    }
}
