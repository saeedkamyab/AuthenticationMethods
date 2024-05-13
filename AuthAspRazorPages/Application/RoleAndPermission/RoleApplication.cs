using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.RoleAndPermission;

namespace AuthAspRazorPages.Application.RoleAndPermission
{
    public class RoleApplication : IRoleApplication
    {
        public ProContext _context;

        public RoleApplication(ProContext context)
        {
            _context = context;
        }

        public bool Create(Role createModel)
        {
            if (_context.Roles.Any(x => x.Name == createModel.Name)) return false;

            var role = new Role(createModel.Name, new List<Permission>());
            _context.Roles.Add(role);
            _context.SaveChanges();

            return true;
        }

        public bool Edit(Role editModel)
        {
            var role = _context.Roles.Find(editModel.Id);
            
            if (role == null) return false;

            var permissions=new List<Permission>();

            editModel.PermissionCodes.ForEach(code => permissions.Add(new Permission(code)));
      
            role.Edit(editModel.Name, permissions);

            _context.SaveChanges();

            return true;
        }
    }
}
