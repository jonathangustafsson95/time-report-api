using System.Runtime.CompilerServices;
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
        public void AddTestInMemory()
        {
            var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            using (var context = new MyContext(options))
            {
                var UserBla = new User()
                {
                    userId = 1,
                    eMail = "bkabkabadbvd"
                };

                context.users.Add(UserBla);
                context.SaveChanges();
            //var repository = new GenericRepository<User>(context);
            //repository.Insert(UserBla);

            }
            //act

        }



        [TestMethod]
        public void TestAddToDataBase()
        {

            var testObject = new User() {userId = 1,eMail = "Bla@bla"};
            var context = new Mock<BulbasaurDevContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<User>()).Entity).Returns(testObject);

            //act
            var repository = new GenericRepository<User>(context.Object);
            repository.Insert(testObject);
            //assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Add(It.Is<User>(y => y == testObject)));




            //arrange
            //var testObject = new MissionMember {missionId = 1, userId = 1,Mission = };
            //var context = new Mock<BulbasaurDevContext>();
            //var dbSetMock = new Mock<DbSet<MissionMember>>();
            //context.Setup(x => x.Set<MissionMember>()).Returns(dbSetMock.Object);
            //dbSetMock.Setup(x => x.Add(It.IsAny<MissionMember>()).Entity).Returns(testObject);

            ////act
            //var repository = new GenericRepository<MissionMember>(context.Object);
            //repository.Insert(testObject);
            ////assert
            //context.Verify(x => x.Set<MissionMember>());
            //dbSetMock.Verify(x => x.Add(It.Is<MissionMember>(y => y == testObject)));



        }
    }
}
