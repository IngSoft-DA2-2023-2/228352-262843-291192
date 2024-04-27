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


        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new ManagerResponse(_manager);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new ManagerResponse(_manager);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new ManagerResponse(_manager);
            var response2 = new ManagerResponse(new Manager
            {
                Id = new Guid(),
                Name = "Jane",
                Email = "jane@abc.com",
                Password = "pass456"
            });
            Assert.IsFalse(response1.Equals(response2));
        }
    }
}
