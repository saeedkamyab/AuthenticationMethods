using AuthAspRazorPages.Models.RoleAndPermission;

namespace AuthAspRazorPages.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public string FName { get; private set; }
        public string ProfilePhoto { get; private set; }


        public int RoleId { get; private set; }
        public Role Role { get; private set; }

    }
}
