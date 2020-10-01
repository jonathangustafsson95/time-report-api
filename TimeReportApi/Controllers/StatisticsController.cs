using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                userId = 1,
                userName = "John",
                password = "abc123",
                eMail = "hej@lol.com"
            };
        }
        [HttpGet]
        [Route("GetStatsInternVsCustomer/{numberOfWeeks:int}")]
        public Statistic GetStatsInternVsCustomer(int numberOfWeeks)
        {
            List<Registry> registryByDate = unitOfWork.RegistryRepository.GetRegistriesByNumberOfDays(numberOfWeeks * 7, dummy.userId);
            int internalCount = 0, customerCount = 0;
            float internalHours = 0, customerHours = 0;
            foreach (Registry reg in registryByDate)
            {
                if (reg.taskId.HasValue)
                {
                    customerCount++;
                    customerHours += (float)reg.hours;
                }
                else
                {
                    internalCount++;
                    internalHours = (float)reg.hours;
                }
            }
            float totalHours = internalHours + customerHours;
            Statistic statistic = new Statistic()
            {
                totalTask = registryByDate.Count,
                internalTaskCount = internalCount,
                customerTaskCount = customerCount,
                totalTime = totalHours,
                internalTimePerc = internalHours / totalHours,
                customerTimePerc = customerHours / totalHours

            };
            //List<Registry> filteredByInternal=registryByDate.Where(t=>t.taskId==null).ToList();

            return statistic;
        }
    }
}