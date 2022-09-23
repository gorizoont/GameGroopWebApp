using GameGroopWebApp.Interfaces;
using GameGroopWebApp.Models;
using GameGroopWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GameGroopWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsRepository _eventsRepository;

        public EventsController(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Events> events = await _eventsRepository.GetAll();
            return View(events);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Events events = await _eventsRepository.GetByIdAsync(id);
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Events events)
        {
            if (!ModelState.IsValid)
            {
                return View(events);
            }
            _eventsRepository.Add(events);
            return RedirectToAction("Index");
        }
    }
}
