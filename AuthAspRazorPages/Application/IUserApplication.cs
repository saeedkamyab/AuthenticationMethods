using AuthAspRazorPages.Models;

namespace AuthAspRazorPages.Application
{
    public interface IUserApplication
    {
        bool Login(Login loginModel);
        void Logout();

        User GetDetails(int id);

        List<User> GetAccounts();

        bool Edit(User editModel);

        void Register(User registerModel);

        bool Delete(int id);
    }
}
