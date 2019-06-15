using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.ICommands.IUserCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IGetUserLoginCommand _getLoginUser;

        public LoginController(IConfiguration config, IGetUserLoginCommand getLoginUser)
        {
            _config = config;
            _getLoginUser = getLoginUser;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]UserLoginDto login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim("UserId", userInfo.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddHours(24),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto AuthenticateUser(UserLoginDto login)
        {
            //if (user.Email == "Jignesh")
            //{
            //    user = new UserDto { Email = "test.btest@gmail.com" };
            //}
            var user = _getLoginUser.Execute(login.Email);
            return user;
        }
    }
}