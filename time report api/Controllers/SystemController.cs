using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.UnitOfWork;
using CommonLibrary.Model;

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
        public ActionResult<User> LoginUser(User user)
        {
            try
            {
                // Hade vart smidigare att ha en GetByName? ist för att kolla alla users.
                var DBusers = unitOfWork.UserRepository.GetAll();

                foreach (var u in DBusers)
                {
                    if (u.userName.ToLower().Equals(user.userName.ToLower()) && u.password.Equals(user.password))
                    {
                        //Returnera en Token för att vidare kunna kommunicera med API?
                        return u;
                    }
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
