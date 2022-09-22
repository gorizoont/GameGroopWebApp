using GameGroopWebApp.Data;
using GameGroopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Detail(int id)
        {
            Events events = _context.Events.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(events);
        }
    }
}
