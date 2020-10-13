using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.UnitOfWork;
using CommonLibrary.Model;
using TimeReportApi.Models;
using DataAccessLayer;
using System.Security.Claims;
using TimeReportApi.Models.ViewModel;
using System.Globalization;

namespace TimeReportApi.Controllers
{
    /// <summary>
    /// Controller for handlings rerporting events.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly User user;
        private readonly IUnitOfWork unitOfWork;
        public ReportingController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            int userId = Int32.Parse(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "userId").Value);
            user = unitOfWork.UserRepository.GetById(userId);
            unitOfWork.UserRepository.GetById(1);
        }

        /// <summary>
        /// This method takes a registries class from body of request, extracts
        /// registry items from registries and inserts new entries and updates 
        /// entries that have been changed.
        /// </summary>
        /// <param name="newRegistries"></param>
        /// <returns>Http response message</returns>
        [HttpPost]
        [Route("TimeReport")]
        public ActionResult<HttpResponse> AddTimeReport([FromBody] Registries newRegistries)
        {
            try
            {
                for (int i = 0; i < newRegistries.RegistriesToReport.Count; i++)
                {
                    if (newRegistries.RegistriesToReport[i].UserId != user.UserId)
                        throw new Exception("You are trying to edit someone elses registries!");

                    // En int kan aldrig  vara  null, så om vi skickar nya registries
                    // bör vi hantera det på något sätt i JSON, typ  sätta regID  till 0?
                    if (newRegistries.RegistriesToReport[i].RegistryId == 0)
                    {
                        // Add new registry
                        unitOfWork.RegistryRepository.Insert(newRegistries.RegistriesToReport[i]);
                    }
                    else 
                    {
                        // Change  a existing registry
                        unitOfWork.RegistryRepository.Update(newRegistries.RegistriesToReport[i]);
                    }
                }
                unitOfWork.RegistryRepository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method takes a list of registry ids from body of request, extracts
        /// and deletes the entries from the database.
        /// </summary>
        /// <param name="registryIdsToDelete"></param>
        /// <returns>Http response message</returns>
        [HttpDelete]
        [Route("TimeReport")] 
        public ActionResult<HttpResponse> DeleteTimeReport([FromBody]RegistriesDelete registryIdsToDelete)
        {
            try
            {
                for (int i = 0; i < registryIdsToDelete.RegistriesToDelete.Count; i++)
                {
                    if (unitOfWork.RegistryRepository.GetById(registryIdsToDelete.RegistriesToDelete[i]).UserId != user.UserId)
                        throw new Exception("You are trying to delete someone elses registries!");

                    unitOfWork.RegistryRepository.Delete(unitOfWork.RegistryRepository.GetById(registryIdsToDelete.RegistriesToDelete[i]).RegistryId);
                }
                unitOfWork.RegistryRepository.Save();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method returns a list of RegistryViewModel items extracted from the database
        /// for the week in which provided date exists.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>A list of RegistryViewModel items.</returns>
        [HttpGet]
        [Route("Week/{dateTime}")]
        public ActionResult<List<RegistryViewModel>> GetWeek(DateTime dateTime)
        {
            try
            {
                DateTime startDate = GetWeekStartDate(dateTime, DayOfWeek.Monday);
                DateTime endDate = startDate.AddDays(7);

                List<Registry> weekRegistries = unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate, user.UserId);
                return (ConvertRegistriesToViewModel(weekRegistries));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        /// <summary>
        /// This method finds the date of the first day in a week which it the returns.
        /// The method takes a optional argument id week should start on a day that is 
        /// not equal to Monday.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="startDay"></param>
        /// <returns>DateItem start date for week in question.</returns>
        private DateTime GetWeekStartDate(DateTime dateTime, DayOfWeek startDay = DayOfWeek.Monday)
        {
            DateTime startDate = dateTime;
            while (startDate.DayOfWeek != startDay)
            {
                startDate = startDate.AddDays(-1);
            }
            return startDate;
        }

        /// <summary>
        /// This method takes a list of registry items, collects additional information 
        /// for each registry and returns a ViewModel.
        /// </summary>
        /// <param name="registries"></param>
        /// <returns>A list of RegistryViewModel items.</returns>
        private List<RegistryViewModel> ConvertRegistriesToViewModel(List<Registry> registries, bool suggestion = false)
        {
            List<RegistryViewModel> weekRegistries = new List<RegistryViewModel>();
            Task task;
            Mission mission;
            RegistryViewModel registryViewModel;

            foreach (var reg in registries)
            {
                registryViewModel = new RegistryViewModel();
                
                if (reg.TaskId == null)
                {
                    registryViewModel.TaskId = null;
                    registryViewModel.MissionName = "Internal time";
                }
                else
                {
                    task = unitOfWork.TaskRepository.GetById(reg.TaskId);
                    mission = unitOfWork.MissionRepository.GetById(task.MissionId);

                    registryViewModel.TaskName = task.Name;
                    registryViewModel.MissionName = mission.MissionName;
                    registryViewModel.MissionColor = mission.Color;
                    registryViewModel.Invoice = task.Invoice;
                    registryViewModel.TaskId = task.TaskId;
                }

                registryViewModel.Day = reg.Date.DayOfWeek;
                registryViewModel.Hours = reg.Hours;
                registryViewModel.Created = reg.Created;
                registryViewModel.Date = reg.Date;

                if (!suggestion)
                    registryViewModel.RegistryId = reg.RegistryId;

                weekRegistries.Add(registryViewModel);
            }
            return weekRegistries;
        }

        /// <summary>
        /// This method returns a list of RegistryViewModel items extracted from the database
        /// for the 5 last registries from the user.
        /// </summary>
        /// <returns>A list of RegistryViewModel items.</returns>
        [HttpGet]
        [Route("LatestRegistries")]
        public ActionResult<List<RegistryViewModel>> GetLatestregistries()
        {
            // Set nr of registries to collect from DB
            int nrOfRegistries = 5;
            try
            {
                return (ConvertRegistriesToViewModel(unitOfWork.RegistryRepository.GetLatestRegistries(nrOfRegistries, user.UserId)));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method returns a list of WeekTemplateViewModel which in turn holds RegistryViewModels 
        /// for the latest weeks. Items are extracted from the database for 5 weeks prior to todays date.
        /// </summary>
        /// <param name="todaysDate"></param>
        /// <returns>A list of WeekTemplateViewModel items.</returns>
        [HttpGet]
        [Route("WeekTemplates/{todaysDate}")]
        public ActionResult<List<WeekTemplateViewModel>> GetWeekTemplates(DateTime todaysDate)
        {
            int nrOfWeeks = 5;
            try
            {
                List<WeekTemplateViewModel> weekTemplates = new List<WeekTemplateViewModel>();
                List<Registry> weekRegistries = new List<Registry>();
                DateTime startDate = GetWeekStartDate(todaysDate, DayOfWeek.Monday);
                DateTime endDate;

                for (int i = 0; i < nrOfWeeks; i++)
                {
                    endDate = startDate;
                    startDate = startDate.AddDays(-7);

                    WeekTemplateViewModel weekVM = new WeekTemplateViewModel
                    {
                        WeekNr = GetWeekNumber(startDate),
                        StartDate = startDate,
                        EndDate = endDate,
                        Week = ConvertRegistriesToViewModel(unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate, user.UserId))
                    };

                    foreach (var registry in weekVM.Week)
                        registry.RegistryId = 0;

                    if (weekVM.Week.Count > 0) 
                        weekTemplates.Add(weekVM);
                }
                return weekTemplates;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// This method returns an int representing the week number for a specific date that year. 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>An int representing the week number.</returns>
        public int GetWeekNumber(DateTime date)
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            return cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// This method returns a list of MissionTaskViewModel which contains the missions and corresponding 
        /// tasks from which the user are a member of. 
        /// </summary>
        /// <returns>A list of MissionTaskViewModel items.</returns>
        [HttpGet]
        [Route("FavoriteMissionTemplate")]
        public ActionResult<List<MissionTaskViewModel>> GetFavoriteMissions()
        {
            try
            {
                List<FavoriteMission> favoriteMissionList = unitOfWork.FavoriteMissionRepository.GetFavoriteMissionsById(user.UserId);
                List<MissionTaskViewModel> missionTaskViewModel = new List<MissionTaskViewModel>();
                List<TaskViewModel> tasksViewModelList;

                for (int i = 0; i < favoriteMissionList.Count; i++)
                {
                    Mission mission = unitOfWork.MissionRepository.GetById(favoriteMissionList[i].MissionId);
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
                        Description = mission.Description,
                        Customer = unitOfWork.CustomerRepository.GetById(mission.CustomerId).Name,
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
    }
}

