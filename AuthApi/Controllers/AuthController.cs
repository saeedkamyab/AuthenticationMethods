using AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Login loginModel)
        {
            if (loginModel.UserName != "admin" && loginModel.Password != "1234")
            {
                return Unauthorized();
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7102",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name,loginModel.UserName),
                    new Claim(ClaimTypes.Role,"Admin"),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signInCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            //return Ok(tokenString);
            return Ok(tokenString);

        }
    }
}
