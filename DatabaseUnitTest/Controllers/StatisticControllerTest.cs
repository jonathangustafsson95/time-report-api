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

//namespace Database_UnitTest.Controllers
//{
//    public class StatisticControllerTest
//    {
//        private IHttpContextAccessor httpContextAccessorMock;
//        public StatisticControllerTest()
//        {
//            int userId = 1;
//            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
//            this.httpContextAccessorMock = A.Fake<IHttpContextAccessor>();
//            httpContextAccessorMock.HttpContext = A.Fake<HttpContext>();
//            httpContextAccessorMock.HttpContext.User = A.Fake<ClaimsPrincipal>();
//            A.CallTo(() => httpContextAccessorMock.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });

//        }
//        [Theory]
//        [MemberData(nameof(GetData), parameters: 2)]
//        public void GetStatsInternVsCustomer(DateTime startDate)
//        {

//        }
//        [Theory]
//        [MemberData(nameof(GetData), parameters: 2)]
//        public void GetStatsCustomerVsCustomer(DateTime date)
//        {

//        }
//        [Theory]
//        [MemberData(nameof(GetData), parameters: 2)]
//        public void GetTaskStats(int missionId)
//        {
//        }
//        public static IEnumerable<object[]> GetData(int numTests)
//        {
//            var allData = new List<object[]>
//            {
//                new object[] { 1,true ,(int)HttpStatusCode.OK },
//                new object[] { 99,false, (int)HttpStatusCode.InternalServerError },
//            };

//            return allData.Take(numTests);
//        }
//    }
//}
