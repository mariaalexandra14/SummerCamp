using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Implementations;
using SummerCamp.DataModels.Models;

namespace SummerCamp.Controllers
{
    [Route("[controller]/{action}")]
    public class TeamController : Controller
    {
        private readonly TeamRepository teamRepository;
        private readonly CoachRepository coachRepository;

        public TeamController(TeamRepository teamRepository, CoachRepository coachRepository)
        {
            this.teamRepository = teamRepository;
            this.coachRepository = coachRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["Coaches"] = new SelectList(coachRepository.GetAll(), "Id", "FullName");
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Name, NickName")] Team team, int coachId)
        {
            if (ModelState.IsValid)
            {
                //team.Coach = coachRepository.GetById(coachId);
                teamRepository.Add(team);
            }

            return RedirectToAction(nameof(Create));
        }
    }
}
