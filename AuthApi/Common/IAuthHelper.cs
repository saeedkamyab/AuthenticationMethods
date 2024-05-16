using AuthApi.Models;
using System.Collections.Generic;

namespace AuthApi.Common
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthenticated();
        LoginResult Signin(AuthViewModel account);
        // void Signin(AuthViewModel account);
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        List<int> GetPermissions();
        long CurrentAccountId();
        string CurrentAccountMobile();
    }
}
