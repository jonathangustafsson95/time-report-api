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
        private readonly IUnitOfWork unitOfWork;
        private readonly User user;
        public MissionController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            int userId = Int32.Parse(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "userId").Value);
            user = unitOfWork.UserRepository.GetById(userId);
        }
        [HttpGet]
        [Route("SearchMission/{searchString}")]
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
            List<MissionMember> mmList = unitOfWork.MissionMemberRepository.GetAllByUserId(user.UserId);

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
                    Tasks = taskViewModelList,
                    isMember = false,
 
                };

                if (mmList.FirstOrDefault(n => n.MissionId == mission.MissionId) != null) 
                {
                    missionsVM.isMember = true;
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
        [Route("MissionMember/{missionId}")]
        public ActionResult<HttpResponse> AddMissionMember(int missionId)
        {
            try
            {
                if (unitOfWork.MissionRepository.Exists(missionId))
                {
                    unitOfWork.MissionMemberRepository.Insert(new MissionMember() { UserId = user.UserId, MissionId = missionId });
                    unitOfWork.MissionMemberRepository.Save();
                    return Ok();
                }
                else 
                    throw new Exception();
               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<MissionMemberController>/5
        [HttpDelete]
        [Route("MissionMember/{missionId}")]
        public ActionResult DeleteMissionMember(int missionId)
        {
            try
            {
                //unitOfWork.MissionMemberRepository.Insert(new MissionMember() { userId=2,});
                unitOfWork.MissionMemberRepository.Delete(user.UserId, missionId);
                unitOfWork.MissionMemberRepository.Save();
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500,e.Message);
            }
        }

        /// <summary>
        /// This method adds a favorite mission to repository. 
        /// </summary>
        /// <param name=" favoriteMission"></param>
        /// <returns> ActionResult </returns>

        [HttpPost]
        [Route("FavoriteMission/{missionId}")]
        public ActionResult AddFavoriteMission(int missionId)
        {
            try
            {
                unitOfWork.FavoriteMissionRepository.Insert(new FavoriteMission() { UserId = user.UserId, MissionId = missionId});
                unitOfWork.FavoriteMissionRepository.Save();
                return Ok();
            }
            catch
            {
                return ValidationProblem();
            }
        }
        [HttpDelete]
        [Route("FavoriteMission/{missionId}")]
        public ActionResult DeleteFavoriteMission(int missionId)
        {
            try
            {
                unitOfWork.FavoriteMissionRepository.Delete(user.UserId, missionId);
                unitOfWork.FavoriteMissionRepository.Save();
                return Ok();
            }
            catch
            {
                return ValidationProblem();
            }
        }

        [HttpGet]
        [Route("FavoriteMissions")]
        public ActionResult<List<MissionViewModel>> GetFavoriteMissions()
        {
            try
            {
                List<FavoriteMission> favoriteMissionList = unitOfWork.FavoriteMissionRepository.GetFavoriteMissionsById(user.UserId);
                List<MissionViewModel> mvmList = new List<MissionViewModel>();
                for (int i = 0; i < favoriteMissionList.Count; i++)
                {
                    Mission mission = unitOfWork.MissionRepository.GetById(favoriteMissionList[i].MissionId);
                    Customer customer = unitOfWork.CustomerRepository.GetAll().FirstOrDefault(n => n.CustomerId == mission.CustomerId);
                    mvmList.Add(new MissionViewModel().ConvertToViewModel(mission, customer));
                }
                return mvmList;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method returns a list of MissionTaskViewModel which contains the missions and corresponding 
        /// tasks from which the user are a member of. 
        /// </summary>
        /// <returns>A list of MissionTaskViewModel items.</returns>
        [HttpGet]
        [Route("UserMissions/{taskId}")]
        public ActionResult<List<MissionTaskViewModel>> GetUserMissions(int taskId)
        {
            try
            {
                List<MissionMember> missionMemberList = unitOfWork.MissionMemberRepository.GetAllByUserId(user.UserId);
                List<MissionTaskViewModel> missionTaskViewModel = new List<MissionTaskViewModel>();
                List<TaskViewModel> tasksViewModelList;
                if (taskId != 0)
                {
                    Task taskSelected = unitOfWork.TaskRepository.GetById(taskId);
                    int idx = missionMemberList.FindIndex(f => f.MissionId == taskSelected.MissionId);
                    if (idx < 0)
                    {
                        missionMemberList.Add(new MissionMember { UserId = user.UserId, MissionId = (int)taskSelected.MissionId });
                    }
                }

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
                        isMember = true,
                        Tasks = tasksViewModelList
                    };
                    missionTaskViewModel.Add(missionsVM);
                }
                return missionTaskViewModel;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method returns a  MissionTaskViewModel which contains the mission and corresponding 
        /// tasks and users for a given ID. 
        /// </summary>
        /// <returns>A MissionTaskViewModel.</returns>
        [HttpGet]
        [Route("SpecificMission/{missionid}")]
        public ActionResult<MissionTaskViewModel> GetSpecificMission(int missionId)
        {
            try
            {
                Mission mission = unitOfWork.MissionRepository.GetById(missionId);
                List<TaskViewModel> tasksViewModelList = new List<TaskViewModel>();
                List<UserViewModel> usersVM = new List<UserViewModel>();

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

                List<MissionMember> missionMembers = unitOfWork.MissionMemberRepository.GetAllByMissionId(missionId);
                foreach (MissionMember missionMember in missionMembers)
                {
                    User userInMission = unitOfWork.UserRepository.GetById(missionMember.UserId);
                    UserViewModel userVm = new UserViewModel
                    {
                        UserId = userInMission.UserId,
                        UserName = userInMission.UserName,
                        EMail = userInMission.EMail
                    };
                    usersVM.Add(userVm);
                }

                MissionTaskViewModel missionVM = new MissionTaskViewModel
                {
                    MissionName = mission.MissionName,
                    MissionId = mission.MissionId,
                    MissionColor = mission.Color,
                    StartDate = mission.Start,
                    Description = mission.Description,
                    Customer = unitOfWork.CustomerRepository.GetById(mission.CustomerId).Name,
                    isMember = true,
                    Tasks = tasksViewModelList,
                    Users = usersVM
                };
                return missionVM;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
