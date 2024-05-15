using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Article;
using AuthAspRazorPages.Permissions;

namespace AuthAspRazorPages.Pages.Articles
{

    public class CreateModel : PageModel
    {
       
      
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

       
        public CreateModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }
        [NeedsPermission(FunctionPermmisionsCode.AddNewArticle)]
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        [NeedsPermission(FunctionPermmisionsCode.AddNewArticle)]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
