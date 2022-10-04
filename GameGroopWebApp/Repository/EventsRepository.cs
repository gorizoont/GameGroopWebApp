using GameGroopWebApp.Data;
using GameGroopWebApp.Interfaces;
using GameGroopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGroopWebApp.Repository
{
    public class EventsRepository : IEventsRepository
    {
        private readonly AppDBContext _context;
        public EventsRepository(AppDBContext context)
        {
            _context = context;
        }
        public bool Add(Events events)
        {
            _context.Add(events);
            return Save();
        }

        public bool Delete(Events events)
        {
            _context.Remove(events);
            return Save();
        }

        public async Task<IEnumerable<Events>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<IEnumerable<Events>> GetAllEventsByCity(string city)
        {
            return await _context.Events.Where(c => c.Address.City.Contains(city)).ToListAsync();
            
        }

        public async Task<Events> GetByIdAsync(int id)
        {
            return await _context.Events.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Events> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Events.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Events events)
        {
            _context.Update(events);
            return Save();
        }
    }
}
