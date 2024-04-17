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
    public class MaintenanceControllerTest
    {
        private MaintenanceStaff _maintenaceStaff;
        private CreateMaintenanceStaffRequest _createMaintenanceStaffRequest;
        private CreateMaintenanceStaffResponse _createMaintenanceStaffResponse;

        [TestInitialize]
        public void Initialize()
        {
            _maintenaceStaff = new MaintenanceStaff
            {
                Id = new Guid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123"
            };
            _createMaintenanceStaffRequest = new CreateMaintenanceStaffRequest
            {
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123"
            };
            _createMaintenanceStaffResponse = new CreateMaintenanceStaffResponse(_maintenaceStaff);

        }
        [TestMethod]
        public void CreateMaintenanceStaff_Ok()
        {
            var mockMaintenanceLogic = new Mock<IMaintenanceLogic>(MockBehavior.Strict);
            mockMaintenanceLogic.Setup(x => x.CreateMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Returns(_maintenaceStaff);
            var maintenanceController = new MaintenanceController(mockMaintenanceLogic.Object);

            var result = maintenanceController.CreateMaintenanceStaff(_createMaintenanceStaffRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateMaintenanceStaffResponse;

            mockMaintenanceLogic.VerifyAll();
            Assert.AreEqual(_createMaintenanceStaffResponse, content);
        }

        [TestMethod]
        public void CreateMaintenanceStaffWithEmailInUse()
        {
            var mockMaintenanceLogic = new Mock<IMaintenanceLogic>(MockBehavior.Strict);
            mockMaintenanceLogic.Setup(x => x.CreateMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Throws(new DuplicatedValueException(new Exception(),""));
            var maintenanceController = new MaintenanceController(mockMaintenanceLogic.Object);
            var result = maintenanceController.CreateMaintenanceStaff(_createMaintenanceStaffRequest);
     
            mockMaintenanceLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreateMaintenanceStaffWithNullAttribute()
        {
            var mockMaintenanceLogic = new Mock<IMaintenanceLogic>(MockBehavior.Strict);
            mockMaintenanceLogic.Setup(x => x.CreateMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Throws(new InvalidArgumentException("name"));
            var maintenanceController = new MaintenanceController(mockMaintenanceLogic.Object);
            var result = maintenanceController.CreateMaintenanceStaff(_createMaintenanceStaffRequest);

            mockMaintenanceLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreateMaintenanceStaffServerError()
        {
            var mockMaintenanceLogic = new Mock<IMaintenanceLogic>(MockBehavior.Strict);
            mockMaintenanceLogic.Setup(x => x.CreateMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Throws(new Exception());
            var maintenanceController = new MaintenanceController(mockMaintenanceLogic.Object);
            var result = maintenanceController.CreateMaintenanceStaff(_createMaintenanceStaffRequest);

            mockMaintenanceLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            Assert.AreEqual((result as StatusCodeResult).StatusCode, 500);
        }

    }
}
