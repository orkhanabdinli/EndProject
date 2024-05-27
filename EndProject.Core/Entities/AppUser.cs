using Microsoft.AspNetCore.Identity;

namespace EndProject.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfileImgUrl { get; set; }
        public string Country {  get; set; }
    }
}
