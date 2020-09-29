using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
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
        private readonly User testObject = new User(){userId = 1,eMail = "bla@bla.com",password = "bla",userName = "blabla"};

        public GenericRepositoriesTest()
        { 
            DevContext = inMemorydbcontext.GetContextWithData();
            unitOfWork=new UnitOfWork(DevContext);
        }
       

        [TestMethod]
        public void TestAddToDataBase()
        {
            
            unitOfWork.UserRepository.Insert(testObject);
            var item =unitOfWork.UserRepository.GetById(testObject.userId);
            Assert.Equal(testObject.userId, item.userId);



        }

        [TestMethod]
        public void UpdateRepository()
        {
            testObject.eMail = "newEmail";
            unitOfWork.UserRepository.Update(testObject);
            Assert.Equal("newEmail",unitOfWork.UserRepository.GetById(1).eMail);
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
