

using AuthApi.Models.RoleAndPermission;

namespace AuthApi.Application.RoleAndPermission
{
    public interface IRoleApplication
    {
        bool Create(Role createModel);
        bool Edit(Role editModel);

        Role GetDetails(int id);

    }
}
