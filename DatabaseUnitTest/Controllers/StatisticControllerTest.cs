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
using time_report_api.Controllers;

namespace Database_UnitTest.Controllers
{
    public class StatisticControllerTest
    {
        private IHttpContextAccessor httpContextAccessorMock;
        public StatisticControllerTest()
        {
            int userId = 1;
            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });
        }
        [Theory]
        [MemberData(nameof(GetInternalCustomerData), parameters: 1)]
        public void GetStatsInternVsCustomer(DateTime startDate,object expected)
        {
            List<Registry> dbRegistries = new List<Registry>();
            Mock<IRegistryRepository> registryRepoMock = new Mock<IRegistryRepository>();
            registryRepoMock.Setup(r => r.GetRegistriesByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(dbRegistries);

            Mock<IUnitOfWork> mockUOF = new Mock<IUnitOfWork>();
            mockUOF.Setup(uow => uow.RegistryRepository).Returns(registryRepoMock.Object);
           
            var controller = new StatisticsController(mockUOF.Object, httpContextAccessorMock);

            //Act
            var result = controller.GetStatsInternVsCustomer(startDate);

            if (expected.GetType() != StatusCodes.Status500InternalServerError.GetType())
            {
                Assert.IsType(expected.GetType(), result.Value);
            }
            else
            {
                Assert.Equal(expected, (result.Result as ObjectResult).StatusCode);
            }
        }
        //[Theory]
        //[MemberData(nameof(GetData), parameters: 2)]
        //public void GetStatsCustomerVsCustomer(DateTime date)
        //{

        //}
        //[Theory]
        //[MemberData(nameof(GetData), parameters: 2)]
        //public void GetTaskStats(int missionId)
        //{
        //}
        public static IEnumerable<object[]> GetInternalCustomerData(int numTests)
        {
            var allData = new List<object[]>
            {
                new object[] { new DateTime(2020,10,23) ,new List<StatisticCustomerInternalViewModel>() },
                new object[] { new DateTime() , (int)HttpStatusCode.InternalServerError },
            };

            return allData.Take(numTests);
        }
    }
}
