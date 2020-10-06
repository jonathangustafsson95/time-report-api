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

namespace time_report_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly User dummy;
        public StatisticsController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            dummy = new User()
            {
                UserId = 1,
                UserName = "John",
                Password = "abc123",
                EMail = "hej@lol.com"
            };
        }
        [HttpGet]
        [Route("GetStatsInternVsCustomer/{startDate}/{endDate}")]
        public Dictionary<string,float> GetStatsInternVsCustomer(DateTime startDate, DateTime endDate)
        {
            List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate, dummy.UserId);
            int internalCount = 0, customerCount = 0;
            float internalHours = 0, customerHours = 0;
            foreach (Registry reg in registryByDate)
            {
                if (reg.TaskId.HasValue)
                {
                    customerCount++;
                    customerHours += (float)reg.Hours;
                }
                else
                {
                    internalCount++;
                    internalHours = (float)reg.Hours;
                }
            }
            float totalHours = internalHours + customerHours;
            //Statistic statistic = new Statistic()
            //{
            //    TotalTask = registryByDate.Count,
            //    InternalTaskCount = internalCount,
            //    CustomerTaskCount = customerCount,
            //    InternalTime = internalHours,
            //    CustomerTime = customerHours,
            //    TotalTime = totalHours,
            //    InternalTimePerc = (internalHours / totalHours) * 100,
            //    CustomerTimePerc = (customerHours / totalHours) * 100
            //};
            Statistic statistic = new Statistic();
            statistic.StatisticDictionary["TotalTask"] = registryByDate.Count;
            statistic.StatisticDictionary["InternalTaskCount"] = internalCount;
            statistic.StatisticDictionary["CustomerTaskCount"] = customerCount;
            statistic.StatisticDictionary["InternalTime"] = internalHours;
            statistic.StatisticDictionary["CustomerTime"] = customerHours;
            statistic.StatisticDictionary["TotalTime"] = totalHours;
            statistic.StatisticDictionary["InternalTimePerc"] = (internalHours / totalHours) * 100;
            statistic.StatisticDictionary["CustomerTimePerc"] = (customerHours / totalHours) * 100;
            //List<Registry> filteredByInternal=registryByDate.Where(t=>t.taskId==null).ToList();

            return statistic.StatisticDictionary; 
        }

        [HttpGet]
        [Route("GetStatsCustomerVsCustomer/{userId:int}/{startDate:DateTime}/{endDate:DateTime}")]
        public Dictionary<string, float> GetStatsCustomerVsCustomer(int userId, DateTime startDate, DateTime endDate)
        {
            List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByDate(startDate, endDate, userId);

            Dictionary<int, float> taskIdAndHoursSpent = new Dictionary<int, float>();
            Dictionary<int, string> taskIdAndCustomerName = new Dictionary<int, string>();
            Dictionary<string, float> customerNameAndHoursSpent = new Dictionary<string, float>();

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
                if(customerNameAndHoursSpent.ContainsKey(taskIdAndCustomerName[item.Key]))
                {
                    customerNameAndHoursSpent[taskIdAndCustomerName[item.Key]] += taskIdAndHoursSpent[item.Key];

                }
                else 
                {
                    customerNameAndHoursSpent.Add(taskIdAndCustomerName[item.Key], taskIdAndHoursSpent[item.Key]);
                }
            }

            return customerNameAndHoursSpent;
        }
    }
}