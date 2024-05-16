

using AuthApi.Models;

namespace AuthApi.Application
{
    public interface IUserApplication
    {
        LoginResult Login(Login loginModel);
        //bool Login2(Login loginModel);
        void Logout();

        User GetDetails(int id);

        List<User> GetAccounts();

        bool Edit(User editModel);

        void Register(User registerModel);

        bool Delete(int id);
    }
}
