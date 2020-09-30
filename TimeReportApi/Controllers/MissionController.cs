using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using TimeReportApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeReportApi.Controllers
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
        public IEnumerable<MissionViewModel> GetAllMissionByUserId(int id)
        {
            List<MissionMember> missionMemberList= unitOfWork.MissionMemberRepository.GetAllByUserId(id);
            List<MissionViewModel> mvmList = new List<MissionViewModel>();
            for(int i=0;i< missionMemberList.Count;i++)
            {
                Mission mission = unitOfWork.MissionRepository.GetById(missionMemberList[i].missionId);
                mvmList.Add(new MissionViewModel().ConvertToViewModel(mission));
            }
            return mvmList;

        }
        /// <summary>
        /// This method returns all users associated to the mission id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IEnumerable<User> </returns>
        [HttpGet]
        [Route("GetAllUserByMissionId/{id:int}")]
        public IEnumerable<UserViewModel> GetAllUserByMissionId(int id)
        {
            List<MissionMember> missionMemberList = unitOfWork.MissionMemberRepository.GetAllByMissionId(id);
            List<UserViewModel> userList = new List<UserViewModel>();
            for (int i = 0; i < missionMemberList.Count; i++)
            {
                User user = unitOfWork.UserRepository.GetById(missionMemberList[i].missionId);
                userList.Add(new UserViewModel().ConvertToViewModel(user));
            }
            return userList;
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
        /// <returns> ActionResult </returns>
        [HttpPost]
        [Route("AddMissionMember")]
        public ActionResult AddMissionMember([FromBody] MissionMember _missionMember)
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


        /// <summary>
        /// This method adds a favorite mission to repository. 
        /// </summary>
        /// <param name=" favoriteMission"></param>
        /// <returns> ActionResult </returns>

        [HttpPost]
        [Route("AddFavoriteMission")]
        public ActionResult AddFavoriteMission([FromBody] FavoriteMission favoriteMission)
        {
            try
            {
                unitOfWork.FavoriteMissionRepository.Insert(favoriteMission);
                unitOfWork.FavoriteMissionRepository.Save();
                return Ok();
            }
            catch
            {
                return ValidationProblem();
            }
        }

        [HttpGet]
        [Route("GetFavoriteMissions/{id:int}")]
        public ActionResult<List<MissionViewModel>> GetFavoriteMissions(int id)
        {
            List<FavoriteMission> favoriteMissionList = unitOfWork.FavoriteMissionRepository.GetFavoriteMissionsById(id);
            List<MissionViewModel> mvmList = new List<MissionViewModel>();
            for (int i = 0; i < favoriteMissionList.Count; i++)
            {
                Mission mission = unitOfWork.MissionRepository.GetById(favoriteMissionList[i].missionId);
                mvmList.Add(new MissionViewModel().ConvertToViewModel(mission));
            }
            return mvmList;
        }

    }
}
