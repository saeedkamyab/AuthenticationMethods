using System.Security.Claims;

namespace AuthApi.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string TokenString { get; set; }
        public List<Claim> claims { get; set; } = new List<Claim>();

    }
}
