using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Razor.Language;
using TimeReportApi.Models;
using TimeReportApi.Models.ViewModel;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly User user;
        private readonly IUnitOfWork unitOfWork;
        private readonly DateTimeFormatInfo dateFormater;

        public StatisticsController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            dateFormater = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;
            this.unitOfWork = unitOfWork;
            int userId = Int32.Parse(httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "userId").Value);
            user = new User { UserId = userId };
        }

        /// <summary>
        /// This method gets statistics for a user's time spent on internal tasks versus customer tasks. It gets statistic
        /// from startDate and five months back. Input is startDate which is usually the present time. UserId is received from
        /// token.
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns> A list of statistics for five months in the shape of five StatisticCustomerInternalViewModel items. </returns>
        [HttpGet]
        [Route("InternalVsCustomer/{startDate}")]
        public ActionResult<List<StatisticCustomerInternalViewModel>> GetStatsInternVsCustomer(DateTime startDate)
        {
            try
            {
                if (startDate > DateTime.Now)
                    throw new Exception();
                DateTime firstDayOfMonth = new DateTime(startDate.Year, startDate.Month, 1);
                float internalHours = 0, customerHours = 0;
                List<StatisticCustomerInternalViewModel> listStatistic = new List<StatisticCustomerInternalViewModel>();
                for (int i = 0; i < 5; i++)
                {
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByDate(firstDayOfMonth, lastDayOfMonth, user.UserId);
                    foreach (Registry reg in registryByDate)
                    {
                        if (reg.TaskId.HasValue)
                            customerHours += (float)reg.Hours;
                        else
                            internalHours += (float)reg.Hours;
                    }
                    listStatistic.Add(new StatisticCustomerInternalViewModel { Month = dateFormater.GetMonthName(firstDayOfMonth.Month), CustomerTime = customerHours, InternalTime = internalHours });
                    firstDayOfMonth = firstDayOfMonth.AddMonths(-1);
                    internalHours = 0;
                    customerHours = 0;
                }
                listStatistic.Reverse();
                return listStatistic;
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Invalid DateTime" });
            }
        }

        /// <summary>
        /// This method gets statistics for a user's time spent on different customers. It gets statistic
        /// from input-date and five months back. Input is usually the present time. UserId is received from
        /// token.
        /// </summary>
        /// <param name="date"></param>
        /// <returns> A list of statistics for five months in the shape of five CustomerVsCustomerStatsViewModel items. </returns>

        [HttpGet]
        [Route("CustomerVsCustomer/{date}")]
        public ActionResult<List<CustomerVsCustomerStatsViewModel>> GetStatsCustomerVsCustomer(DateTime date)
        {
            try
            {
                if (date > DateTime.Now) 
                    throw new Exception();
                DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByDate(firstDayOfMonth, lastDayOfMonth, user.UserId);

                Dictionary<int, float> taskIdAndHoursSpent = new Dictionary<int, float>();
                Dictionary<int, string> taskIdAndCustomerName = new Dictionary<int, string>();
                Dictionary<string, float> customerNameAndHoursSpent = new Dictionary<string, float>();
                List<CustomerVsCustomerStatsViewModel> CVSCViewModelList = new List<CustomerVsCustomerStatsViewModel>();
                float allHours = 0;
                string customerName = "";
                float internalHours = 0;

                foreach (Registry reg in registryByDate)
                {
                    if (reg.TaskId.HasValue)
                    {
                        if (!taskIdAndHoursSpent.ContainsKey(reg.TaskId.Value))
                        {
                            customerName = (unitOfWork.CustomerRepository.GetById(unitOfWork.MissionRepository.GetById(unitOfWork.TaskRepository.GetById(reg.TaskId).MissionId.Value).CustomerId)).Name;

                            taskIdAndCustomerName.Add(reg.TaskId.Value, customerName);
                            taskIdAndHoursSpent.Add(reg.TaskId.Value, 0);
                        }
                        taskIdAndHoursSpent[reg.TaskId.Value] += (float)reg.Hours;
                    }
                    else
                    {
                        internalHours += (float)reg.Hours;
                    }
                }

                foreach (KeyValuePair<int, float> item in taskIdAndHoursSpent)
                {
                    allHours += taskIdAndHoursSpent[item.Key];

                    if (customerNameAndHoursSpent.ContainsKey(taskIdAndCustomerName[item.Key]))
                    {
                        customerNameAndHoursSpent[taskIdAndCustomerName[item.Key]] += taskIdAndHoursSpent[item.Key];
                    }
                    else
                    {
                        customerNameAndHoursSpent.Add(taskIdAndCustomerName[item.Key], taskIdAndHoursSpent[item.Key]);
                    }
                }

                foreach (var item in customerNameAndHoursSpent)
                {
                    CVSCViewModelList.Add(new CustomerVsCustomerStatsViewModel
                    {
                        CustomerName = item.Key,
                        Hours = item.Value
                    });
                }

                CVSCViewModelList.Add(new CustomerVsCustomerStatsViewModel
                {
                    CustomerName = "Internal",
                    Hours = internalHours
                });
                return CVSCViewModelList;
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Invalid DateTime" });
            }
        }

        /// <summary>
        /// This method gets statistics for time spent on the different tasks of a specific mission. 
        /// </summary>
        /// <param name="int missionId"></param>
        /// <returns> A list of TaskStatsViewModel items that compares the EstimatedHours for a task
        /// versus how many hours have actually been spent on the task. </returns>

        [HttpGet]
        [Route("TaskStats/{missionId:int}")]
        public ActionResult<List<TaskStatsViewModel>> GetTaskStats(int missionId)
        {
            try
            {
                if (unitOfWork.MissionRepository.Exists(missionId))
                {
                List<TaskStatsViewModel> tsVMList = new List<TaskStatsViewModel>();
                double actualHours = 0;
                DateTime endDateForTaskCheck = new DateTime();
                List<CommonLibrary.Model.Task> taskList = unitOfWork.TaskRepository.GetAllByMissionId(missionId);
                foreach (var task in taskList)
                {
                    if (task.Finished == null)
                    {
                        endDateForTaskCheck = DateTime.Now;
                    }
                    else
                    {
                        endDateForTaskCheck = task.Finished ?? default(DateTime);
                    }

                    List<Registry> tasksRegistries = unitOfWork.RegistryRepository.GetRegistriesByTask(task.Start, endDateForTaskCheck, task.TaskId);
                    foreach (var registry in tasksRegistries)
                    {
                        actualHours += registry.Hours;
                    }

                    TaskStatsViewModel tsVM = new TaskStatsViewModel
                    {
                        TaskId = task.TaskId,
                        TaskName = task.Name,
                        EstimatedHours = task.EstimatedHour,
                        ActualHours = actualHours
                    };
                    tsVMList.Add(tsVM);
                    actualHours = 0;
                }

                return tsVMList;
            }
                else
                {
                    throw new Exception();
                }
                }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Invalid DateTime" });
            }
        }
    }
}