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
        private readonly Customer _Customer = new Customer() { CustomerId=1, Name="ICA", Created= new DateTime(2020, 07, 02) };

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
                _unitOfWork.CustomerRepository.Insert(_Customer);
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

        [TestMethod]
        public void DeleteTest()
        {
            SeedInMemory(1);
            _unitOfWork.CustomerRepository.Delete(_testUserObject.UserId);
            _unitOfWork.CustomerRepository.Save();
            var allCustomerTestList = _unitOfWork.CustomerRepository.GetAll();
            Assert.Empty(allCustomerTestList);
        }

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
    }
   

}


