using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAspRazorPages.Models.RoleAndPermission
{
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; set; }



        public List<User> Users { get; private set; }
        public List<Permission> Permissions { get; private set; }
        [NotMapped]
        public List<int> PermissionCodes { get; private set; }

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
