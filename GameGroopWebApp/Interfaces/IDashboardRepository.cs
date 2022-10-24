using GameGroopWebApp.Models;

namespace GameGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Events>> GetAllUserEvents();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();

    }
}
