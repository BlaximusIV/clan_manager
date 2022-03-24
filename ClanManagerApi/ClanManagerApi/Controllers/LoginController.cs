using ClanManagerApi.Extensions;
using ClanManagerApi.Models.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClanManagerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration) =>
            _configuration = configuration;

        private Dictionary<string, string> Users = new Dictionary<string, string>
        {
            // Add Users Locally - Temporary solution until permanent user data datastore found
            
        };

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            Users.TryGetValue(user.UserName, out var result);

            if (result is null || result != user.Password)
                return Unauthorized();

            var Claims = new List<Claim>
            {
                // Temporary, until another user data store is found
                new Claim("type", "Admin")
            };

            var securityConfiguration = _configuration.GetSecurityConfiguration();
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityConfiguration.SecurityKey));

            var Token = new JwtSecurityToken(
                securityConfiguration.TokenIssuer,
                securityConfiguration.TokenAudience,
                Claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256));

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(Token));
        }
    }
}
