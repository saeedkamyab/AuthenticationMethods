using AuthApi.Application;
using AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserApplication _userApp;

        public AuthController(IUserApplication userApp)
        {
            _userApp = userApp;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Login loginModel)
        {
            var loginResult = _userApp.Login(loginModel);

            if (loginResult == null)
            {
                return Unauthorized();
            }
            else
            {

                return Ok(loginResult);

            }
        }
    }
}
