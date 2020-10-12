using System;
using CommonLibrary.Model;
using DataAccessLayer.UnitOfWork;
using Moq;

namespace DatabaseUnitTest.Controllers
{
    public interface IReportingController
    {
        User User { get; set; }
        UnitOfWork UnitOfwork { get; set; }


    }

    //[TestClass]
    //public class ReportingControllerTests
    //{
    //    private readonly UnitOfWork unitOfWork;
    //    private readonly BulbasaurDevContext DevContext;
    //    private readonly Registry testObject = new Registry() { RegistryId=1, TaskId=1, UserId=1, Hours=5, Created= DateTime.Today, Date=DateTime.Today};

    //    public ReportingControllerTests()
    //    {
    //        //DevContext = testContext.GetContextWithData();
    //        unitOfWork = new UnitOfWork(DevContext);
    //    }
    //}
}
