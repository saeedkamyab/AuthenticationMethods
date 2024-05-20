using AuthApi.Models;
using System.Collections.Generic;

namespace AuthApi.Common
{
    public interface IAuthHelper
    {
        LoginResult Signin(AuthViewModel account);
        bool IsAuthenticated();
        List<int> GetPermissions();
        void SignOut();

        #region I don't need for now
        //long CurrentAccountId();
        //string CurrentAccountRole();
        //AuthViewModel CurrentAccountInfo();
        //string CurrentAccountMobile();
        #endregion
    }
}
