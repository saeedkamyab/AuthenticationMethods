using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models;
using AuthAspRazorPages.Application;

namespace AuthAspRazorPages.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserApplication _userApp;
        public DeleteModel(IUserApplication userApp)
        {
            _userApp = userApp;
        }

        [BindProperty]
        public User User { get; set; } = default!;

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
            else
            {
                User = user;
            }
            return Page();
        }

        public IActionResult OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_userApp.Delete(id)) return BadRequest();


            return RedirectToPage("./Index");
        }
    }
}
