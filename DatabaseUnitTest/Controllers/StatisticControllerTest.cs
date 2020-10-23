﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLibrary.Model;
using DataAccessLayer.IRepositories;
using DataAccessLayer.UnitOfWork;
using Moq;
using TimeReportApi.Controllers;
using TimeReportApi.Models;
using Xunit;
using Xunit.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using TimeReportApi.Models.ViewModel;
using NSubstitute;
using time_report_api.Controllers;

namespace Database_UnitTest.Controllers
{
    public class StatisticControllerTest
    {
        private IHttpContextAccessor httpContextAccessorMock;
        public StatisticControllerTest()
        {
            int userId = 1;
            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });

        }
        //        [Theory]
        //        [MemberData(nameof(GetData), parameters: 2)]
        //        public void GetStatsInternVsCustomer(DateTime startDate)
        //        {

        //        }
        //[Theory]
        //[MemberData(nameof(GetData), parameters: 1)]
        //public void GetStatsCustomerVsCustomer(DateTime date)
        //{
        //    Customer dbCustomer = new Customer
        //    {
        //        Name = "DHL",
        //        Created = new DateTime(2020, 8, 5)
        //    };
        //    Mission dbMission = new Mission
        //    {
        //        Created = new DateTime(2020, 8, 5),
        //        Description = "Project1 for DHL",
        //        Finished = null,
        //        MissionName = "DHL Project1",
        //        Color = "#F0D87B",
        //        Start = new DateTime(2020, 8, 6),
        //        Status = 1,
        //        UserId = 1,
        //        CustomerId = 1
        //    };
        //    List<Task> dbTasks = new List<Task> { new Task { TaskId = 1, } };
        //    Mock<IRegistryRepository> regiRepoMock = new Mock<IRegistryRepository>();
        //    regiRepoMock.Setup(r => r.GetRegistriesByTask(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dbRegistries);
        //    Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
        //    customerRepoMock.Setup(c => c.GetById(It.IsAny<int>())).Returns(dbCustomer);
        //    Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
        //    missionRepoMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(dbMission);
        //    Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
        //    taskRepoMock.Setup(r => r.GetAllByMissionId(It.IsAny<int>())).Returns(dbTasks);




        //}
        [Theory]
    [MemberData(nameof(GetTasksStatsData), parameters: 2)]
    public void GetTaskStats(int missionId,bool exists, object expected)
        {
            //Arrange
            List<Task> dbTasks = new List<Task> { new Task { TaskId = 1, } };
            List<Registry> dbRegistries = new List<Registry>();
            Task dbTask = new Task
            {
                UserId = 1,
                MissionId = 1,
                Status = 0,
                ActualHours = null,
                Created = new DateTime(2020, 10, 5),
                Description = "DHL Project 1 Task1",
                EstimatedHour = 8.30,
                Invoice = InvoiceType.Invoicable,
                Name = "Task1 DHL Project1",
                Start = new DateTime(2020, 10, 6),
                Finished = new DateTime(2020, 05, 6)
            };
            Mission dbMission = new Mission
            {
                Created = new DateTime(2020, 8, 5),
                Description = "Project1 for DHL",
                Finished = null,
                MissionName = "DHL Project1",
                Color = "#F0D87B",
                Start = new DateTime(2020, 8, 6),
                Status = 1,
                UserId = 1,
                CustomerId = 1
            };
            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(dbMission);
            missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);
            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetAllByMissionId(It.IsAny<int>())).Returns(dbTasks);
            Mock<IRegistryRepository> regiRepoMock = new Mock<IRegistryRepository>();
            regiRepoMock.Setup(r => r.GetRegistriesByTask(It.IsAny<DateTime>(),It.IsAny<DateTime>(),It.IsAny<int>())).Returns(dbRegistries);
            
            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(regiRepoMock.Object);
            var controller = new StatisticsController(mockUOF.Object, httpContextAccessorMock);
            //Act

            var result = controller.GetTaskStats(missionId);

            //Assert

            if (expected.GetType() != StatusCodes.Status500InternalServerError.GetType())
            {
                Assert.IsType(expected.GetType(), result.Value);
            }
            else
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }


        }
        public static IEnumerable<object[]> GetTasksStatsData(int numTests)
        {
            List<TaskStatsViewModel> tsVMList = new List<TaskStatsViewModel>();
            var allData = new List<object[]>
            {
                new object[] { 1,true,tsVMList },
                new object[] {1,false,(int)HttpStatusCode.InternalServerError },
            };
            return allData.Take(numTests);
        }
    }
}
