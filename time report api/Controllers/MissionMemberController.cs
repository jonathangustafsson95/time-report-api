using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionMemberController : ControllerBase
    {
        private readonly UnitOfWork UnitOfWork;
        private readonly User dummy;
        public MissionMemberController(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            dummy = new User()
            {
                userId = 1,
                userName = "John",
                password = "abc123",
                eMail = "hej@lol.com"
            };
        }
        // GET: api/<MissionMemberController>
        
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<MissionMemberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        //[FromBody]

        [HttpPost]
        [Route("AddMissionMember/{_missionId}")]
        public void Post( int _missionId)
        {
            int missionId = _missionId;


        }

        // PUT api/<MissionMemberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MissionMemberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
