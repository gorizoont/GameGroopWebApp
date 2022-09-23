using GameGroopWebApp.Models;

namespace GameGroopWebApp.Interfaces
{
    public interface IEventsRepository
    {
        Task<IEnumerable<Events>> GetAll();
        Task<Events> GetByIdAsync(int id);
        Task<IEnumerable<Events>> GetAllEventsByCity(string city);
        bool Add(Events events);
        bool Delete(Events events);
        bool Update(Events events);
        bool Save();
    }
}
