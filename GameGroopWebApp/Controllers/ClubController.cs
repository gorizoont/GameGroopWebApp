using GameGroopWebApp.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
