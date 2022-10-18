using GameGroopWebApp.Models;

namespace GameGroopWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Events>> GetAllUserEvents();
        Task<List<Club>> GetAllUserClubs();
    }
}
