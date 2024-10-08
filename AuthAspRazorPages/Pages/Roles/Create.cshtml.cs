﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.RoleAndPermission;
using AuthAspRazorPages.Application.RoleAndPermission;

namespace AuthAspRazorPages.Pages.Roles
{
    public class CreateModel : PageModel
    {
    
        private readonly IRoleApplication _roleApp;

        public CreateModel(IRoleApplication roleApp)
        {
            _roleApp = roleApp;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Role Role { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            _roleApp.Create(Role);

            return RedirectToPage("./Index");
        }
    }
}
