using GameGroopWebApp.Data;
using GameGroopWebApp.Interfaces;
using GameGroopWebApp.Models;

namespace GameGroopWebApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(AppDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == curUser);
            return userClubs.ToList();
        }

        public async Task<List<Events>> GetAllUserEvents()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userEvents = _context.Events.Where(r => r.AppUser.Id == curUser);
            return userEvents.ToList();
        }
    }
}
