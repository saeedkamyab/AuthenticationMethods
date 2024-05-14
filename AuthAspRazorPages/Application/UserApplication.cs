using AuthAspRazorPages.Common;
using AuthAspRazorPages.EFcore;
using AuthAspRazorPages.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AuthAspRazorPages.Application
{
    public class UserApplication : IUserApplication
    {

        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passHasher;
        private readonly IFileUploader _fileUploader;
        private readonly ProContext _proContext;

        public UserApplication(IAuthHelper authHelper, IPasswordHasher passHasher, IFileUploader fileUploader, ProContext proContext)
        {
            _authHelper = authHelper;
            _passHasher = passHasher;
            _proContext = proContext;
            _fileUploader = fileUploader;
        }

        public List<User> GetAccounts()
        {
            return _proContext.Users.ToList();
        }

        public User GetDetails(int id)
        {
            return _proContext.Users.FirstOrDefault(u => u.Id == id);
        }
        public void Register(User registerModel)
        {
            var pass = registerModel.Password;

            pass = _passHasher.Hash(pass);


            var user = new User(registerModel.UserName,pass, registerModel.FullName, registerModel.FName,
                registerModel.ProfilePhoto, registerModel.RoleId);

            _proContext.Add(user);

            _proContext.SaveChanges();


        }
        public bool Edit(User editModel)
        {
            string password = null;
            var user = GetDetails(editModel.Id);
            if (user == null) return false;

            if (editModel.Password != null)
            {
                if (editModel.Password != editModel.RePassword)
                    return false;
                password = _passHasher.Hash(editModel.Password);
            }
            var path = editModel.Id.ToString();

            var picturePath = _fileUploader.Upload(editModel.ProfilePhotoFile, path);

            user.Edit(editModel.UserName, password, editModel.FullName, editModel.FName, picturePath, editModel.RoleId);
            _proContext.SaveChanges();
            return true;

        }

        public bool Delete(int id)
        {
            var user = GetDetails(id);
            if (user == null) return false;
            _proContext.Remove(user);
            _proContext.SaveChanges();
            return true;
        }

        public bool Login(Login loginModel)
        {
            var user = _proContext.Users.FirstOrDefault(x => x.UserName == loginModel.UserName);

            if (user == null)
                return false;

            //var res = _passHasher.Check(user.Password,loginModel.Password);

            //if(!res.Verified) return false;

            var permissions = _proContext.Roles.Find(user.RoleId)
              .Permissions
              .Select(x => x.Code)
              .ToList();


            var authViewModel = new AuthViewModel(user.Id, user.RoleId, user.FullName, user.UserName, user.ProfilePhoto, permissions);

            _authHelper.Signin(authViewModel);

            return true;
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }


    }
}
