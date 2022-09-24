using GameGroopWebApp.Data.Enum;
using GameGroopWebApp.Models;

namespace GameGroopWebApp.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public IFormFile Image { get; set; }
        
    }
}
