
using AuthApi.Common;
using AuthApi.Common.com;
using AuthApi.EFcore;
using AuthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace AuthApi.Application
{
    public class UserApplication : IUserApplication
    {

        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passHasher;
     
        private readonly ProContext _proContext;

        public UserApplication(IAuthHelper authHelper, IPasswordHasher passHasher, ProContext proContext)
        {
            _authHelper = authHelper;
            _passHasher = passHasher;
            _proContext = proContext;
        
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


            var user = new User(registerModel.UserName, pass, registerModel.FullName, registerModel.FName,
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

         

            user.Edit(editModel.UserName, password, editModel.FullName, editModel.FName, "", editModel.RoleId);
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

        public string Login(Login loginModel)
        {
            var user = _proContext.Users.FirstOrDefault(x => x.UserName == loginModel.UserName && x.Password==loginModel.Password);

            if (user == null)
            {
                var res = JsonConvert.SerializeObject(new LoginResult { Success = false, TokenString = "" });
                return res;
            }



            var permissions = _proContext.Roles.Find(user.RoleId)
              .Permissions
              .Select(x => x.Code)
              .ToList();


            var authViewModel = new AuthViewModel(user.Id, user.RoleId, user.FullName, user.UserName, user.ProfilePhoto, permissions);

            var authRes = _authHelper.Signin(authViewModel);

            return JsonConvert.SerializeObject(authRes);





            //Old version

            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            //var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            //var tokenOptions = new JwtSecurityToken(
            //    issuer: "https://localhost:7102",
            //    claims: new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name,loginModel.UserName),
            //        new Claim(ClaimTypes.Role,"Admin"),
            //    },
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: signInCredentials
            //    );
            //var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);


        }


        //public bool Login(Login loginModel)
        //{
        //    var user = _proContext.Users.FirstOrDefault(x => x.UserName == loginModel.UserName);

        //    if (user == null)
        //        return false;

        //    //var res = _passHasher.Check(user.Password,loginModel.Password);

        //    //if(!res.Verified) return false;

        //    var permissions = _proContext.Roles.Find(user.RoleId)
        //      .Permissions
        //      .Select(x => x.Code)
        //      .ToList();


        //    var authViewModel = new AuthViewModel(user.Id, user.RoleId, user.FullName, user.UserName, user.ProfilePhoto, permissions);

        //    _authHelper.Signin(authViewModel);

        //    return true;
        //}

        public void Logout()
        {
            _authHelper.SignOut();
        }


    }
}
