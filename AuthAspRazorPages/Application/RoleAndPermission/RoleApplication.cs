using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models.RoleAndPermission;
using AuthAspRazorPages.Permissions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            editModel.PermissionCodes?.ForEach(code => permissions.Add(new Permission(code)));
      
            role.Edit(editModel.Name, permissions);

            _context.SaveChanges();

            return true;
        }

        public Role GetDetails(int id)
        {

            //var role = _context.Roles.FirstOrDefault(x=>x.Id==id);

            //var role = _context.Roles.Select(x => new Role
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    MappedPermissions = x.MappedPermissions
            //}).AsNoTracking().FirstOrDefault(x => x.Id == id);
            //role.MappedPermissions = role.MappedPermissions.Select(x => x.Code).ToList();


            var role = _context.Roles.Select(x => new Role
            {
                Id = x.Id,
                Name = x.Name,
                MappedPermissions = MapPermissions(x.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            role.PermissionCodes = role.MappedPermissions.Select(x => x.Code).ToList();

            return role;
        }
        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }
        //public List<Role> List()
        //{
        //    return _context.Roles.Select(x => new Role
        //    {
        //        Id = x.Id,
        //        Name = x.Name
        //    }).ToList();
        //}
    }
}
