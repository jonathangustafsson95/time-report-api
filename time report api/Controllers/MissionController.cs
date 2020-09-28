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
        private readonly UnitOfWork unitOfWork;
        private readonly User dummy;
        public MissionController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            dummy = new User()
            {
                userId = 1,
                userName = "John",
                password = "abc123",
                eMail = "hej@lol.com"
            };
        }

        /// <summary>
        /// This method returns all missions when called 
        /// </summary>
        /// <param></param>
        /// <returns> IEnumerable<Mission> </returns>
        [HttpGet]
        [Route("GetAllMission")]
        public IEnumerable<Mission> GetAllMissions()
        {
            return unitOfWork.MissionRepository.GetAll();
        }
        /// <summary>
        /// This method returns all missions associated to the user id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IEnumerable<Mission> </returns>
        [HttpGet]
        [Route("GetAllMissionByUserId/{id:int}")]
        public IEnumerable<Mission> GetAllMissionByUserId(int id)
        {
            List<MissionMember> missionMemberList= unitOfWork.MissionMemberRepository.GetAllByUserId(id);
            List<Mission> missionList = new List<Mission>();
            foreach(MissionMember mm in missionMemberList)
            {
                missionList.Add(unitOfWork.MissionRepository.GetById(mm.missionId));
            }
            return missionList;
        }
        /// <summary>
        /// This method returns all users associated to the mission id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IEnumerable<Mission> </returns>
        [HttpGet]
        [Route("GetAllUserByMissionId/{id:int}")]
        public IEnumerable<User> GetAllUserByMissionId(int id)
        {
            List<MissionMember> missionMemberList = unitOfWork.MissionMemberRepository.GetAllByMissionId(id);
            List<User> missionList = new List<User>();
            foreach (MissionMember mm in missionMemberList)
            {
                missionList.Add(unitOfWork.UserRepository.GetById(mm.userId));
            }
            return missionList;
        }
        /// <summary>
        /// This method returns all missions associated to the mission name
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IEnumerable<Mission> </returns>
        [HttpGet]
        [Route("GetAllUserByMissionName/{name}")]
        public IEnumerable<Mission> GetAllMissionByMissionName(string name)
        {
            List<Mission> missionList = unitOfWork.MissionRepository.Search<Mission>(x => x.missionName, name);
            return missionList;
        }

        /// <summary>
        /// This method adds a missionmember to the table when called
        /// </summary>
        /// <param name=" _missionMember"></param>
        /// <returns> ActionResult> </returns>
        [HttpPost]
        [Route("AddMissionMember")]
        public ActionResult Post([FromBody] MissionMember _missionMember)
        {
            try
            {
                unitOfWork.MissionMemberRepository.Insert(_missionMember);
                unitOfWork.MissionMemberRepository.Save();
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
