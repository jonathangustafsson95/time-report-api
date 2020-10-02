using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.UnitOfWork;
using CommonLibrary.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeReportApi.Models;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace TimeReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private UnitOfWork unitOfWork;
        private readonly IConfiguration _config;

        public SystemController(UnitOfWork unitOfWork, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromBody]User login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            };

            return response;
        }

        User AuthenticateUser(User loginCredentials)
        {
            User user = unitOfWork.UserRepository.GetByName(loginCredentials.UserName).SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }

        string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("userName", userInfo.UserName.ToString()),
                new Claim("role", userInfo.Role),
                new Claim("userId", userInfo.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
