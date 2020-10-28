using System;
using System.Collections.Generic;
using System.Linq;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeReportApi.Models;
using TimeReportApi.Models.ViewModel;
using CommonLibrary.ErrorMessage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeReportApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly User user;
        public MissionController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            int userId = Int32.Parse(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "userId").Value);
            user = new User { UserId = userId };
        }

        /// <summary>
        /// This method searches for missions based on a searchstring. It matches the searchstring 
        /// against either Mission name or Customer name.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns> A list of MissionTaskViewModel items. </returns>
        [HttpGet]
        [Route("SearchMission/{searchString}")]
        public ActionResult<IEnumerable<MissionTaskViewModel>> GetAllMissionsBySearchString(string searchString)
        {
            try
            {
                List<MissionTaskViewModel> missionTasksViewModelList = new List<MissionTaskViewModel>();
                if (searchString.Trim().Length == 0)
                    return missionTasksViewModelList;

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

                List<MissionMember> mmList = unitOfWork.MissionMemberRepository.GetAllByUserId(user.UserId);

                foreach (var mission in missionList)
                {
                    MissionTaskViewModel missionsVM = new MissionTaskViewModel
                    {
                        MissionName = mission.MissionName,
                        MissionId = mission.MissionId,
                        MissionColor = mission.Color,
                        StartDate = mission.Start,
                        Description = mission.Description,
                        Customer = unitOfWork.CustomerRepository.GetById(mission.CustomerId).Name,
                        Tasks = GetAllTasks(mission.MissionId),
                        IsMember = false,
                    };

                    if (mmList.FirstOrDefault(n => n.MissionId == mission.MissionId) != null)
                    {
                        missionsVM.IsMember = true;
                    };
                    missionTasksViewModelList.Add(missionsVM);
                }
                return missionTasksViewModelList;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ErrorMessage.DatabaseCommunicationError);
            }
        }

        /// <summary>
        /// This method adds a missionmember to the database when called
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns> ActionResult<HttpResponse> </returns>
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
                    throw new ArgumentException(ErrorMessage.MissionDoesNotExistError);

            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
            }
        }
        /// <summary>
        /// This method deletes a member from the database when called. Which
        /// user to link to the missionmember-row in the database-table is received
        /// from token.
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns> ActionResult<HttpResponse> </returns>
        [HttpDelete]
        [Route("MissionMember/{missionId}")]
        public ActionResult<HttpResponse> DeleteMissionMember(int missionId)
        {
            try
            {
                if (unitOfWork.MissionRepository.Exists(missionId))
                {
                    unitOfWork.MissionMemberRepository.Delete(user.UserId, missionId);
                    unitOfWork.MissionMemberRepository.Save();
                    return Ok();
                }
                else
                    throw new ArgumentException("Mission does not exist");
            }
            catch (ArgumentException e)
            {
                return StatusCode(400, e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
            }


        }

        /// <summary>
        /// This method adds a favorite mission to database. Which
        /// user to link to the FavoriteMission-row in the database-table is received
        /// from token.
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns> ActionResult<HttpResponse> </returns>

        [HttpPost]
        [Route("FavoriteMission/{missionId}")]
        public ActionResult<HttpResponse> AddFavoriteMission(int missionId)
        {
            try
            {
                if (unitOfWork.MissionRepository.Exists(missionId))
                {
                    unitOfWork.FavoriteMissionRepository.Insert(new FavoriteMission() { UserId = user.UserId, MissionId = missionId });
                    unitOfWork.FavoriteMissionRepository.Save();
                    return Ok();
                }
                else
                    throw new ArgumentException("Mission does not exist");
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest,e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
            }
        }

        /// <summary>
        /// This method deletes a favorite mission from database. Which
        /// user to link to the FavoriteMission-row in the database-table is received
        /// from token.
        /// </summary>
        /// <param name="missionId"></param>
        /// <returns> ActionResult<HttpResponse> </returns>
        [HttpDelete]
        [Route("FavoriteMission/{missionId}")]
        public ActionResult<HttpResponse> DeleteFavoriteMission(int missionId)
        {
            try
            {
                if (unitOfWork.MissionRepository.Exists(missionId))
                {
                    unitOfWork.FavoriteMissionRepository.Delete(user.UserId, missionId);
                    unitOfWork.FavoriteMissionRepository.Save();
                    return Ok();
                }
                else
                {
                    throw new ArgumentException("Mission does not exist");
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
            }
        }

        /// <summary>
        /// This method gets all the favorite missions for a specific user. Which
        /// user is received from token.
        /// </summary>
        /// <param></param>
        /// <returns> A list of MissionViewModel items. </returns>
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
                    List<TaskViewModel>tasksViewModelList =GetAllTasks(mission.MissionId);
                    mvmList.Add(new MissionViewModel().ConvertToViewModel(mission, customer,tasksViewModelList));
                }
                
                return mvmList;
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
            }
        }

        /// <summary>
        /// This method returns a list of MissionTaskViewModel which contains the missions and corresponding 
        /// tasks from which the user are a member of. Which user is received from token.
        /// </summary>
        /// <param name="taskId"></param>
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
                    tasksViewModelList = GetAllTasks(mission.MissionId);
                    MissionTaskViewModel missionsVM = new MissionTaskViewModel
                    {
                        MissionName = mission.MissionName,
                        MissionId = mission.MissionId,
                        MissionColor = mission.Color,
                        StartDate = mission.Start,
                        Description = mission.Description,
                        Customer = unitOfWork.CustomerRepository.GetById(mission.CustomerId).Name,
                        IsMember = true,
                        Tasks = tasksViewModelList
                    };
                    missionTaskViewModel.Add(missionsVM);
                }
                return missionTaskViewModel;
            
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occured when trying to communicate with the database." });
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
                if (!unitOfWork.MissionRepository.Exists(missionId))
                    throw new Exception();
                Mission mission = unitOfWork.MissionRepository.GetById(missionId);
                List<TaskViewModel> tasksViewModelList = GetAllTasks(mission.MissionId);
                List<UserViewModel> usersVM = new List<UserViewModel>();
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
                    IsMember = true,
                    Tasks = tasksViewModelList,
                    Users = usersVM
                };
                return missionVM;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// This method returns a  list of TaskViewModekl which contains the mission´s tasks 
        /// </summary>
        /// <returns>A list of TaskViewModel.</returns>
        private List<TaskViewModel> GetAllTasks(int missionId)
    {
        List<TaskViewModel> tasksViewModelList = new List<TaskViewModel>();
        foreach (Task task in unitOfWork.TaskRepository.GetAllByMissionId(missionId))
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
            return tasksViewModelList;
    }
    }
}

       