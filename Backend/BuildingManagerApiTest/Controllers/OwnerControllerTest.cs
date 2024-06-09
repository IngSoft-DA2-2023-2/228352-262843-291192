using BuildingManagerApi.Controllers;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerLogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class OwnerControllerTest
    {
        private Owner _owner;
        private CreateOwnerRequest _createOwnerRequest;
        private CreateOwnerResponse _createOwnerResponse;

        [TestInitialize]
        public void Initialize()
        {
            _owner = new Owner
            {
                Name = "John",
                LastName = "Doe",
                Email = "jhon@gmail.com"
            };
            _createOwnerRequest = new CreateOwnerRequest
            {
                Name = "John",
                LastName = "Doe",
                Email = "jhon@gmail.com"
            };
            _createOwnerResponse = new CreateOwnerResponse(_owner);
        }

        [TestMethod]
        public void CreateOwnerTest()
        {
            var mockOwnerLogic = new Mock<IOwnerLogic>(MockBehavior.Strict);
            mockOwnerLogic.Setup(x => x.CreateOwner(It.IsAny<Owner>())).Returns(_owner);
            var ownerController = new OwnerController(mockOwnerLogic.Object);

            var result = ownerController.CreateOwner(_createOwnerRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateOwnerResponse;

            mockOwnerLogic.VerifyAll();
            Assert.AreEqual(_createOwnerResponse, content);
        }   
    }
}
