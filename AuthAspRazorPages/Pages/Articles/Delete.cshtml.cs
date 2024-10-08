﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.Article;
using AuthAspRazorPages.Permissions;

namespace AuthAspRazorPages.Pages.Articles
{

    public class DeleteModel : PageModel
    {
        private readonly AuthAspRazorPages.EFcore.ProContext _context;

        public DeleteModel(AuthAspRazorPages.EFcore.ProContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        [NeedsPermission(FunctionPermmisionsCode.RemoveArticle)]
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
        [NeedsPermission(FunctionPermmisionsCode.RemoveArticle)]
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                Article = article;
                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
