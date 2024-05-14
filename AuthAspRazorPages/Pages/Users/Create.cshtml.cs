using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models;
using AuthAspRazorPages.Application;

namespace AuthAspRazorPages.Pages.Users
{
 
    public class CreateModel : PageModel
    {
        public SelectList Roles;
        private readonly AuthAspRazorPages.EFcore.ProContext _context;
        private readonly IUserApplication _userApp;
        public CreateModel(ProContext context, IUserApplication userApp) 
        {
            _userApp = userApp;
            _context = context;
        }

        public IActionResult OnGet()
        {
       // ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            Roles = new SelectList(_context.Roles.ToList(), "Id", "Name");
   
            return Page();
        }

        [BindProperty]
        public User RegisterModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
       
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _userApp.Register(RegisterModel);
            return RedirectToPage("./Index");
        }
    }
}
