using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
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
    public class AdminControllerTest
    {
        private Admin _admin;
        private CreateAdminRequest _createAdminRequest;
        private CreateAdminResponse _createAdminResponse;

        [TestInitialize]
        public void Initialize()
        {
            _admin = new Admin
            {
                Id = new Guid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123"
            };
            _createAdminRequest = new CreateAdminRequest
            {
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123"
            };
            _createAdminResponse = new CreateAdminResponse(_admin);

        }
        [TestMethod]
        public void CreateAdmin_Ok()
        {
            var mockAdminLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockAdminLogic.Setup(x => x.CreateUser(It.IsAny<Admin>())).Returns(_admin);
            var adminController = new AdminController(mockAdminLogic.Object);

            var result = adminController.CreateAdmin(_createAdminRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateAdminResponse;

            mockAdminLogic.VerifyAll();
            Assert.AreEqual(_createAdminResponse, content);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new CreateAdminResponse(_admin);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new CreateAdminResponse(_admin);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new CreateAdminResponse(_admin);
            var response2 = new CreateAdminResponse(new Admin
            {
                Id = new Guid(),
                Name = "Jane",
                Lastname = "Doe",
                Email = "jane@abc.com",
                Password = "pass456"
            });
            Assert.IsFalse(response1.Equals(response2));
        }
    }
}
