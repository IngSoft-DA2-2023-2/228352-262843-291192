using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ManagerControllerTest
    {
        private Manager _manager;

        [TestInitialize]
        public void Initialize()
        {
            _manager = new Manager
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Password = "pass123"
            };
        }

        [TestMethod]
        public void DeleteManager_OK()
        {
            var mockManagerLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockManagerLogic.Setup(x => x.DeleteUser(It.IsAny<Guid>())).Returns(_manager);
            var managerController = new ManagerController(mockManagerLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ManagerResponse(_manager));
            ManagerResponse expectedObject = expected.Value as ManagerResponse;

            OkObjectResult result = managerController.DeleteManager(_manager.Id) as OkObjectResult;
            ManagerResponse resultObject = result.Value as ManagerResponse;

            mockManagerLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        
    }
}
