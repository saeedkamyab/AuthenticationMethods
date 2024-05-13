using AuthAspRazorPages.Common;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models;

namespace AuthAspRazorPages.Application
{
    public class UserApplication : IUserApplication
    {

        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passHasher;
        private readonly ProContext _proContext;

        public UserApplication(IAuthHelper authHelper, IPasswordHasher passHasher,ProContext proContext)
        {
            _authHelper = authHelper;
            _passHasher = passHasher;
            _proContext = proContext;
        }

        public bool Login(Login loginModel)
        {
            var user = _proContext.Users.FirstOrDefault(x=>x.UserName==loginModel.UserName);

            if (user == null)
                return false;

            var res = _passHasher.Check(user.Password,loginModel.Password);

            if(!res.Verified) return false;


            var authViewModel=new AuthViewModel(user.Id,0,user.FullName,user.UserName,user.ProfilePhoto,new List<int>());

            _authHelper.Signin(authViewModel);

            return true;
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
