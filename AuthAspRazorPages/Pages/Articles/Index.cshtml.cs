using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Article;
using AuthAspRazorPages.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace AuthAspRazorPages.Pages.Articles
{
    [NeedsPermission(FunctionPermmisionsCode.ArticleList)]
    public class IndexModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public IndexModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;
      
        public async Task OnGetAsync()
        {
            Article = await _context.Articles.ToListAsync();
        }
    }
}
