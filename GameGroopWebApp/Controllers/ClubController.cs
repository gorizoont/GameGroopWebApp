using Microsoft.AspNetCore.Mvc;

namespace GameGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
