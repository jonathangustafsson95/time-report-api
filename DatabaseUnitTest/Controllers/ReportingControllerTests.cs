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
using System.Net;

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
            userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
            registryRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.AddTimeReport(newRegistries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result.Result as StatusCodeResult).StatusCode);
        }


        //[Theory]
        //[MemberData(nameof(GetRegistries))]
        //public void AddTimeReport_ThrowsInternalServerErrorException(Registries newRegistries)
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

        //    newRegistries.RegistriesToReport[0].RegistryId = -1;

        //    //Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //    //userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

        //    Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
        //    registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
        //    registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
        //    registryRepoMock.Setup(r => r.Save());

        //    Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //    //mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //    mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

        //    var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
        //    //Act
        //    var result = controller.AddTimeReport(newRegistries);

        //    //Assert
        //    Assert.IsType<ActionResult<HttpResponse>>(result);
        //    Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as StatusCodeResult).StatusCode);
        //}

        // [Theory]
        // [MemberData(nameof(GetRegistries))]
        // public void AddTimeReport_ThrowsAccessViolationException(Registries newRegistries)
        // {
        //     //Arrange
        //     User dbUser = new User
        //     {
        //         UserId = 1,
        //         UserName = "Bengt",
        //         Password = "bengt123",
        //         EMail = "Bengt@bengt.se",
        //         Role = "User"
        //     };

        //     newRegistries.RegistriesToReport[0].UserId = 2;

        //     Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //     userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

        //     Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
        //     registryRepoMock.Setup(r => r.Insert(It.IsAny<Registry>()));
        //     registryRepoMock.Setup(r => r.Update(It.IsAny<Registry>()));
        //     registryRepoMock.Setup(r => r.Save());

        //     Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //     mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //     mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

        //     var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
        //     //Act
        //     var result = controller.AddTimeReport(newRegistries);

        //     //Assert
        //     Assert.IsType<ActionResult<HttpResponse>>(result);
        //     Assert.Equal((int)HttpStatusCode.Forbidden, (result.Result as StatusCodeResult).StatusCode);
        // }

        // [Theory]
        // [MemberData(nameof(GetRegistriesToDelete))]
        // public void DeleteTimeReport_SuccessTest(RegistriesDelete registries)
        // {
        //     //Arrange
        //     User dbUser = new User
        //     {
        //         UserId = 1,
        //         UserName = "Bengt",
        //         Password = "bengt123",
        //         EMail = "Bengt@bengt.se",
        //         Role = "User"
        //     };

        //     registries.RegistriesToReport[0].RegistryId = 1;
        //     registries.RegistriesToReport[1].RegistryId = 2;
        //     registries.RegistriesToReport[2].RegistryId = 3;


        //     Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //     userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

        //     Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
        //     registryRepoMock.Setup(r => r.Delete(It.IsAny<Registry>()));
        //     registryRepoMock.Setup(r => r.Save());

        //     Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //     mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //     mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

        //     var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

        //     //Act
        //     var result = controller.DeleteTimeReport(registries);

        //     //Assert
        //     Assert.IsType<ActionResult<HttpResponse>>(result);
        //     Assert.Equal((int)HttpStatusCode.OK, (result.Result as StatusCodeResult).StatusCode);
        // }


        // [Theory]
        // [MemberData(nameof(GetRegistriesToDelete))]
        // public void DeleteTimeReport_ThrowsAccessViolationException(RegistriesDelete registries)
        // {
        //     //Arrange
        //     User dbUser = new User
        //     {
        //         UserId = 1,
        //         UserName = "Bengt",
        //         Password = "bengt123",
        //         EMail = "Bengt@bengt.se",
        //         Role = "User"
        //     };
        //     registries.RegistriesToDelete[0].RegistryId = 1;
        //     registries.RegistriesToDelete[1].RegistryId = 2;
        //     registries.RegistriesToDelete[2].RegistryId = 3;
        //     registries.RegistriesToDelete[0].UserId = 2;

        //     Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //     userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

        //     Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
        //     registryRepoMock.Setup(r => r.Delete(It.IsAny<Registry>()));
        //     registryRepoMock.Setup(r => r.Save());

        //     Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //     mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //     mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

        //     var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
        //     //Act
        //     var result = controller.DeleteTimeReport(registries);

        //     //Assert
        //     Assert.IsType<ActionResult<HttpResponse>>(result);
        //     Assert.Equal((int)HttpStatusCode.Forbidden, (result.Result as StatusCodeResult).StatusCode);
        // }

        // [Theory]
        // [MemberData(nameof(GetRegistriesToDelete))]
        // public void DeleteTimeReport_ThrowsInternalServerErrorException(RegistriesDelete registries)
        // {
        //     //Arrange
        //     User dbUser = new User
        //     {
        //         UserId = 1,
        //         UserName = "Bengt",
        //         Password = "bengt123",
        //         EMail = "Bengt@bengt.se",
        //         Role = "User"
        //     };

        //     Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
        //     userRepoMock.Setup(u => u.GetById(It.IsAny<int>())).Returns(dbUser);

        //     Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
        //     registryRepoMock.Setup(r => r.Delete(It.IsAny<Registry>()));
        //     registryRepoMock.Setup(r => r.Save());

        //     Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
        //     mockUOF.Setup(uow => uow.UserRepository).Returns(userRepoMock.Object);
        //     mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

        //     var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);  
        //     //Act
        //     var result = controller.DeleteTimeReport(registries);

        //     //Assert
        //     Assert.IsType<ActionResult<HttpResponse>>(result);
        //     Assert.Equal((int)HttpStatusCode.InternalServerError, (result.Result as StatusCodeResult).StatusCode);
        // }
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
    }
}