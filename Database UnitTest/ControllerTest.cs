using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using time_report_api.Controllers;
using DataAccessLayer;
using CommonLibrary;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using CommonLibrary.Model;
using Microsoft.EntityFrameworkCore;


namespace Database_UnitTest
{
    [TestClass]
    class ControllerTest
    {

        BulbasaurDevContext dbcontext;
        private UnitOfWork unitOfWork;
    
        [TestMethod]
        public void GetAllTest()
        {
            //arrange
            dbcontext = GetDataContext();
            unitOfWork = new UnitOfWork(dbcontext);
            var controller = new MissionController(unitOfWork);
            User user = new User() { userId = 1, password = "abc123", eMail = "adjalj", missionFavorites = null, missionMemberships = null, userName = "name" };
            //act 
            List<User>userList= (List<User>)controller.GetAllMissions();
            //assert
            Assert.AreEqual(userList, new List<User>());
        }
        private BulbasaurDevContext GetDataContext()
        {
            var options = new DbContextOptionsBuilder<BulbasaurDevContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new BulbasaurDevContext(options);
            
            return context;
        }
    }
}
