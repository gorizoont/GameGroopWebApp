using GameGroopWebApp.Data;
using GameGroopWebApp.Interfaces;
using GameGroopWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userEvents = await _dashboardRepository.GetAllUserEvents();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Events = userEvents,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}
