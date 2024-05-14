using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Book;

namespace AuthAspRazorPages.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public IndexModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Books.ToListAsync();
        }
    }
}
