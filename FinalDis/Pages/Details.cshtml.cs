using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using DissertationProject.Models;
using FinalDis.Data;

namespace DissertationProject.Pages.Topics
{
    public class DetailsModel : PageModel
    {
        private readonly FinalDisContext _context;

        public Topic Topic { get; set; }

        public DetailsModel(FinalDisContext context)
          
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the topic including the related quizzes
            Topic = await _context.Topics
                .Include(t => t.Quizzes) // This loads the quizzes related to the topic
                .FirstOrDefaultAsync(t => t.TopicID == id);

            if (Topic == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
