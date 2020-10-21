//using System;
//using System.Collections.Generic;
//using System.Text;
//using CommonLibrary.Model;
//using DataAccessLayer.IRepositories;
//using DataAccessLayer.UnitOfWork;
//using Moq;
//using TimeReportApi.Controllers;
//using TimeReportApi.Models;
//using Xunit;
//using Xunit.Extensions;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Http;
//using FakeItEasy;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;
//namespace Database_UnitTest.Controllers
//{
//    public class MissionControllerTests
//    {
//        private IHttpContextAccessor httpContextAccessorMock;
//        public MissionControllerTests()
//        {
//            int userId = 1;
//            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
//            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
//            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
//            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
//            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });
//        }

//        [Theory]
//        [MemberData(nameof(GetMissionMember))]
//        public void AddMissionMember_SuccessTest(MissionMember missionMember)
//        {
//            //Arrange
//            User dbUser = new User
//            {
//                UserId = 1,
//                UserName = "Bengt",
//                Password = "bengt123",
//                EMail = "Bengt@bengt.se",
//                Role = "User"
//            };

//            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
//            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

//            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
//            registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
//            registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
//            registryRepoMock.Setup(r => r.Save());

//            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
//            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
//            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

//            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

//            //Act
//            var result = controller.AddTimeReport(newRegistries);

//            //Assert
//            Assert.IsType<ActionResult<HttpResponse>>(result);
//            Assert.Equal((int)HttpStatusCode.OK, (result.Result as StatusCodeResult).StatusCode);
//        }
//        public static IEnumerable<object> MissionMember GetMissionMember()
//        {
//            List<object> listMember = new List<object>
//            {
//                    new MissionMember
//                    {
//                        MissionId = 1,
//                        UserId=1
//                    },
//            };
//            return listMember;
//        }
//    }
//}
