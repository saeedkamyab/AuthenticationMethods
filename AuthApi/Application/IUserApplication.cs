

using AuthApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Application
{
    public interface IUserApplication
    {
        string Login(Login loginModel);
        //bool Login2(Login loginModel);
        void Logout();

        User GetDetails(int id);

        List<User> GetAccounts();

        bool Edit(User editModel);

        void Register(User registerModel);

        bool Delete(int id);
    }
}
