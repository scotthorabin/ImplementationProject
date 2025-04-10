using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DissertationProject.Models;
using Microsoft.AspNetCore.Identity; // Adjust the namespace if needed

namespace FinalDis.Data
{
    public class FinalDisContext : IdentityDbContext<IdentityUser>
    {
        
        public FinalDisContext(DbContextOptions<FinalDisContext> options)
            : base(options)
        { }

        // Your custom DbSets

        
       public DbSet<UserAchievement> UserAchievements { get; set; }  // For storing user badges
        public DbSet<UserPoints> UserPoints { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite key for UserAchievement
            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.UserId, ua.Badge });
        }
    }
}
