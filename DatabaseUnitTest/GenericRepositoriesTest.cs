using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonLibrary.Model;
using DataAccessLayer;
using DataAccessLayer.UnitOfWork;
using Assert = Xunit.Assert;
using TimeReportApi.Controllers;
using TimeReportApi.Models;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System.Security.Claims;
using FakeItEasy;

namespace DatabaseUnitTest
{
    public static class InMemoryDbContext
    {
        public static BulbasaurDevContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<BulbasaurDevContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BulbasaurDevContext(options);

            //context.Add(new User() { userId = 1, eMail = "bla@bla.com", password = "bla", userName = "blabla" }); 
            context.SaveChanges();

            return context;
        }
        public static void UpdateContext(BulbasaurDevContext dbcontext) => dbcontext.Database.EnsureCreated();
      
    }
    [TestClass]
    public class GenericRepositoriesTest
    {

        private readonly UnitOfWork _unitOfWork;
        private readonly User _testUserObject = new User(){UserId = 1,EMail = "bla@bla.com",Password = "bla",UserName = "blabla"};
        private readonly User _anotherUser = new User() { UserId = 2, UserName = "test2", EMail = "Bla2@gmail.com", Password = "blaSecret" };
        private readonly Registry _testRegistryObject=new Registry(){RegistryId = 1,UserId = 1,Created =new DateTime(2020,02,14),
            Date = new DateTime(2020,07,02), Hours = 12,Invoice = InvoiceType.Invoicable,TaskId = 1};
        private readonly Registry _anotherTestRegistryObject=new Registry(){RegistryId = 2,UserId = 1,Created = new DateTime(2010,05,28) ,
            Date = new DateTime(2019,09,02), Hours = 15,Invoice = InvoiceType.Invoicable,TaskId = 1};
        private readonly MissionMember _testMissionMember=new MissionMember(){MissionId = 1,UserId = 1};


        public GenericRepositoriesTest()
        {
            var devContext = InMemoryDbContext.GetContextWithData();
            _unitOfWork=new UnitOfWork(devContext);
        }

        public void SeedInMemory(int numOfElement)
        {
            if (numOfElement == 1)
            {
                _unitOfWork.UserRepository.Insert(_testUserObject);
                _unitOfWork.RegistryRepository.Insert(_testRegistryObject);
                _unitOfWork.MissionMemberRepository.Insert(_testMissionMember);
                _unitOfWork.UserRepository.Save();
            }
            else
            {
                _unitOfWork.UserRepository.Insert(_testUserObject);
                _unitOfWork.UserRepository.Insert(_anotherUser);
                _unitOfWork.RegistryRepository.Insert(_testRegistryObject);
                _unitOfWork.RegistryRepository.Insert(_anotherTestRegistryObject);
                _unitOfWork.UserRepository.Save();
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            _unitOfWork.UserRepository.Insert(_testUserObject);
            var item =_unitOfWork.UserRepository.GetById(_testUserObject.UserId);
            Assert.Equal(_testUserObject.UserId, item.UserId);
        }

        [TestMethod]
        public void GetByIdComposite()
        {
            SeedInMemory(1);
            var item = _unitOfWork.MissionMemberRepository.GetById(1, 1);
            Assert.Equal(_testMissionMember,item);
        }

        [TestMethod]
        public void UpdateRepositoryTest()
        {
            _testUserObject.EMail = "newEmail";
            _unitOfWork.UserRepository.Update(_testUserObject);
            Assert.Equal("newEmail",_unitOfWork.UserRepository.GetById(1).EMail);
        }
        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<User> allUsers = new List<User>() { _testUserObject, _anotherUser };
            SeedInMemory(2);
            var allUsersTestList = _unitOfWork.UserRepository.GetAll();
            Assert.Equal(allUsers, allUsersTestList);
        }

        //[TestMethod]
        //public void DeleteTest()
        //{
        //    SeedInMemory(1);
        //    _unitOfWork.UserRepository.Delete(_testUserObject.UserId);
        //    //unitOfWork.MissionMemberRepository.Delete(testMissionMember.missionId,testMissionMember.userId);
        //    //unitOfWork.RegistryRepository.Delete(testRegistryObject.registryId);
        //    _unitOfWork.UserRepository.Save();
        //    var allUsersTestList = _unitOfWork.UserRepository.GetAll();
        //    Assert.Empty(allUsersTestList);
        //}

        [TestMethod]
        public void DeleteTestComposite()
        {
            SeedInMemory(1);
            _unitOfWork.MissionMemberRepository.Delete(1,1);
            _unitOfWork.MissionMemberRepository.Save();
            var allMissionMemberList = _unitOfWork.MissionMemberRepository.GetAll();
            Assert.Empty(allMissionMemberList);
        }

        [TestMethod]
        public void ExistsTest()
        {
            SeedInMemory(1);
            var actual=_unitOfWork.UserRepository.Exists(_testUserObject.UserId);
            Assert.True(actual);
        }
        [TestMethod]
        public void ExistsTestComposite()
        {
            SeedInMemory(1);
            var actual = _unitOfWork.MissionMemberRepository.Exists(_testMissionMember.MissionId,_testMissionMember.UserId);
            Assert.True(actual);
        }

        [TestMethod]
        public void SearchByTextTest()
        {
            SeedInMemory(2);
            var listOfUsers = _unitOfWork.UserRepository.GetAll();
            List<User> result=_unitOfWork.UserRepository.Search<User>((x=>x.UserName),"blabla");
            var userInTest = result[0];
            Assert.Equal("blabla",userInTest.UserName);
        }

        [TestMethod]
        public void SearchByIdTest()
        {
            SeedInMemory(2);
            var listOfUsers = _unitOfWork.UserRepository.GetAll();
            List<User> result = _unitOfWork.UserRepository.Search<User>(x => x.UserId, 1);
            var userInTest = result[0];
            Assert.Equal(_testUserObject.UserId, userInTest.UserId);
        }
        [TestMethod]
        public void SearchByDateTest()
        {
            SeedInMemory(2);
            var listOfRegistries = _unitOfWork.RegistryRepository.GetAll();
            List<Registry> result = _unitOfWork.RegistryRepository.Search<Registry>(getKey: x => x.Date,new DateTime(2019 , 09,02));
            var regiInTest = result[0];
            Assert.Equal(_anotherTestRegistryObject.Date, regiInTest.Date);
        }

    }
    [TestClass]
    public class ControllerTest
    {
        private readonly BulbasaurDevContext DbContext;
        private readonly UnitOfWork unitOfWork;

        private readonly MissionController missionController;

        public ControllerTest()
        {
            int userId = 1;
            var userIdClaim = A.Fake<Claim>(x => x.WithArgumentsForConstructor(() => new Claim("userId", userId.ToString())));
            var httpContextAccessor = A.Fake<IHttpContextAccessor>();
            httpContextAccessor.HttpContext = A.Fake<HttpContext>();
            httpContextAccessor.HttpContext.User = A.Fake<ClaimsPrincipal>();
            A.CallTo(() => httpContextAccessor.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });
            DbContext = InMemoryDbContext.GetContextWithData();
            unitOfWork = new UnitOfWork(DbContext);
            missionController = new MissionController(unitOfWork, httpContextAccessor);
        }
        [TestMethod]
        public void GetAllMissions()
        {
            //arrange
            InMemoryDbContext.UpdateContext(DbContext);

            List<Mission> trueList = (List<Mission>)unitOfWork.MissionRepository.GetAll();
            //act 
            List<Mission> userList = (List<Mission>)missionController.GetAllMissions();
            //assert
            Assert.Equal(userList, trueList);
        }
        //[TestMethod]
        //public void GetAllMissionsByUserId()
        //{
        //    //arrange
        //    InMemoryDbContext.UpdateContext(DbContext);

        //    List<MissionMember> missionMembers = (List<MissionMember>)unitOfWork.MissionMemberRepository.GetAllByUserId(1);
        //    List<int> trueIdList = new List<int>();
        //    List<int> testIdList = new List<int>();
        //    foreach (MissionMember mm in missionMembers)
        //    {
        //        Mission mission = unitOfWork.MissionRepository.GetById(mm.UserId);
        //        trueIdList.Add(mission.MissionId);
        //    }

        //    //act 
        //    List<MissionViewModel> missionList = (List<MissionViewModel>)missionController.GetAllMissionByUserId(1);
        //    foreach (MissionViewModel mission in missionList)
        //    {
        //        testIdList.Add(mission.missionId);
        //    }
        //    //assert
        //    Assert.Equal(trueIdList, testIdList);
        //}

    }

}


