using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DissertationProject.Pages.Quizzes
{
    public class AchievementsModel : PageModel
    {
        private readonly FinalDisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public List<UserAchievement> UserAchievements { get; set; }
        public string Message { get; set; }

        public AchievementsModel(FinalDisContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // If the user is not logged in, redirect to the login page
                RedirectToPage("/Account/Login");
                return;
            }

            // Retrieve the user's achievements from the database
            UserAchievements = await _context.UserAchievements
                .Where(ua => ua.UserId == user.Id)
                .ToListAsync();

            // Retrieve the message from TempData, if any
            Message = TempData["Message"]?.ToString();
        }
    }
}
