using GameGroopWebApp.Data;
using GameGroopWebApp.Interfaces;
using GameGroopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
    }
}
