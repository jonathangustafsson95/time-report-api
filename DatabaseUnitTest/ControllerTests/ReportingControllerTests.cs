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

namespace DatabaseUnitTest.ControllerTests
{

    public static class ControllerTestsContext
    {
        public static BulbasaurDevContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<BulbasaurDevContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new BulbasaurDevContext(options);
            context.Database.EnsureCreated();

            //context.Add(new User() { userId = 1, eMail = "bla@bla.com", password = "bla", userName = "blabla" });
            context.SaveChanges();

            return context;
        }
    }

    [TestClass]
    public class ReportingControllerTests
    {
        private readonly UnitOfWork unitOfWork;
        private readonly BulbasaurDevContext DevContext;
        private readonly Registry testObject = new Registry() { registryId=1, taskId=1, userId=1, hours=5, created= DateTime.Today, date=DateTime.Today};

        public ReportingControllerTests()
        {
            //DevContext = testContext.GetContextWithData();
            unitOfWork = new UnitOfWork(DevContext);
        }






    }
}
