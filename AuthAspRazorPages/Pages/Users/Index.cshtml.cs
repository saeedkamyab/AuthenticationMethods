using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models;

namespace AuthAspRazorPages.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public IndexModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users
                .Include(u => u.Role).ToListAsync();
        }
    }
}
