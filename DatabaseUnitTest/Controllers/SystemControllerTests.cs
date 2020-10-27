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
using Microsoft.Extensions.Configuration;

namespace DatabaseUnitTest.Controllers
{
    public class SystemControllerTests
    {
        private readonly IHttpContextAccessor httpContextAccessorMock;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IUnitOfWork> mockUOF;
        private readonly Mock<IConfiguration> configuration;

        public SystemControllerTests()
        {
            int userId = 1;
            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });

            userRepositoryMock = new Mock<IUserRepository>();
            mockUOF = new Mock<IUnitOfWork>();

            configuration = new Mock<IConfiguration>();
            configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:SecretKey")]).Returns("This is not the real secret key");
            configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("https://localhost:45645");
            configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Audience")]).Returns("https://localhost:45645");
        }

        public static IEnumerable<object[]> GetUser()
        {
            yield return new object[]
            {
                new User
                {
                    UserName = "Bengt",
                    Password = "bengt123",
                    EMail = "Bengt@bengt.se"
                },
            };
        }

        [Theory]
        [MemberData(nameof(GetUser))]
        public void Login_SuccessTest(User login){
            //Arrange
            userRepositoryMock.Setup(u => u.GetByName(It.IsAny<string>()));
            userRepositoryMock.Setup(u => u.GetByName(login.UserName)).Returns(new List<User> { login });   
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepositoryMock.Object);
            SystemController controller = new SystemController(mockUOF.Object, configuration.Object);
            
            //Act
            var result = controller.Login(login);
            
            //Assert
            Assert.Equal((int)HttpStatusCode.OK, (result as ObjectResult).StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetUser))]
        public void Login_UnauthorizedTest(User login){
            //Arrange
            userRepositoryMock.Setup(u => u.GetByName(It.IsAny<string>())).Returns(new List<User>());
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepositoryMock.Object);
            SystemController controller = new SystemController(mockUOF.Object, configuration.Object);

            //Act
            var result = controller.Login(login);

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, (result as ObjectResult).StatusCode);
        }

        [Theory]
        [MemberData(nameof(GetUser))]
        public void Login_ThrowsInternalExceptionTest(User login){
            //Arrange
            userRepositoryMock.Setup(u => u.GetByName(It.IsAny<string>()));
            userRepositoryMock.Setup(u => u.GetByName(login.UserName)).Throws<Exception>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepositoryMock.Object); 
            SystemController controller = new SystemController(mockUOF.Object, configuration.Object);

            //Act
            var result = controller.Login(login);
            
            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (result as ObjectResult).StatusCode);
        }

    }
}
