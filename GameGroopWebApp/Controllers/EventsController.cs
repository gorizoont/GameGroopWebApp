using GameGroopWebApp.Data;
using GameGroopWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameGroopWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDBContext _context;

        public EventsController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Events> events = _context.Events.ToList();
            return View(events);
        }
    }
}
