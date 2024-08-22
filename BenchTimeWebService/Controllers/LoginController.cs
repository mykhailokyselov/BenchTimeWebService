using BenchTimeWebService.DTO;
using BenchTimeWebService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BenchTimeWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            // For demonstration, assume these are the correct credentials
            var user = new User { Username = "admin", Password = "password" };
            var loginDto = new LoginDto { Username = "admin", Password = "password" };
            if (loginDto.Username == user.Username && loginDto.Password == user.Password)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretKey123456789MikeSuperLol"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.NameIdentifier, user.Username),
        //    new Claim(ClaimTypes.Name, user.Username)
        //};

            var token = new JwtSecurityToken(
                issuer: "Mike",
                audience: "All",
                //claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
