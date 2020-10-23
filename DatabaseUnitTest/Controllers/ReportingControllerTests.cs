﻿using System;
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
            Registry reg2 = new Registry
            {
                RegistryId = 2,
                TaskId = 1,
                UserId = 1,
                Hours = 2,
                Created = new DateTime(2021 - 01 - 04),
                Date = new DateTime(2021 - 01 - 04),
                Invoice = InvoiceType.Invoicable
            };
            Registry reg3 = new Registry
            {
                RegistryId = 3,
                TaskId = 1,
                UserId = 1,
                Hours = 2,
                Created = new DateTime(2021 - 01 - 04),
                Date = new DateTime(2021 - 01 - 04),
                Invoice = InvoiceType.Invoicable
            };
            List<Registry> mockedRegistries = new List<Registry>{ reg1, reg2, reg3 };

            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();

            registryRepoMock.Setup(r => r.GetById(1)).Returns(reg1);
            registryRepoMock.Setup(r => r.GetById(2)).Returns(reg2);
            registryRepoMock.Setup(r => r.GetById(3)).Returns(reg3);

            registryRepoMock.Setup(r => r.Delete(It.IsAny<object>())).Callback<object>(regId => mockedRegistries.RemoveAll(reg => reg.RegistryId == (int)regId));
            registryRepoMock.Setup(r => r.Save());

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);

            var controller = new ReportingController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.DeleteTimeReport(registries);

            //Assert
            Assert.IsType<ActionResult<HttpResponse>>(result);
            Assert.Equal((int)HttpStatusCode.OK, (result.Result as StatusCodeResult).StatusCode);
            Assert.Empty(mockedRegistries);
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
    }
}