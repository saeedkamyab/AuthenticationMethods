
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthApi.Models.RoleAndPermission
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;  set; }
        public string Name { get; set; }



        public List<User> Users { get;  set; }
        
        public List<Permission> Permissions { get;  set; }=new List<Permission>();
        
        [NotMapped]
        public List<int> PermissionCodes { get;  set; }
        //[NotMapped]
        //public List<PermissionDto> MappedPermissions { get; set; }

        public Role() { }

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
