using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DissertationProject.Models;
using Microsoft.AspNetCore.Identity;
using FinalDis.Models; // Adjust the namespace if needed

namespace FinalDis.Data
{
    public class FinalDisContext : IdentityDbContext<IdentityUser>
    {
        
        public FinalDisContext(DbContextOptions<FinalDisContext> options)
            : base(options)
        { }

     
        // Database Sets
        
       public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserPoints> UserPoints { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<FAQ> FAQs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // composite key for user achievement
            modelBuilder.Entity<UserAchievement>()
                .HasKey(ua => new { ua.UserId, ua.Badge });
        }
    }
}
