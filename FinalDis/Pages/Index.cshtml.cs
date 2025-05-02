using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DissertationProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;

namespace DissertationProject.Pages.Topics
{
    public class IndexModel : PageModel
    {
        private readonly FinalDisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public List<Topic> Topics { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public int UserPoints { get; set; }

        // Injecting FinalDisContext and UserManager
        public IndexModel(FinalDisContext context, UserManager<IdentityUser> userManager)
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
                .Include(q => q.Topic)  // Include the related Topic for each Quiz
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