using System;
using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IRepositories;
using DataAccessLayer.UnitOfWork;
using Moq;
using TimeReportApi.Controllers;
using TimeReportApi.Models;
using TimeReportApi.Models.ViewModel;
using Xunit;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Times = Moq.Times;

namespace DatabaseUnitTest.Controllers
{
    public class ReportingControllerTests
    {
        private IHttpContextAccessor httpContextAccessorMock;
        public ReportingControllerTests()
        {
            int userId = 1;
            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });
        }

        public static IEnumerable<object[]> GetRegistries()
        {
            yield return new object[]
            {
                new Registries
                {
                    RegistriesToReport = new List<Registry>
                    {
                        new Registry
                        {
                            RegistryId = 0,
                            TaskId = 1,
                            UserId = 1,
                            Hours = 2,
                            Created = new DateTime(2021 - 01 - 04),
                            Date = new DateTime(2021 - 01 - 04),
                            Invoice = InvoiceType.Invoicable
                        },
                        new Registry
                        {
                            RegistryId = 0,
                            TaskId = 1,
                            UserId = 1,
                            Hours = 2,
                            Created = new DateTime(2021 - 01 - 04),
                            Date = new DateTime(2021 - 01 - 04),
                            Invoice = InvoiceType.Invoicable
                        },
                        new Registry
                        {
                            RegistryId = 0,
                            TaskId = 1,
                            UserId = 1,
                            Hours = 2,
                            Created = new DateTime(2021 - 01 - 04),
                            Date = new DateTime(2021 - 01 - 04),
                            Invoice = InvoiceType.Invoicable
                        }
                    }
                }
            };
        }

        public static IEnumerable<object[]> GetRegistriesToDelete()
        {
            yield return new object[]
            {
                new RegistriesDelete
                {
                    RegistriesToDelete = new List<int> 
                    {
                        1,2,3
                    }
                }
            };
        }

        public List<Registry> GetTestRegistries()
        {
            Registry reg1 = new Registry
            {
                RegistryId = 0,
                TaskId = 1,
                UserId = 1,
                Hours = 2,
                Created = new DateTime(2020-10-23),
                Date = new DateTime(2020-10-23),
                Invoice = InvoiceType.Invoicable
            };
            Registry reg2 = new Registry
            {
                RegistryId = 0,
                TaskId = 2,
                UserId = 1,
                Hours = 2,
                Created = new DateTime(2020-10-24),
                Date = new DateTime(2020-10-24),
                Invoice = InvoiceType.Invoicable
            };
            Registry reg3 = new Registry
            {
                RegistryId = 0,
                TaskId = null,
                UserId = 1,
                Hours = 2,
                Created = new DateTime(2020-10-25),
                Date = new DateTime(2020-10-25),
                Invoice = InvoiceType.Invoicable
            };
            return new List<Registry> {reg1,reg2,reg3};
        }

        public List<Mission> GetTestMissions()
        {
            Mission mission1 = new Mission
            {
                MissionId = 1,
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
            Mission mission2 = new Mission
            {
                MissionId = 2,
                Created = new DateTime(2020, 9, 5),
                Description = "Project2 for DHL",
                Finished = new DateTime(2020, 10, 1),
                MissionName = "DHL Project2",
                Color = "#5B8D76",
                Start = new DateTime(2020, 8, 6),
                Status = 1,
                UserId = 1,
                CustomerId = 1
            };
            return new List<Mission> {mission1, mission2};
        }

        public List<Task> GetTestTasks()
        {
            Task task1 = new Task
            {
                TaskId = 1,
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
                Finished = null
            };
            Task task2 = new Task
            {
                TaskId = 2,
                UserId = 1,
                MissionId = 2,
                Status = 0,
                ActualHours = null,
                Created = new DateTime(2020, 11, 5),
                Description = "DHL Project 1 Task2",
                EstimatedHour = 8.30,
                Invoice = InvoiceType.NotInvoicable,
                Name = "Task2 DHL Project1",
                Start = new DateTime(2020, 12, 6),
                Finished = new DateTime(2020, 12, 7)
            };
            return new List<Task> {task1, task2}; 
        }

        [Theory]
        [MemberData(nameof(GetRegistries))]
        public void AddTimeReport_SuccessTest(Registries newRegistries)
        {
            //Arrange
            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.AddTimeReport(newRegistries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result.Result as StatusCodeResult).StatusCode);
        }


        [Theory]
        [MemberData(nameof(GetRegistries))]
        public void AddTimeReport_ThrowsInternalServerErrorException(Registries newRegistries)
        {
           //Arrange

           newRegistries.RegistriesToReport[0].RegistryId = 1;

           Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
           registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
           registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>())).Throws<Exception>();
           registryRepoMock.Setup(r => r.Save());

           Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
           mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

           var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
           //Act
           var result = controller.AddTimeReport(newRegistries);

           //Assert
           Assert.IsType<ActionResult<HttpResponse>>(result);
           Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);

        }

        [Theory]
        [MemberData(nameof(GetRegistries))]
        public void AddTimeReport_ThrowsAccessViolationException(Registries newRegistries)
        {
            newRegistries.RegistriesToReport[0].UserId = 2;

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
            //Act
            var result = controller.AddTimeReport(newRegistries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.Forbidden, (result.Result as ObjectResult).StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetRegistriesToDelete))]
        public void DeleteTimeReport_SuccessTest(RegistriesDelete registries)
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();
            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();

            registryRepoMock.Setup(r => r.GetById(1)).Returns(weekRegistries[0]);
            registryRepoMock.Setup(r => r.GetById(2)).Returns(weekRegistries[1]);
            registryRepoMock.Setup(r => r.GetById(3)).Returns(weekRegistries[2]);

            registryRepoMock.Setup(r => r.Delete(It.IsAny<object>())).Callback<object>(regId => weekRegistries.RemoveAll(reg => reg.RegistryId == (int)regId));
            registryRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.DeleteTimeReport(registries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result.Result as StatusCodeResult).StatusCode);
            Assert.Empty(weekRegistries);
            registryRepoMock.Verify(r => r.Delete(It.IsAny<object>()), Times.Exactly(3));
            registryRepoMock.Verify(r => r.Save(), Times.Once);
        }


        [Theory]
        [MemberData(nameof(GetRegistriesToDelete))]
        public void DeleteTimeReport_ThrowsAccessViolationException(RegistriesDelete registries)
        {
            //Arrange
            Registry reg1 = new Registry
            {
                RegistryId = 1,
                TaskId = 1,
                UserId = 2,
                Hours = 2,
                Created = new DateTime(2021 - 01 - 04),
                Date = new DateTime(2021 - 01 - 04),
                Invoice = InvoiceType.Invoicable
            };
            List<Registry> mockedRegistries = new List<Registry>{ reg1 };

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();

            registryRepoMock.Setup(r => r.GetById(1)).Returns(reg1);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.DeleteTimeReport(registries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.Forbidden, (result.Result as ObjectResult).StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetRegistriesToDelete))]
        public void DeleteTimeReport_ThrowsInternalServerErrorException(RegistriesDelete registries)
        {
           //Arrange
            Registry reg1 = new Registry
            {
                RegistryId = 1,
                TaskId = 1,
                UserId = 1,
                Hours = 2,
                Created = new DateTime(2021 - 01 - 04),
                Date = new DateTime(2021 - 01 - 04),
                Invoice = InvoiceType.Invoicable
            };
            List<Registry> mockedRegistries = new List<Registry>{ reg1 };

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetById(1)).Returns(reg1);
            registryRepoMock.Setup(r => r.Delete(It.IsAny<object>())).Throws<Exception>();

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.DeleteTimeReport(registries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);
        }

        [Theory]
        [InlineData("2020-10-22")]
        public void GetWeek_SuccessTest(DateTime dateTime)
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();
            List<Mission> missions = GetTestMissions();
            List<Task> tasks = GetTestTasks();

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(weekRegistries);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.GetById(1)).Returns(missions[0]);
            missionRepoMock.Setup(r => r.GetById(2)).Returns(missions[1]);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetById(1)).Returns(tasks[0]);
            taskRepoMock.Setup(r => r.GetById(2)).Returns(tasks[1]);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetWeek(dateTime);

            //Assert
            Assert.IsType<ActionResult<List<RegistryViewModel>>>(result);
            Assert.Equal(3, result.Value.Count);
        }
        
        [Theory]
        [InlineData("2020-10-22")]
        public void GetWeek_ThrowsInternalServerErrorException(DateTime dateTime)
        {
            //Arrange
            
            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Throws<Exception>();

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetWeek(dateTime);

            //Assert
            Assert.IsType<ActionResult<List<RegistryViewModel>>>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);
        }

        [Theory]
        [InlineData("2020-10-22")]
        public void GetDay_SuccessTest(DateTime dateTime)
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();
            List<Mission> missions = GetTestMissions();
            List<Task> tasks = GetTestTasks();

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(weekRegistries);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.GetById(1)).Returns(missions[0]);
            missionRepoMock.Setup(r => r.GetById(2)).Returns(missions[1]);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetById(1)).Returns(tasks[0]);
            taskRepoMock.Setup(r => r.GetById(2)).Returns(tasks[1]);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetDay(dateTime);

            //Assert
            Assert.IsType<ActionResult<List<RegistryViewModel>>>(result);
            Assert.Equal(3, result.Value.Count);
        }
        
        [Theory]
        [InlineData("2020-10-22")]
        public void GetDay_ThrowsInternalServerErrorException(DateTime dateTime)
        {
            //Arrange
            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Throws<Exception>();
            
            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetDay(dateTime);

            //Assert
            Assert.IsType<ActionResult<List<RegistryViewModel>>>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetLatestRegistries_SuccessTest()
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();
            List<Mission> missions = GetTestMissions();
            List<Task> tasks = GetTestTasks();

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetLatestRegistries(It.IsAny<int>(), It.IsAny<int>())).Returns(weekRegistries);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.GetById(1)).Returns(missions[0]);
            missionRepoMock.Setup(r => r.GetById(2)).Returns(missions[1]);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetById(1)).Returns(tasks[0]);
            taskRepoMock.Setup(r => r.GetById(2)).Returns(tasks[1]);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetLatestregistries();

            //Assert
            Assert.IsType<ActionResult<List<RegistryViewModel>>>(result);
            Assert.Equal(3, result.Value.Count);
        }
        
        [Fact]
        public void GetLatestRegistries_ThrowsInternalServerErrorException()
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetLatestRegistries(It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetLatestregistries();

            //Assert
            Assert.IsType<ActionResult<List<RegistryViewModel>>>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);
        }

        [Theory]
        [InlineData("2020-10-22")]
        public void GetWeekTemplates_SuccessTest(DateTime todaysDate)
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();
            List<Mission> missions = GetTestMissions();
            List<Task> tasks = GetTestTasks();

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.SetupSequence(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns(weekRegistries)
                .Returns(new List<Registry>())
                .Returns(new List<Registry>())
                .Returns(new List<Registry>())
                .Returns(new List<Registry>());

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.GetById(1)).Returns(missions[0]);
            missionRepoMock.Setup(r => r.GetById(2)).Returns(missions[1]);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetById(1)).Returns(tasks[0]);
            taskRepoMock.Setup(r => r.GetById(2)).Returns(tasks[1]);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetWeekTemplates(todaysDate);

            //Assert
            Assert.IsType<ActionResult<List<WeekTemplateViewModel>>>(result);
            registryRepoMock.Verify(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()), Times.Exactly(5));
            Assert.Equal(1, result.Value.Count);
        }
        
        [Theory]
        [InlineData("2020-10-22")]
        public void GetWeekTemplates_ThrowsInternalServerErrorException(DateTime todaysDate)
        {
            //Arrange
            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetLatestRegistries(It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetWeekTemplates(todaysDate);

            //Assert
            Assert.IsType<ActionResult<List<WeekTemplateViewModel>>>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetFavoriteMissions_SuccessTest()
        {
            //Arrange
            List<Registry> weekRegistries = GetTestRegistries();
            List<Task> tasks = new List<Task> 
            {
                new Task
                {
                    TaskId = 1,
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
                    Finished = null
                }
            };
            List<FavoriteMission> favoriteMissions = new List<FavoriteMission> 
            {
                new FavoriteMission { UserId = 1, MissionId = 1 }
            };

            Mission mission = new Mission
            {
                MissionId = 1,
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

            Customer customer = new Customer{
                CustomerId = 1,
                Name = "Customer1",
                Created = new DateTime(2020-10-22)
            };

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.GetById(1)).Returns(mission);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetAllByMissionId(1)).Returns(tasks);
            
            Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(r => r.GetById(1)).Returns(customer);

            Mock<IFavoriteMissionRepository> favoriteMissionRepoMock = new Mock<IFavoriteMissionRepository>();
            favoriteMissionRepoMock.Setup(r => r.GetFavoriteMissionsById(1)).Returns(favoriteMissions);
            
            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);
            mockUOF.Setup(uow => uow.CustomerRepository).Returns(customerRepoMock.Object);
            mockUOF.Setup(uow => uow.FavoriteMissionRepository).Returns(favoriteMissionRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetFavoriteMissions();

            //Assert
            Assert.IsType<ActionResult<List<MissionTaskViewModel>>>(result);
            Assert.Equal(1, result.Value.Count);
        }
        
        [Fact]
        public void GetFavoriteMissions_ThrowsInternalServerErrorException()
        {
            //Arrange
            Mock<IFavoriteMissionRepository> favoriteMissionRepoMock = new Mock<IFavoriteMissionRepository>();
            favoriteMissionRepoMock.Setup(r => r.GetFavoriteMissionsById(1)).Throws<Exception>();

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.FavoriteMissionRepository).Returns(favoriteMissionRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetFavoriteMissions();

            //Assert
            Assert.IsType<ActionResult<List<MissionTaskViewModel>>>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as ObjectResult).StatusCode);
        }
    }
}