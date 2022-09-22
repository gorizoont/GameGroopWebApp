using GameGroopWebApp.Data;
using GameGroopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly AppDBContext _context;

        public ClubController(AppDBContext context)
        {
            _context = context;
        }

        

        public IActionResult Index() //C
        {
            var clubs = _context.Clubs.ToList(); //M
            return View(clubs); //V
        }

        public IActionResult Detail(int id)
        {
            Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(club);
        }

    }
}
