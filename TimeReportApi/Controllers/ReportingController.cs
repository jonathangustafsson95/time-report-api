using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.UnitOfWork;
using CommonLibrary.Model;
using time_report_api.Models;
using DataAccessLayer;
using System.Security.Claims;

namespace time_report_api.Controllers
{
    /// <summary>
    /// Controller for handlings rerporting events.
    /// </summary>
    [Route("api/[controller]")]
    // Kalla på Authorize vid specifika metoder för att begränsa dem,
    // sätter man Authorize på hela kontrollern blir alla metoder begränsade och kräver login
    [Authorize]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly User user;
        private readonly UnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ReportingController(UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            int userId = Int32.Parse(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "userId").Value);
            user = unitOfWork.UserRepository.GetById(userId);
        }

        /// <summary>
        /// This method takes a registries class from body of request, extracts
        /// registry items from registries and inserts new entries and updates 
        /// entries that have been changed.
        /// </summary>
        /// <param name="newRegistries"></param>
        /// <returns>Http response message</returns>
        [HttpPost]
        [Route("AddTimeReport")]
        public ActionResult<User> AddTimeReport([FromBody] Registries newRegistries)
        {
            try
            {
                for (int i = 0; i < newRegistries.registriesToReport.Count; i++)
                {
                    // En int kan aldrig  vara  null, så om vi skickar nya registries
                    // bör vi hantera det på något sätt i JSON, typ  sätta regID  till 0?
                    if (newRegistries.registriesToReport[i].registryId == 0)
                    {
                        // Add new registry
                        unitOfWork.RegistryRepository.Insert(newRegistries.registriesToReport[i]);
                    }
                    else 
                    {
                        // Change  a existing registry
                        unitOfWork.RegistryRepository.Update(newRegistries.registriesToReport[i]);
                    }
                }
                unitOfWork.RegistryRepository.Save();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong!");
            }
        }

        /// <summary>
        /// This method returns a list of RegistryViewModel items extracted from the database
        /// for the week in which provided date exists.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>A list of RegistryViewModel items.</returns>
        [HttpGet]
        [Route("GetWeek/{dateTime}")]
        public ActionResult<List<RegistryViewModel>> GetWeek(DateTime dateTime)
        {
            DateTime startDate = GetWeekStartDate(dateTime, DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(7);

            List<Registry> weekRegistries = unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate, user.userId);
            return(ConvertRegistriesToViewModel(weekRegistries));     
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
        private List<RegistryViewModel> ConvertRegistriesToViewModel(List<Registry> registries)
        {
            List<RegistryViewModel> weekRegistries = new List<RegistryViewModel>();
            Task task;
            RegistryViewModel registryViewModel;

            foreach (var reg in registries)
            {
                registryViewModel = new RegistryViewModel();
                
                if (reg.taskId == null)
                {
                    registryViewModel.taskId = null;
                    registryViewModel.missionName = "Internal time";
                }
                else
                {
                    task = unitOfWork.TaskRepository.GetById(reg.taskId);
                    registryViewModel.taskName = task.name;
                    registryViewModel.missionName = unitOfWork.MissionRepository.GetById(task.missionId).missionName;
                    registryViewModel.invoice = task.invoice;
                    registryViewModel.taskId = task.taskId;
                }

                registryViewModel.registryId = reg.registryId;
                registryViewModel.day = reg.date.DayOfWeek;
                registryViewModel.hours = reg.hours;


                weekRegistries.Add(registryViewModel);
            }
            return weekRegistries;
        }
    }
}
