using Microsoft.AspNetCore.Identity;

namespace DissertationProject.Models
{
    public class UserPoints
    {
        public string Id { get; set; }
        public string UserId { get; set; }  // FK to IdentityUser
        public int Points { get; set; }     // Points for the user
        public IdentityUser User { get; set; }
    }

}
