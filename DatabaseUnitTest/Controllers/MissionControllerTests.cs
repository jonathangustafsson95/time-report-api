using System;
using System.Collections.Generic;
using CommonLibrary.Model;
using DataAccessLayer.IRepositories;
using DataAccessLayer.UnitOfWork;
using Moq;
using TimeReportApi.Controllers;
using TimeReportApi.Models;
using Xunit;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using TimeReportApi.Models.ViewModel;

namespace Database_UnitTest.Controllers
{
    public class MissionControllerTests
    {
        private readonly IHttpContextAccessor httpContextAccessorMock;
        public MissionControllerTests()
        {
            int userId = 1;
            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });
        
        }
        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        public void AddMissionMember_Validation_Test(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se"
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);
            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);

            Mock<IMissionMemberRepository> missionMemberRepoMock = new Mock<IMissionMemberRepository>();
            missionMemberRepoMock.Setup(r => r.Insert(It.IsAny<MissionMember>()));
            missionMemberRepoMock.Setup(r => r.Update(It.IsAny<MissionMember>()));
            missionMemberRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionMemberRepository).Returns(missionMemberRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.AddMissionMember(id);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            if (expected == StatusCodes.Status400BadRequest)
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }
            else
            {
                Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);

            }
        }
        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        public void DeleteFavoriteMission_Validation_Test(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se"
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

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);

            Mock<IFavoriteMissionRepository> favortieRepoMock = new Mock<IFavoriteMissionRepository>();
            favortieRepoMock.Setup(f => f.Delete(It.IsAny<User>(), It.IsAny<FavoriteMission>()));
            favortieRepoMock.Setup(f => f.Update(It.IsAny<FavoriteMission>()));
            favortieRepoMock.Setup(f => f.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.FavoriteMissionRepository).Returns(favortieRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.DeleteFavoriteMission(id);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            if (expected == StatusCodes.Status400BadRequest)
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }
            else
            {
                Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
            }
        }
        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        public void DeleteMissionMember_Validation_Test(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se"
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);
            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);

            Mock<IMissionMemberRepository> missionMemberRepoMock = new Mock<IMissionMemberRepository>();
            missionMemberRepoMock.Setup(r => r.Delete(It.IsAny<int>(), It.IsAny<int>()));
            missionMemberRepoMock.Setup(r => r.Update(It.IsAny<MissionMember>()));
            missionMemberRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionMemberRepository).Returns(missionMemberRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.DeleteMissionMember(id);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            if (expected == StatusCodes.Status400BadRequest)
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }
            else
            {
                Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
            }
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        public void AddFavoriteMission_Validation_Test(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se"
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);

            Mock<IFavoriteMissionRepository> favoriteMissionRepoMock = new Mock<IFavoriteMissionRepository>();
            favoriteMissionRepoMock.Setup(r => r.Insert(It.IsAny<FavoriteMission>()));
            favoriteMissionRepoMock.Setup(r => r.Update(It.IsAny<FavoriteMission>()));
            favoriteMissionRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.FavoriteMissionRepository).Returns(favoriteMissionRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.AddFavoriteMission(id);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            if(expected==StatusCodes.Status400BadRequest)
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }
            else
            {
                Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
            }
        }
        [Theory]
        [MemberData(nameof(GetUserMissionsData), parameters: 2)]
        public void GetUserMissions_Validation_Test(int id, bool exists, ActionResult<List<MissionTaskViewModel>> expected,int expCode)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se"
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
                Finished = null
            };
            TaskViewModel dbTaskViewModel = new TaskViewModel();
            MissionTaskViewModel dbMissionTaskViewModel = new MissionTaskViewModel();
            Customer dbCustomer = new Customer
            {
                Name = "DHL",
                Created = new DateTime(2020, 8, 5)
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(dbMission);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(t => t.GetById(It.IsAny<int>())).Returns(dbTask);
            taskRepoMock.Setup(t => t.Exists(It.IsAny<int>())).Returns(exists);

            Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(c => c.GetById(It.IsAny<int>())).Returns(dbCustomer);

            Mock<IMissionMemberRepository> missionMemberRepoMock = new Mock<IMissionMemberRepository>();

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);
            mockUOF.Setup(uow => uow.CustomerRepository).Returns(customerRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionMemberRepository).Returns(missionMemberRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetUserMissions(id);

            //Assert
            if (expected.GetType() !=  StatusCodes.Status500InternalServerError.GetType())
            {
                Assert.IsType(expected.GetType(), result);
            }
            else
            {
                Assert.Equal(expCode, (result.Result as StatusCodeResult).StatusCode);
            }
        }
        [Theory]
        [MemberData(nameof(GetAllMissionBySearchData),parameters:3)]
        public void GetAllMissionsBySearchString_Validation_Test(string searchString, object expected)
        {
            //Arrange
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
            Customer dbCustomer = new Customer
            {
                Name = "DHL",
                Created = new DateTime(2020, 8, 5)
            };
            List<Customer> dbCustomers = new List<Customer>();
            dbCustomers.Add(dbCustomer);
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
                Finished = null
            };
            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(dbMission);

            Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(c => c.GetById(It.IsAny<int>())).Returns(dbCustomer);
            customerRepoMock.Setup(c => c.Search<Customer>(It.IsAny<Func<Customer,string>>(), It.IsAny<string>())).Returns(dbCustomers);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(t => t.GetById(It.IsAny<int>())).Returns(dbTask);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);
            mockUOF.Setup(uow => uow.CustomerRepository).Returns(customerRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);
            //Act
            var result = controller.GetAllMissionsBySearchString(searchString);

            //Assert
            if (expected.GetType() != StatusCodes.Status500InternalServerError.GetType())
            {
                Assert.IsType(expected.GetType(), result);
            }
            else if(searchString== "  ")
            {
                Assert.Equal(expected, result.Value.Count());
            }
            else
            {
                Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
            }
        }
        [Theory]
        [MemberData(nameof(GetFavoriteMissionData), parameters: 1)]
        public void GetFavoriteMissions_Validation_Test(object expected)
        {
            Mission dbMission = new Mission();
            List < Customer > dbCustomer= new List<Customer>();
            List<FavoriteMission> dbFavoriteMission = new List<FavoriteMission>();

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(dbMission);

            Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(r => r.GetAll()).Returns(dbCustomer);

            Mock<IFavoriteMissionRepository> favoriteMissionRepoMock = new Mock<IFavoriteMissionRepository>();
            favoriteMissionRepoMock.Setup(r => r.GetFavoriteMissionsById(It.IsAny<int>())).Returns(dbFavoriteMission);
            favoriteMissionRepoMock.Setup(r => r.Update(It.IsAny<FavoriteMission>()));
            favoriteMissionRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.FavoriteMissionRepository).Returns(favoriteMissionRepoMock.Object);
            mockUOF.Setup(uow => uow.CustomerRepository).Returns(customerRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetFavoriteMissions();

            if (expected.GetType() !=  StatusCodes.Status500InternalServerError.GetType())
            {
                Assert.IsType(expected.GetType(), result.Value);
            }
            else
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }
         
        }
        [Theory]
        [MemberData(nameof(GetSpecificMissionData), parameters: 1)]
        public void GetSpecificMission_Validation_Test(int id, bool exists, object expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se"
            };
            Customer dbCustomer = new Customer();
            Mission dbMission = new Mission { MissionId = 1, CustomerId = 1 };
            List<Task> dbTasks = new List<Task> { new Task { TaskId = 1, } };
            List<MissionMember> dbMissionMember = new List<MissionMember>();
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

            Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
            missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);
            missionRepoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(dbMission);

            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(r => r.GetAllByMissionId(It.IsAny<int>())).Returns(dbTasks);

            Mock<IMissionMemberRepository> missionMemberRepoMock = new Mock<IMissionMemberRepository>();
            missionMemberRepoMock.Setup(r => r.GetAllByMissionId(It.IsAny<int>())).Returns(dbMissionMember);

            Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbCustomer);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
            mockUOF.Setup(uow => uow.MissionMemberRepository).Returns(missionMemberRepoMock.Object);
            mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);
            mockUOF.Setup(uow => uow.CustomerRepository).Returns(customerRepoMock.Object);

            var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);
            //Act
            var result = controller.GetSpecificMission(id);
            //Assert
            if (expected.GetType() != StatusCodes.Status400BadRequest.GetType())
            {
                Assert.IsType(expected.GetType(), result.Value);
            }
            else
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }

        }
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true ,(int)HttpStatusCode.OK },
                new object[] { 99,false, (int)HttpStatusCode.BadRequest },
            };

            return allData.Take(numTests);
        }
        public static IEnumerable<object[]> GetSpecificMissionData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true ,new MissionTaskViewModel { MissionId=1 , } },
                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
        public static IEnumerable<object[]> GetFavoriteMissionData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] {new List<MissionViewModel>()},
                new object[] {(int)HttpStatusCode.InternalServerError } 
            };

            return allData.Take(numTests);
        }
        public static IEnumerable<object[]> GetUserMissionsData(int numTests)
        {
            List<MissionTaskViewModel> listOfExpected = new List<MissionTaskViewModel>();
   

            var allData = new List<object[]>
            {
                new object[] { 1,true , listOfExpected,4},
                new object[] { 99,false,listOfExpected,(int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
        public static IEnumerable<object[]> GetAllMissionBySearchData(int numTests)
        {
            ActionResult<IEnumerable<MissionTaskViewModel>> listOfExpected = new List<MissionTaskViewModel>();


            var allData = new List<object[]>
            {
                new object[] { "DHL", listOfExpected},
                new object[] { "  ", 0}
            };

            return allData.Take(numTests);
        }
    }
}

