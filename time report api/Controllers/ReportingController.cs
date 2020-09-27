using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.UnitOfWork;
using CommonLibrary.Model;
using time_report_api.Models;
using DataAccessLayer.Data;

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
    // Kalla på Authorize vid specifika metoder för att begränsa dem,
    // sätter man Authorize på hela kontrollern blir alla metoder begränsade och kräver login
    //[Authorize]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly User user;
        private readonly UnitOfWork unitOfWork;
        public ReportingController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            user = new User()
            {
                userId = 1,
                userName = "John",
                password = "abc123",
                eMail = "hej@lol.com"
            };
        }

        [HttpPost]
        [Route("AddTimeReport")]
        public ActionResult<User> AddTimeReport([FromBody] Registries newRegistries)
        {
            try
            {
                for (int i = 0; i < newRegistries.registries.Count; i++)
                {
                    // En int kan aldrig  vara  null, så om vi skickar nya registries
                    // bör vi hantera det på något sätt i JSON, typ  sätta regID  till 0?
                    if (newRegistries.registries[i].registryId == 0)
                    {
                        // Add new registry
                        unitOfWork.RegistryRepository.Insert(newRegistries.registries[i]);
                    }
                    else 
                    {
                        // Change  a existing registry
                        unitOfWork.RegistryRepository.Update(newRegistries.registries[i]);
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

        [HttpGet]
        [Route("GetWeek/{dateTime}")]
        public ActionResult<List<RegistryViewModel>> GetWeek(DateTime dateTime)
        {
            DateTime startDate = GetWeekStartDate(dateTime, DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(7);

            List<Registry> weekRegistries = unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate, user.userId);
            return(ConvertRegistriesToViewModel(weekRegistries));     
        }
        private DateTime GetWeekStartDate(DateTime dateTime, DayOfWeek startDay)
        {
            DateTime startDate = dateTime;
            while (startDate.DayOfWeek != startDay)
            {
                startDate = startDate.AddDays(-1);
            }
            return startDate;
        }
        private List<RegistryViewModel> ConvertRegistriesToViewModel(List<Registry> registries)
        {
            List<RegistryViewModel> weekRegistries = new List<RegistryViewModel>();
            Task task;
            Mission mission;


            foreach (var reg in registries)
            {
                task = unitOfWork.TaskRepository.GetById(reg.taskId);
                mission = unitOfWork.MissionRepository.GetById(task.missionId);

                weekRegistries.Add(new RegistryViewModel
                {
                    RegistryId = reg.registryId,
                    MissionName = mission.missionName,
                    TaskName = task.name,
                    Day = reg.date.Day,
                    Hours = reg.hours
                });
            }
            return weekRegistries;
        }
    }
}
