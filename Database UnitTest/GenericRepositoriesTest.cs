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


namespace Database_UnitTest
{
    public static class testContext
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
    }
    [TestClass]
    public class GenericRepositoriesTest
    {

        private readonly UnitOfWork unitOfWork;
        private readonly BulbasaurDevContext DevContext;
        private readonly User testObject = new User(){userId = 1,eMail = "bla@bla.com",password = "bla",userName = "blabla"};

        public GenericRepositoriesTest()
        { 
            DevContext = testContext.GetContextWithData();
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

    }
}
