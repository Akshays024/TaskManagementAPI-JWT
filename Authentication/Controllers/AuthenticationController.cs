using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private static List<User> users = new List<User>();
        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            var existingUser = users.FirstOrDefault(s => s.UserName == user.UserName);
            if(existingUser != null)
            {
                return BadRequest("user already Exist");
            }
            users.Add(user);
            return Ok("registered successfully");
        }
        [HttpPost("login")]
        public IActionResult Login(LOgin login)
        {
            var user = users.FirstOrDefault(s => s.UserName == login.UserName && s.Password == login.Password);
            if(user == null)
            {
                return BadRequest("invalid username or password");
            }
            var token = GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens = refreshToken;
            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken
            });
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("protected");
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshToken refreshToken)
        {
            var user = users.FirstOrDefault(s => s.RefreshTokens == refreshToken.RefreshTokens);
            if(user == null)
            {
                return Unauthorized();
            }
            var newAccessToken = GenerateToken(user);
            return Ok(new
            {
                AccessToken = newAccessToken
            });


        }
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-jwt-secret-key-12345-this-is-for-demo-2006"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(1), signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
