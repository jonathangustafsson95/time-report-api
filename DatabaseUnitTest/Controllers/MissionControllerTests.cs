using System;
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

namespace Database_UnitTest.Controllers
{
    public class MissionControllerTests
    {
        private IHttpContextAccessor httpContextAccessorMock;
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
        public void AddMissionMember(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se",
                Role = "User"
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);
            //får man göra så?
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
            if (expected == StatusCodes.Status500InternalServerError)
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
        public void DeleteFavoriteMission(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se",
                Role = "User"
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
            if (expected == StatusCodes.Status500InternalServerError)
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
        public void DeleteMissionMember(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se",
                Role = "User"
            };

            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);
            //får man göra så?
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
            if (expected == StatusCodes.Status500InternalServerError)
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
        public void AddFavoriteMission(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se",
                Role = "User"
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
            if(expected==StatusCodes.Status500InternalServerError)
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
        public void GetUserMissions(int id, bool exists, int expected)
        {
            //Arrange
            User dbUser = new User
            {
                UserId = 1,
                UserName = "Bengt",
                Password = "bengt123",
                EMail = "Bengt@bengt.se",
                Role = "User"
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
            //missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);
            missionRepoMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(dbMission);
            Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
            taskRepoMock.Setup(t => t.GetById(It.IsAny<int>())).Returns(dbTask);
            taskRepoMock.Setup(t => t.Exists(It.IsAny<int>())).Returns(exists);

            Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(c => c.GetById(It.IsAny<int>())).Returns(dbCustomer);

            Mock<IMissionMemberRepository> missionMemberRepoMock = new Mock<IMissionMemberRepository>();
            //missionMemberRepoMock.Setup(c => c.GetAllByUserId(It.IsAny<int>())).Returns(/*afafasda*/);

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
            Assert.IsAssignableFrom<MissionTaskViewModel>(dbMissionTaskViewModel);

            //Assert.IsInstanceOfType()
            //Assert.IsType<ActionResult<HttpResponse>>(result) ;
            //Assert.Equal(dbMissionTaskViewModel, result);
        }
        //[Theory]
        //[MemberData(nameof(GetData), parameters: 2)]
        //public void GetFavoriteMissions(int userId, Mission mission, List<Customer> customers, List<FavoriteMission> favoriteMissions, List<MissionViewModel> expected)
        //{
        //    //Arrange
        //    //User dbUser = new User
        //    //{
        //    //    UserId = 1,
        //    //    UserName = "Bengt",
        //    //    Password = "bengt123",
        //    //    EMail = "Bengt@bengt.se",
        //    //    Role = "User"
        //    //};
        //    List<MissionViewModel> dbMissionViewModels = new List<MissionViewModel>
        //    {
        //        new MissionViewModel
        //        {
        //            UserId = 1,
        //            MissionId = 1,
        //            CustomerId=1
        //        },
        //            new MissionViewModel
        //        {
        //            UserId = 1,
        //            MissionId = 2,
        //            CustomerId=1
        //        }
        //    };
        //    //Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //    //userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);
        //    //får man göra så?
        //    Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
        //    missionRepoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(mission);

        //    Mock<ICustomerRepository> customerRepoMock = new Mock<ICustomerRepository>();
        //    customerRepoMock.Setup(r => r.GetAll()).Returns(customers);

        //    Mock<IFavoriteMissionRepository> favoriteMissionRepoMock = new Mock<IFavoriteMissionRepository>();
        //    favoriteMissionRepoMock.Setup(r => r.GetFavoriteMissionsById(It.IsAny<int>())).Returns(favoriteMissions);
        //    favoriteMissionRepoMock.Setup(r => r.Update(It.IsAny<FavoriteMission>()));
        //    favoriteMissionRepoMock.Setup(r => r.Save());

        //    Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //    //mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //    mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
        //    mockUOF.Setup(uow => uow.FavoriteMissionRepository).Returns(favoriteMissionRepoMock.Object);
        //    mockUOF.Setup(uow => uow.CustomerRepository).Returns(customerRepoMock.Object);

        //    var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

        //    //Act
        //    var result = controller.GetFavoriteMissions();

        //    //Assert
        //    //Assert.IsType<ActionResult<List<MissionViewModel>>>(result);
        //    //Assert.IsAssignableFrom<List<MissionViewModel>>(dbMissionViewModels);
        //    //Assert.Equal(expected, (result.Result. as List<MissionViewModel>)));
        //}
        //[Theory]
        //[MemberData(nameof(GetData), parameters: 1)]
        //public void GetSpecificMission(int id, Mission mission, List<Task> listTask ,bool exists, MissionTaskViewModel expected)
        //{
        //    //Arrange
        //    User dbUser = new User
        //    {
        //        UserId = 1,
        //        UserName = "Bengt",
        //        Password = "bengt123",
        //        EMail = "Bengt@bengt.se",
        //        Role = "User"
        //    };


        //    Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //    userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

        //    Mock<IMissionRepository> missionRepoMock = new Mock<IMissionRepository>();
        //    missionRepoMock.Setup(r => r.Exists(It.IsAny<int>())).Returns(exists);
        //    missionRepoMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(mission);

        //    Mock<ITaskRepository> taskRepoMock = new Mock<ITaskRepository>();
        //    taskRepoMock.Setup(r => r.GetAllByMissionId(It.IsAny<int>())).Returns(listTask);
          
        //    Mock<IMissionMemberRepository> missionMemberRepoMock = new Mock<IMissionMemberRepository>();
        //    missionMemberRepoMock.Setup(r => r.GetAllByMissionId(It.IsAny<int>()));
          

        //    Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //    mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //    mockUOF.Setup(uow => uow.MissionRepository).Returns(missionRepoMock.Object);
        //    mockUOF.Setup(uow => uow.MissionMemberRepository).Returns(missionMemberRepoMock.Object);
        //    mockUOF.Setup(uow => uow.TaskRepository).Returns(taskRepoMock.Object);

        //    var controller = new MissionController(mockUOF.Object, httpContextAccessorMock);

        //    //Act
        //    var result = controller.GetSpecificMission(id);
        //    //ej klar
        //    //Assert
        //    Assert.IsAssignableFrom<MissionTaskViewModel>(expected);
        //    //Assert.IsType<ActionResult<HttpResponse>>(result);
        //    //Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
        //}
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true ,(int)HttpStatusCode.OK },
                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
        public static IEnumerable<object[]> GetSpecificMissionData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true,new Mission { MissionId=1, CustomerId=1},new List<Task> { new Task { TaskId=1, } } ,new MissionTaskViewModel { MissionId=1 , } },
                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
        //public static IEnumerable<object[]> GetFavoriteMissionData(int numTests)
        //{
        //    var allData = new List<object[]>
        //    {
        //        new object[] {1,
        //        new Mission
        //        {
        //            MissionId=1
        //        },
        //        new List<Customer>
        //        {
        //            new Customer
        //            {
        //                CustomerId=1
        //            }

        //        },
        //        new List<FavoriteMission>
        //        {
        //            new FavoriteMission
        //            {
        //                MissionId=1,
        //                UserId=1
        //            }
        //        },
        //        new List<MissionViewModel>
        //        {
        //            new MissionViewModel
        //            {
        //                UserId = 1,
        //                MissionId = 1,
        //                CustomerId=1
        //            },
        //             new MissionViewModel
        //            {
        //                UserId = 1,
        //                MissionId = 2,
        //                CustomerId=1
        //            }
        //        }
        //},
        //        new object[] { },
        //    };

        //    return allData.Take(numTests);
        //}

        public static IEnumerable<object[]> GetUserMissionsData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true ,(int)HttpStatusCode.OK },
                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
    }
}

