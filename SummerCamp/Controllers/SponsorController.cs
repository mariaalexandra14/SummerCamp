using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class SponsorController : Controller
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IMapper _mapper;

        public SponsorController(ISponsorRepository sponsorRepository, IMapper mapper)
        {
            _sponsorRepository = sponsorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sponsors = _sponsorRepository.GetAll();
            return View(_mapper.Map<IList<SponsorViewModel>>(sponsors));
        }

        // GET: Coaches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coaches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SponsorViewModel sponsorViewModel)
        {
            _sponsorRepository.Add(_mapper.Map<Sponsor>(sponsorViewModel));
            _sponsorRepository.Save();

            return RedirectToAction(nameof(Index));
        }


        // GET: Coaches/Edit/{id}
        public IActionResult Edit(int Id)
        {
            var sponsor = _sponsorRepository.GetById(Id);

            return View(_mapper.Map<SponsorViewModel>(sponsor));
        }

        // POST: Coaches/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SponsorViewModel sponsorViewModel)
        {
            _sponsorRepository.Update(_mapper.Map<Sponsor>(sponsorViewModel));

            _sponsorRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: Coaches/Delete/5
        public IActionResult Delete(int id)
        {
            var sponsor = _sponsorRepository.GetById(id);

            if (sponsor == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<SponsorViewModel>(sponsor));
        }

        // POST: Coaches/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(SponsorViewModel sponsorViewModel)
        {
            if (sponsorViewModel != null)
            {
                _sponsorRepository.Delete(_mapper.Map<Sponsor>(sponsorViewModel));
                _sponsorRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
