using Microsoft.AspNetCore.Identity;

namespace DissertationProject.Models
{
    public class UserAchievement
    {
        public string UserId { get; set; }   // Foreign key to the IdentityUser
        public string Badge { get; set; }     // The badge awarded to the user
        public DateTime DateAchieved { get; set; }

        // Navigation property
        public IdentityUser User { get; set; } // Optional: if you want to access the user data from this class
    }

}
