using FinalDis.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DissertationProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinalDis.Models;

namespace FinalDis.Pages
{
    public class FandQModel : PageModel
    {
        private readonly FinalDisContext _context;

        public FandQModel(FinalDisContext context)
        {
            _context = context;
        }

        public List<FAQ> FAQs { get; set; }

        public async Task OnGetAsync()
        {
            // Retrieve FAQs from the database
            FAQs = await _context.FAQs.ToListAsync();
        }
    }
}
