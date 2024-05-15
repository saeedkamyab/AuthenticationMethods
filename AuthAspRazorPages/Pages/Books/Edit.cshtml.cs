using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Book;
using AuthAspRazorPages.Permissions;

namespace AuthAspRazorPages.Pages.Books
{

    public class EditModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public EditModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;


        [NeedsPermission(FunctionPermmisionsCode.EditBook)]
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            Book = book;
            return Page();
        }

        [NeedsPermission(FunctionPermmisionsCode.EditBook)]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
