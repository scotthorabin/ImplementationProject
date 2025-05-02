using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DissertationProject.Pages
{
    public class ResultModel : PageModel
    {
        private readonly FinalDisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public string Message { get; set; }
        public int Points { get; set; }
        public List<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();

        public List<string> TopicNames { get; set; } = new List<string>(); // Add this to store topic names for each badge

        public ResultModel(FinalDisContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                RedirectToPage("/Account/Login");
                return;
            }

            // Retrieve the user's achievements
            UserAchievements = await _context.UserAchievements
                .Where(ua => ua.UserId == user.Id)
                .ToListAsync();

            // Retrieve the message from TempData
            Message = TempData["Message"]?.ToString();

            // Displays the badge names
            foreach (var achievement in UserAchievements)
            {
                Console.WriteLine($"Earned Badge: {achievement.Badge}");
            }
        }

    }
}



