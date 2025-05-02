using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DissertationProject.Models
{
    public class UserAchievement
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        public string UserId { get; set; }   // Foreign key to IdentityUser
        public string Badge { get; set; }    // The badge awarded
        public DateTime DateAchieved { get; set; }
        public IdentityUser User { get; set; }
    }
}
