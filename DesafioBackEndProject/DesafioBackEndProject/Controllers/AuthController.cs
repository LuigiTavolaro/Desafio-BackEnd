using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace DesafioBackEndProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            // Simulação de autenticação
            if (login.Username == "admin" && login.Password == "password")
            {
                var token = GenerateToken("admin");
                return Ok(new { token });
            }
            else if (login.Username == "driver" && login.Password == "password")
            {
                var token = GenerateToken("driver");
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateToken(string role)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role) // Incluindo o role no token
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FWM4sX5KV8pko3rkhOxoeGPHqeL7z2niLCZCcrRmimdnz0tdA0vMxRnXkHGWEket"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "xyz.com",
                audience: "xyz.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
