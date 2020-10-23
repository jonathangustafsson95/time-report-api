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
using Microsoft.EntityFrameworkCore.Storage;

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
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { 1,true ,(int)HttpStatusCode.OK },
                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }

        //public static IEnumerable<object[]> GetMissionMember()
        //{
        //    yield return  new object[]
        //    {
        //        new List<MissionMember>
        //        {
        //            new MissionMember
        //            {
        //                MissionId=1,
        //                UserId=1
        //            },
        //            new MissionMember
        //            {
        //                MissionId=2,
        //                UserId=1
        //            },
        //            new MissionMember
        //            {
        //                MissionId=3,
        //                UserId=1
        //            }
        //        }
        //    };
        //}


    }
}

