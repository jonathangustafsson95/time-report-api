using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        [HttpGet]
        [Route("Bajs")]
        public ActionResult Bajs()
        {
            return StatusCode(500, "Something went wrong!");
        }
    }
}