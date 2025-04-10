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
        public DbSet<UserPoints> UserPoints { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize your model here if needed
        }
    }
}
