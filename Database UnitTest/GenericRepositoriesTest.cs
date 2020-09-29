using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Castle.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Core;
using CommonLibrary.Model;
using DataAccessLayer;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Assert = Xunit.Assert;
using time_report_api.Controllers;

namespace Database_UnitTest
{
    public static class inMemorydbcontext
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

        private readonly UnitOfWork unitOfWork;
        private readonly BulbasaurDevContext DevContext;
        private readonly User testUserObject = new User(){userId = 1,eMail = "bla@bla.com",password = "bla",userName = "blabla"};
        private readonly User anotherUser = new User() { userId = 2, userName = "test2", eMail = "Bla2@gmail.com", password = "blaSecret" };
        private readonly Registry testRegistryObject=new Registry(){registryId = 1,userId = 1,created =new DateTime(2020,02,14),
            date = new DateTime(2020,07,02), hours = 12,invoice = InvoiceType.invoicable,taskId = 1};
        private readonly Registry anotherTestRegistryObject=new Registry(){registryId = 2,userId = 1,created = new DateTime(2010,05,28) ,
            date = new DateTime(2019,09,02), hours = 15,invoice = InvoiceType.invoicable,taskId = 1};


        public GenericRepositoriesTest()
        { 
            DevContext = inMemorydbcontext.GetContextWithData();
            unitOfWork=new UnitOfWork(DevContext);
        }

        public void SeedInMemory(int numOfElement)
        {
            if (numOfElement == 1)
            {
                unitOfWork.UserRepository.Insert(testUserObject);
                unitOfWork.RegistryRepository.Insert(testRegistryObject);
                unitOfWork.UserRepository.Save();
            }
            else
            {
                unitOfWork.UserRepository.Insert(testUserObject);
                unitOfWork.UserRepository.Insert(anotherUser);
                unitOfWork.RegistryRepository.Insert(testRegistryObject);
                unitOfWork.RegistryRepository.Insert(anotherTestRegistryObject);
                unitOfWork.UserRepository.Save();
            }
        }

        [TestMethod]
        public void InsertTest()
        {
            unitOfWork.UserRepository.Insert(testUserObject);
            var item =unitOfWork.UserRepository.GetById(testUserObject.userId);
            Assert.Equal(testUserObject.userId, item.userId);
        }

        [TestMethod]
        public void UpdateRepositoryTest()
        {
            testUserObject.eMail = "newEmail";
            unitOfWork.UserRepository.Update(testUserObject);
            Assert.Equal("newEmail",unitOfWork.UserRepository.GetById(1).eMail);
        }
        [TestMethod]
        public void GetAllTest()
        {
            IEnumerable<User> allUsers = new List<User>() { testUserObject, anotherUser };
            SeedInMemory(2);
            var allUsersTestList = unitOfWork.UserRepository.GetAll();
            Assert.Equal(allUsers, allUsersTestList);
        }

        [TestMethod]
        public void DeleteTest()
        {
            SeedInMemory(1);
            unitOfWork.UserRepository.Delete(testUserObject.userId);
            unitOfWork.UserRepository.Save();
            var allUsersTestList = unitOfWork.UserRepository.GetAll();
            Assert.Empty(allUsersTestList);
        }

        [TestMethod]
        public void ExistsTest()
        {
            SeedInMemory(1);
            var actual=unitOfWork.UserRepository.Exists(testUserObject.userId);
            Assert.True(actual);
        }

        [TestMethod]
        public void SearchByTextTest()
        {
            SeedInMemory(2);
            var listOfUsers = unitOfWork.UserRepository.GetAll();
            List<User> result=unitOfWork.UserRepository.Search<User>((x=>x.userName),"blabla");
            var userInTest = result[0];
            Assert.Equal("blabla",userInTest.userName);
        }

        [TestMethod]
        public void SearchByIdTest()
        {
            SeedInMemory(2);
            var listOfUsers = unitOfWork.UserRepository.GetAll();
            List<User> result = unitOfWork.UserRepository.Search<User>(x => x.userId, 1);
            var userInTest = result[0];
            Assert.Equal(testUserObject.userId, userInTest.userId);
        }
        [TestMethod]
        public void SearchByDateTest()
        {
            SeedInMemory(2);
            var listOfRegistries = unitOfWork.RegistryRepository.GetAll();
            List<Registry> result = unitOfWork.RegistryRepository.Search<Registry>(getKey: x => x.date,new DateTime(2019 , 09,02));
            var regiInTest = result[0];
            Assert.Equal(anotherTestRegistryObject.date, regiInTest.date);
        }

    }
    [TestClass]
    public class ControllerTest
    {
        BulbasaurDevContext DbContext { get; set; }
        UnitOfWork unitOfWork { get; set; }
        public ControllerTest()
        {
            DbContext = inMemorydbcontext.GetContextWithData();
            unitOfWork = new UnitOfWork(DbContext);

        }
        [TestMethod]
        public void GetAllMissions()
        {
            //arrange
            var controller = new MissionController(unitOfWork);
            inMemorydbcontext.UpdateContext(DbContext);

            List<Mission> trueList = (List<Mission>)unitOfWork.MissionRepository.GetAll();
            //act 
            List<Mission> userList = (List<Mission>)controller.GetAllMissions();
            //assert
            Assert.Equal(userList, trueList);
        }
        [TestMethod]
        public void GetAllMissionsByUserId()
        {
            //arrange
            var controller = new MissionController(unitOfWork);

            List<MissionMember> missionMembers = (List<MissionMember>)unitOfWork.MissionMemberRepository.GetAllByUserId(1);
            List<int> trueIdList = new List<int>();
            List<int> testIdList = new List<int>();
            foreach (MissionMember mm in missionMembers)
            {
                Mission mission = unitOfWork.MissionRepository.GetById(mm.userId);
                trueIdList.Add(mission.missionId);
            }

            //act 
            List<Mission> missionList = (List<Mission>)controller.GetAllMissionByUserId(1);
            foreach(Mission mission in missionList)
            {
                
            }
            //assert
            //Assert.Equal(userList, testIdList);
        }

    }
}
