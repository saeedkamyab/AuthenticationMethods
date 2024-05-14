using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Article;

namespace AuthAspRazorPages.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public DetailsModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                Article = article;
            }
            return Page();
        }
    }
}
