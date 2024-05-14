using AuthAspRazorPages.Models.RoleAndPermission;

namespace AuthAspRazorPages.Application.RoleAndPermission
{
    public interface IRoleApplication
    {
        bool Create(Role createModel);
        bool Edit(Role editModel);

        Role GetDetails(int id);

    }
}
