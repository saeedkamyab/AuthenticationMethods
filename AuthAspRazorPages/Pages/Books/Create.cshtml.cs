using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Book;
using AuthAspRazorPages.Permissions;

namespace AuthAspRazorPages.Pages.Books
{

    public class CreateModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public CreateModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        [NeedsPermission(FunctionPermmisionsCode.AddNewBook)]
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [NeedsPermission(FunctionPermmisionsCode.AddNewBook)]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
