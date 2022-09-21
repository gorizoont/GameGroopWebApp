using Microsoft.AspNetCore.Mvc;

namespace GameGroopWebApp.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
