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

        public ResultModel(FinalDisContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // Redirect to login if the user is not logged in
                Response.Redirect("/Account/Login"); // You can manually handle the redirection
                return; // Ensure that no further processing occurs after redirection
            }

            // Retrieve the user's points from UserPoints table
            var userPoints = await _context.UserPoints
                .FirstOrDefaultAsync(up => up.UserId == user.Id);

            // Set the message and points for the Razor page
            Message = TempData["Message"]?.ToString();
            Points = userPoints?.Points ?? 0;

            // Retrieve the user's achievements from UserAchievements table
            UserAchievements = await _context.UserAchievements
                .Where(ua => ua.UserId == user.Id)
                .ToListAsync();
        }
    }
}
