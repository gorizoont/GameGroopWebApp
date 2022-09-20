namespace GameGroopWebApp.Models
{
    public class AppUser
    {
        public int? Rank { get; set; }
        public int? Level { get; set; }
        public Address? Address { get; set; }
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<Events> Event { get; set; }
    }
}
