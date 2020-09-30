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
using time_report_api.Models;
using DataAccessLayer;

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private UnitOfWork unitOfWork;
        public SystemController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<User> LoginUser(string userName, string passWord)
        {
            try
            {
                IActionResult respone = Unauthorized();
                LoginRequest login = new LoginRequest
                {
                    userName = userName,
                    passWord = passWord
                };

                var user = unitOfWork.UserRepository.GetByName(login.userName).SingleOrDefault();

                if (user.userName == login.userName && user.password == login.passWord)
                {
                }

                return BadRequest("Wrong username/password");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }
    }
}
