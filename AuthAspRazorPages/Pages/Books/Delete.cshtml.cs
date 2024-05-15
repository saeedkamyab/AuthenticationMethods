using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Book;
using AuthAspRazorPages.Permissions;

namespace AuthAspRazorPages.Pages.Books
{

    public class DeleteModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public DeleteModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [NeedsPermission(FunctionPermmisionsCode.RemoveBook)]
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
            }
            return Page();
        }

        [NeedsPermission(FunctionPermmisionsCode.RemoveBook)]
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                Book = book;
                _context.Books.Remove(Book);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
