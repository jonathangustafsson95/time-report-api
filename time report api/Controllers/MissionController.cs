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
    public class MissionController : ControllerBase
    {
        private readonly UnitOfWork UnitOfWork;
        private readonly User dummy;
        public MissionController(UnitOfWork unitOfWork)
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
      
        
        [HttpGet]
        [Route("GetAllMissionMember")]
        public IEnumerable<MissionMember> Get()
        {


            return UnitOfWork.MissionMemberRepository.GetAll().ToList();
        }
    

        // GET api/<MissionMemberController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        

        [HttpPost]
        [Route("AddMissionMember")]
        public ActionResult Post([FromBody] MissionMember _missionMember)
        {
            try
            {
                UnitOfWork.MissionMemberRepository.Insert(_missionMember);
                UnitOfWork.MissionMemberRepository.Save();
                return Ok();
            }
            catch
            {
                return ValidationProblem();
            }
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
