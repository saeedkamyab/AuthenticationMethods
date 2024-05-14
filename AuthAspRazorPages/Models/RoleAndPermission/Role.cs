using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAspRazorPages.Models.RoleAndPermission
{
    public class Role
    {
        public int Id { get;  set; }
        public string Name { get; set; }



        public List<User> Users { get;  set; }
        public List<Permission> Permissions { get;  set; }
        [NotMapped]
        public List<int> PermissionCodes { get;  set; }

        protected Role() { }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            Users = new List<User>();
        }
        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
