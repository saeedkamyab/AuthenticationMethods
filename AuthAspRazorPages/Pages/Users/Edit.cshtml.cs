using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models;
using AuthAspRazorPages.Application;

namespace AuthAspRazorPages.Pages.Users
{
    public class EditModel : PageModel
    {
        public SelectList Roles;
        private readonly AuthAspRazorPages.EFcore.ProContext _context;
        private readonly IUserApplication _userApp;
        public EditModel(AuthAspRazorPages.EFcore.ProContext context, IUserApplication userApplication)
        {
            _context = context;
            _userApp = userApplication;
        }

        [BindProperty]
        public User editModel { get; set; } = default!;

        public IActionResult OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userApp.GetDetails(id);
            if (user == null)
            {
                return NotFound();
            }
            editModel = user;
            Roles = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _userApp.Edit(editModel);
        
            //_context.Attach(User).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(User.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
