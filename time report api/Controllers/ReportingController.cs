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
    // Kalla på Authorize vid specifika metoder för att begränsa dem,
    // sätter man Authorize på hela kontrollern blir alla metoder begränsade och kräver login
    [Authorize]
    [ApiController]
    public class ReportingController : ControllerBase
    {

        private UnitOfWork unitOfWork;
        public ReportingController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize]
        [Route("AddTimeReport")]
        public ActionResult<User> AddTimeReport( )
        {
            try
            {
                return BadRequest("");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        [HttpGet]
        [Route("Bajs")]
        public ActionResult Bajs()
        {
            return StatusCode(500, "Something went wrong!");
        }
    }
}