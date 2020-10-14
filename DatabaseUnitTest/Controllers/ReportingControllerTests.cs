using System;
using System.Collections;
using System.Collections.Generic;
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

        [Theory]
        [MemberData(nameof(GetRegistries))]
        public void AddTimeReport_SuccessTest(Registries newRegistries)
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
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>()));
            userRepoMock.Setup(u => u.Insert(It.IsAny<User>()));
            userRepoMock.Object.Insert(dbUser);

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            
            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.AddTimeReport(newRegistries);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Theory]
        [MemberData(nameof(GetRegistries))]
        public void AddTimeReport_ThrowsException(Registries newRegistries)
        {
            //Arrange
             Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>()));

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            
            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
            //Act
            var result = controller.AddTimeReport(newRegistries);

            //Assert
            Assert.IsType<StatusCodeResult>(result);
            //Assert.Equal((int)(StatusCodeResult)result.StatusCode,500);
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
    }
}