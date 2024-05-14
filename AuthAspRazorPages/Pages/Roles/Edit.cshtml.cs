using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.RoleAndPermission;
using AuthAspRazorPages.Application.RoleAndPermission;
using AuthAspRazorPages.Permissions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Components.Forms;

namespace AuthAspRazorPages.Pages.Roles
{
    public class EditModel : PageModel
    {

        private readonly IRoleApplication _roleApp;
        private readonly IEnumerable<IPermissionExposer> _exposers;

        public List<SelectListItem> Permissions = new List<SelectListItem>();

        public EditModel(IRoleApplication roleApp, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApp = roleApp;
            _exposers = exposers;
        }

        [BindProperty]
        public Role editRoleModel { get; set; } = default!;

        public IActionResult OnGet(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            editRoleModel = _roleApp.GetDetails(id);
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    var group = new SelectListGroup { Name = key };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if (editRoleModel.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }



            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            foreach (var permItem in Permissions)
            {
                if (permItem.Selected == true)
                    editRoleModel.Permissions.Add(new Permission(Convert.ToInt32(permItem.Value), permItem.Text));
            }

            var result = _roleApp.Edit(editRoleModel);
            return RedirectToPage("Index");
        }


    }
}
