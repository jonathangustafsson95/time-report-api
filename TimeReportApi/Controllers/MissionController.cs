using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TimeReportApi.Models;
using TimeReportApi.Models.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly User user;
        public MissionController(UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            int userId = Int32.Parse(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "userId").Value);
            user = unitOfWork.UserRepository.GetById(userId);
        }

        /// <summary>
        /// This method returns all missions when called 
        /// </summary >
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
        //[HttpGet]
        //[Route("GetAllMissionByUserId/{id:int}")]
        //public IEnumerable<MissionViewModel> GetAllMissionByUserId(int id)
        //{
        //    List<MissionMember> missionMemberList= unitOfWork.MissionMemberRepository.GetAllByUserId(id);
        //    List<MissionViewModel> mvmList = new List<MissionViewModel>();
        //    for(int i=0;i< missionMemberList.Count;i++)
        //    {
        //        Mission mission = unitOfWork.MissionRepository.GetById(missionMemberList[i].MissionId);
        //        mvmList.Add(new MissionViewModel().ConvertToViewModel(mission));
        //    }
        //    return mvmList;

        //}
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
                User user = unitOfWork.UserRepository.GetById(missionMemberList[i].MissionId);
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
        [Route("GetAllMissionByMissionName/{name}")]
        public IEnumerable<Mission> GetAllMissionByMissionName(string name)
        {
            List<Mission> missionList = unitOfWork.MissionRepository.Search<Mission>(x => x.MissionName, name);
            return missionList;
        }

        [HttpGet]
        [Route("GetAllMissionByCustomerName/{name}")]
        public IEnumerable<Mission> GetAllMissionByCustomerName(string name)
        {
            List<Customer> customerList = unitOfWork.CustomerRepository.Search<Customer>(x => x.Name, name);
            List<Mission> missionsLinkedToCustomerID = new List<Mission>();
            foreach (var customer in customerList)
            {
                List<Mission> allByCustomerId = unitOfWork.MissionRepository.GetAllByCustomerId(customer.CustomerId);
                missionsLinkedToCustomerID.Concat(allByCustomerId);
            }

            return missionsLinkedToCustomerID;
        }

        [HttpGet]
        [Route("GetAllMissionsBySearchString/{searchString}")]
        public IEnumerable<MissionTaskViewModel> GetAllMissionsBySearchString(string searchString)
        {
            string lowerCaseSearchString = searchString.ToLower();
            List<Mission> missionList = unitOfWork.MissionRepository.Search<Mission>(x => x.MissionName.ToLower(), lowerCaseSearchString);
            List<Customer> customerList = unitOfWork.CustomerRepository.Search<Customer>(x => x.Name.ToLower(), lowerCaseSearchString);
            List<Mission> missionsLinkedToCustomerID = new List<Mission>();

            foreach (var customer in customerList)
            {
                List<Mission> allByCustomerId = unitOfWork.MissionRepository.GetAllByCustomerId(customer.CustomerId);
                missionsLinkedToCustomerID.Concat(allByCustomerId);
            }

            foreach (var missionMission in missionList)
            {
                foreach (var customerMission in missionsLinkedToCustomerID)
                {
                    if (missionMission.MissionId == customerMission.MissionId)
                    {
                        missionList.Remove(missionMission);
                    }
                }
            }

            missionList.Concat(missionsLinkedToCustomerID);

            List<MissionTaskViewModel> missionTasksViewModelList = new List<MissionTaskViewModel>();

            foreach (var mission in missionList )
            {
                List<TaskViewModel> taskViewModelList = new List<TaskViewModel>();

                foreach (Task task in unitOfWork.TaskRepository.GetAllByMissionId(mission.MissionId))
                {
                    TaskViewModel taskVM = new TaskViewModel
                    {
                        TaskId = task.TaskId,
                        MissionId = task.MissionId,
                        UserId = task.UserId,
                        Name = task.Name,
                        Description = task.Description,
                    };
                    taskViewModelList.Add(taskVM);
                }

                MissionTaskViewModel missionsVM = new MissionTaskViewModel
                {
                    MissionName = mission.MissionName,
                    MissionId = mission.MissionId,
                    MissionColor = mission.Color,
                    StartDate = mission.Start,
                    Description = mission.Description,
                    Customer = unitOfWork.CustomerRepository.GetById(mission.CustomerId).Name,
                    Tasks = taskViewModelList
                };
                missionTasksViewModelList.Add(missionsVM);
            }
            return missionTasksViewModelList;


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
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<MissionMemberController>/5
        [HttpDelete]
        [Route("DeleteMissionMember/{userId}/{missionId}")]
        public ActionResult DeleteMissionMember(int userId, int missionId)
        {
            //unitOfWork.MissionMemberRepository.Insert(new MissionMember() { userId=2,});
            unitOfWork.MissionMemberRepository.Delete(userId, missionId);
            unitOfWork.MissionMemberRepository.Save();
            return Ok();
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
        [HttpDelete]
        [Route("FavoriteMission")]
        public ActionResult DeleteFavoriteMission([FromBody] FavoriteMission favoriteMission)
        {
            try
            {
                unitOfWork.FavoriteMissionRepository.Delete(favoriteMission.UserId,favoriteMission.MissionId);
                unitOfWork.FavoriteMissionRepository.Save();
                return Ok();
            }
            catch
            {
                return ValidationProblem();
            }
        }

        [HttpGet]
        [Route("GetFavoriteMissions")]
        public ActionResult<List<MissionViewModel>> GetFavoriteMissions()
        {
            List<FavoriteMission> favoriteMissionList = unitOfWork.FavoriteMissionRepository.GetFavoriteMissionsById(user.UserId);
            List<MissionViewModel> mvmList = new List<MissionViewModel>();
            for (int i = 0; i < favoriteMissionList.Count; i++)
            {
                Mission mission = unitOfWork.MissionRepository.GetById(favoriteMissionList[i].MissionId);
                Customer customer = unitOfWork.CustomerRepository.GetAll().FirstOrDefault(n => n.CustomerId == mission.CustomerId);
                mvmList.Add(new MissionViewModel().ConvertToViewModel(mission,customer));
            }
            return mvmList;
        }

        [HttpGet]
        [Route("GetAllTasksInAMission/{id:int}")]
        public IEnumerable<Task> GetAllTasksInAMission(int missionId)
        {
            IEnumerable<Task> allTasksForAMission = unitOfWork.TaskRepository.GetAll();
            var allTasksInAMission = allTasksForAMission.ToList();
            allTasksInAMission.Select(m => m.MissionId == missionId);
            return allTasksInAMission;
        }

        /// <summary>
        /// This method returns a list of MissionTaskViewModel which contains the missions and corresponding 
        /// tasks from which the user are a member of. 
        /// </summary>
        /// <returns>A list of MissionTaskViewModel items.</returns>
        [HttpGet]
        [Route("UserMissions")]
        public ActionResult<List<MissionTaskViewModel>> GetUserMissions()
        {
            List<MissionMember> missionMemberList = unitOfWork.MissionMemberRepository.GetAllByUserId(user.UserId);
            List<MissionTaskViewModel> missionTaskViewModel = new List<MissionTaskViewModel>();
            List<TaskViewModel> tasksViewModelList; 

            for (int i = 0; i < missionMemberList.Count; i++)
            {
                Mission mission = unitOfWork.MissionRepository.GetById(missionMemberList[i].MissionId);
                tasksViewModelList = new List<TaskViewModel>();

                foreach (Task task in unitOfWork.TaskRepository.GetAllByMissionId(mission.MissionId))
                {
                    TaskViewModel taskVM = new TaskViewModel
                    {
                        TaskId = task.TaskId,
                        MissionId = task.MissionId,
                        UserId = task.UserId,
                        Name = task.Name,
                        Description = task.Description,
                    };
                    tasksViewModelList.Add(taskVM);
                }
        
                MissionTaskViewModel missionsVM = new MissionTaskViewModel
                {
                    MissionName = mission.MissionName,
                    MissionId = mission.MissionId,
                    MissionColor = mission.Color,
                    StartDate = mission.Start,
                    Description = mission.Description,
                    Customer = unitOfWork.CustomerRepository.GetById(mission.CustomerId).Name,
                    Tasks = tasksViewModelList
                };
                missionTaskViewModel.Add(missionsVM);
            }
            return missionTaskViewModel;
        }
    }
}
