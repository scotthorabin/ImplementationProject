using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalDis.Pages
{
    public class TopicListModel : PageModel

    {
        private readonly FinalDisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public List<Topic> Topics { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public int UserPoints { get; set; }  // Add UserPoints property to store the current user's points

        // Injecting FinalDisContext and UserManager
        public TopicListModel(FinalDisContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            // Fetch Topics from the database
            Topics = await _context.Topics.ToListAsync();

            // Fetch Quizzes and include their Topics (if needed) from the database
            Quizzes = await _context.Quizzes
                .Include(q => q.Topic)  
                .ToListAsync();

            // Fetch the current user's points
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userPoints = await _context.UserPoints
                    .FirstOrDefaultAsync(up => up.UserId == user.Id);  

                if (userPoints != null)
                {
                    UserPoints = userPoints.Points;  
                }
            }
        }
    }
}