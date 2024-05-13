using AuthAspRazorPages.Models;

namespace AuthAspRazorPages.Application
{
    public interface IUserApplication
    {
        bool Login(Login loginModel);
        void Logout();
    }
}
