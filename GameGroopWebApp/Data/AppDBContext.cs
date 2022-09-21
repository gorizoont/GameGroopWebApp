using GameGroopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGroopWebApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet <Events> Events { get; set; }
        public DbSet <Club> Clubs { get; set; }
        public DbSet <Address> Addresss { get; set; }
    }
}
