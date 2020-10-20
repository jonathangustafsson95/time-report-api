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

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
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
            user = unitOfWork.UserRepository.GetById(userId);
        }

        [HttpGet]
        [Route("InternalVsCustomer/{startDate}")]
        public List<StatisticCustomerInternalViewModel> GetStatsInternVsCustomer(DateTime startDate)
        {
            DateTime firstDayOfMonth = new DateTime(startDate.Year, startDate.Month, 1);
            float internalHours = 0, customerHours = 0;
            List<StatisticCustomerInternalViewModel> listStatistic=new List<StatisticCustomerInternalViewModel>();
            for (int i = 0; i <= 5; i++)
            {
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByDate(firstDayOfMonth, lastDayOfMonth ,user.UserId);
                foreach (Registry reg in registryByDate)
                {
                    if (reg.TaskId.HasValue)
                        customerHours += (float)reg.Hours;
                    else
                        internalHours += (float)reg.Hours;
                }
                listStatistic.Add(new StatisticCustomerInternalViewModel { Month= dateFormater.GetMonthName(firstDayOfMonth.Month), CustomerTime=customerHours, InternalTime=internalHours });
                firstDayOfMonth=firstDayOfMonth.AddMonths(-1);
                internalHours = 0; 
                customerHours=0;
            }                
            return listStatistic; 
        }

        [HttpGet]
        [Route("GetStatsCustomerVsCustomer/{startDate:DateTime}/{endDate:DateTime}")]
        public Dictionary<string, float> GetStatsCustomerVsCustomer(DateTime startDate, DateTime endDate)
        {

            List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate,user.UserId);

            Dictionary<int, float> taskIdAndHoursSpent = new Dictionary<int, float>();
            Dictionary<int, string> taskIdAndCustomerName = new Dictionary<int, string>();
            Dictionary<string, float> customerNameAndHoursSpent = new Dictionary<string, float>();
            Dictionary<string, float> customerNameAndPercent = new Dictionary<string, float>();
            float allHours = 0;

            foreach (Registry reg in registryByDate)
            {
                if (reg.TaskId.HasValue) 
                { 
                    if(!taskIdAndHoursSpent.ContainsKey(reg.TaskId.Value))
                    {
                        string customerName = (unitOfWork.CustomerRepository.GetById(unitOfWork.MissionRepository.GetById(unitOfWork.TaskRepository.GetById(reg.TaskId).MissionId.Value).CustomerId)).Name;
                        taskIdAndCustomerName.Add(reg.TaskId.Value, customerName);
                        taskIdAndHoursSpent.Add(reg.TaskId.Value, 0);

                    }

                    taskIdAndHoursSpent[reg.TaskId.Value] += (float)reg.Hours;
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

            foreach (KeyValuePair<string,float> item in customerNameAndHoursSpent)
            {
                customerNameAndPercent[item.Key + "%"] = (item.Value / allHours) * 100;
            }

            var customerStatistics = customerNameAndHoursSpent.Union(customerNameAndPercent).ToDictionary(k => k.Key, v => v.Value);
            return customerStatistics;
            
        }

        [HttpGet]
        [Route("GetTaskStats/{missionId:int}")]
        public List<TaskStatsViewModel> getTaskStats(int missionId)
        {
            List<TaskStatsViewModel> tsVMList = new List<TaskStatsViewModel>();
            double actualHours = 0;
            DateTime endDateForTaskCheck = new DateTime();
            List<CommonLibrary.Model.Task> taskList = unitOfWork.TaskRepository.GetAllByMissionId(missionId);
            foreach (var task in taskList)
            {
                if(task.Finished == null)
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

    }
}