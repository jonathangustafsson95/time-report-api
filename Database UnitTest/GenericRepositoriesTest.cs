using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data.Repositories;
using CommonLibrary.Model;
using DataAccessLayer.Data;
using Moq;
using Xunit;
using DataAccessLayer.Data.IRepositories;
using DataAccessLayer.Data.IReppositories;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;

namespace Database_UnitTest
{
    [TestClass]
    public class GenericRepositoriesTest
    {
        
        [TestMethod]
        public void TestAddToDataBase()
        {



            //arrange
            var testObject = new MissionMember();
            var context = new Mock<BulbasaurContext>();
            var dbSetMock = new Mock<DbSet<MissionMember>>();
            context.Setup(x => x.Set<MissionMember>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<MissionMember>()).Entity).Returns(testObject);

            //act
            var repository = new GenericRepository<MissionMember>(context.Object);
            repository.Insert(testObject);
            //assert
            context.Verify(x => x.Set<MissionMember>());
            dbSetMock.Verify(x => x.Add(It.Is<MissionMember>(y => y == testObject)));



        }
    }
}
