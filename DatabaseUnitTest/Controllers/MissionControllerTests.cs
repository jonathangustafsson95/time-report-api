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
        [MemberData(nameof(GetData),parameters:2)]
        public void AddMissionMember(int id,bool exists, int expected)
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
            Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
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
            missionMemberRepoMock.Setup(r => r.Delete(It.IsAny<MissionMember>()));
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
            Assert.Equal(expected, (result.Result as StatusCodeResult).StatusCode);
        }
        //[Theory]
        //[MemberData(nameof(GetData), parameters: 2)]
        //public void GetFavoriteMissions(int userId, Mission mission, List<Customer> customers,List<FavoriteMission> favoriteMissions ,List<MissionViewModel> expected)
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
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true ,(int)HttpStatusCode.OK },
                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
        public static IEnumerable<object[]> GetFavoriteMissionData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] {1,
                new Mission
                {
                    MissionId=1
                },
                new List<Customer>
                {
                    new Customer
                    {
                        CustomerId=1
                    }

                },
                new List<FavoriteMission>
                {
                    new FavoriteMission
                    {
                        MissionId=1,
                        UserId=1
                    }
                },
                new List<MissionViewModel>
                {
                    new MissionViewModel
                    {
                        UserId = 1,
                        MissionId = 1,
                        CustomerId=1
                    },
                     new MissionViewModel
                    {
                        UserId = 1,
                        MissionId = 2,
                        CustomerId=1
                    }
                }
        },
                new object[] { },
            };

            return allData.Take(numTests);
        }

    }
}

