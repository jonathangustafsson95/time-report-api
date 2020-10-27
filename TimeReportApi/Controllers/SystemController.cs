using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.UnitOfWork;
using CommonLibrary.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TimeReportApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _config;

        public SystemController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            _config = config;
        }

        /// <summary>
        /// This method handles user log ins and returns a Ok response if 
        /// credentials exist in database.
        /// </summary>
        /// <param name="login"></param>
        /// <returns>response with token and user details.</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromBody]User login)
        {
            try
            {

                IActionResult response = Unauthorized(new { message = "Invalid credentials..." });
                User user = AuthenticateUser(login);
                if (user != null)
                {
                    var tokenString = GenerateJWTToken(user);
                    response = Ok(new
                    {
                        token = tokenString,
                        userDetails = new User() { UserId = user.UserId, UserName = user.UserName },
                    });
                };

                return response;
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
            }
        }

        /// <summary>
        /// This method is a helper method which returns a user, null if the 
        /// user doesn't exist in the database.
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <returns>A user</returns>
        private User AuthenticateUser(User loginCredentials)
        {
            User user = unitOfWork.UserRepository.GetByName(loginCredentials.UserName).SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }

        /// <summary>
        /// This method generates a token with claims in it. It uses 
        /// parameters from appsettings file.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Tokenstring</returns>
        private string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("userName", userInfo.UserName.ToString()),
                new Claim("userId", userInfo.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), //30 minutes for token validity
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
