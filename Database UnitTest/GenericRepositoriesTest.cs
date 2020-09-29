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
    [TestClass]
    public class GenericRepositoriesTest
    {

        private readonly UnitOfWork unitOfWork;
        private readonly BulbasaurDevContext DevContext;
        private readonly User testObject = new User(){userId = 1,eMail = "bla@bla.com",password = "bla",userName = "blabla"};

        public GenericRepositoriesTest()
        { 
            DevContext = GetContextWithData();
            unitOfWork=new UnitOfWork(DevContext);
        }
        private BulbasaurDevContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<BulbasaurDevContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BulbasaurDevContext(options);
            //context.Add(new User() { userId = 1, eMail = "bla@bla.com", password = "bla", userName = "blabla" });
            context.SaveChanges();

            return context;
        }

        [TestMethod]
        public void InsertTest()
        {
            unitOfWork.UserRepository.Insert(testObject);
            var item =unitOfWork.UserRepository.GetById(testObject.userId);
            Assert.Equal(testObject.userId, item.userId);
        }

        [TestMethod]
        public void UpdateRepositoryTest()
        {
            testObject.eMail = "newEmail";
            unitOfWork.UserRepository.Update(testObject);
            Assert.Equal("newEmail",unitOfWork.UserRepository.GetById(1).eMail);
        }

        [TestMethod]
        public void GetAllTest()
        {
            User anotherUser = new User() { userId = 2, userName = "test2", eMail = "Bla2@gmail.com", password = "blaSecret" };
            IEnumerable<User> allUsers = new List<User>() { testObject, anotherUser };
            DevContext.Add(testObject);
            DevContext.Add(anotherUser);
            //unitOfWork.UserRepository.Insert(testObject);
            //unitOfWork.UserRepository.Insert(anotherUser);
            var allUsersTestList = unitOfWork.UserRepository.GetAll();
            Assert.Equal(allUsers, allUsersTestList);
        }
}
