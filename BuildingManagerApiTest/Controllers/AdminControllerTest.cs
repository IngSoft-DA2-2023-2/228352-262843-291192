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

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
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
            var mockAdminLogic = new Mock<IAdminLogic>(MockBehavior.Strict);
            mockAdminLogic.Setup(x => x.CreateAdmin(It.IsAny<Admin>())).Returns(_admin);
            var adminController = new AdminController(mockAdminLogic.Object);

            var result = adminController.CreateAdmin(_createAdminRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateAdminResponse;

            mockAdminLogic.VerifyAll();
            Assert.AreEqual(_createAdminResponse, content);
        }

        [TestMethod]
        public void CreateAdminWithEmailInUse()
        {
            var mockAdminLogic = new Mock<IAdminLogic>(MockBehavior.Strict);
            mockAdminLogic.Setup(x => x.CreateAdmin(It.IsAny<Admin>())).Throws(new EmailAlreadyInUseException(new Exception()));
            var adminController = new AdminController(mockAdminLogic.Object);
            var result = adminController.CreateAdmin(_createAdminRequest);
     
            mockAdminLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void CreateAdminWithNullAttribute()
        {
            var mockAdminLogic = new Mock<IAdminLogic>(MockBehavior.Strict);
            mockAdminLogic.Setup(x => x.CreateAdmin(It.IsAny<Admin>())).Throws(new InvalidArgumentException("name"));
            var adminController = new AdminController(mockAdminLogic.Object);
            var result = adminController.CreateAdmin(_createAdminRequest);

            mockAdminLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
