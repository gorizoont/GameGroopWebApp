using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameGroopWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? Rank { get; set; }
        public int? Level { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ProfileImageUrl { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<Events> Event { get; set; }
        
    }
}
