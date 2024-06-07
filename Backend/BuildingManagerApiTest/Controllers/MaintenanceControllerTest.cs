using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
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
                Password = "pass123",
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
            var mockMaintenanceLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockMaintenanceLogic.Setup(x => x.CreateUser(It.IsAny<MaintenanceStaff>())).Returns(_maintenaceStaff);
            var maintenanceController = new MaintenanceController(mockMaintenanceLogic.Object);

            var result = maintenanceController.CreateMaintenanceStaff(_createMaintenanceStaffRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateMaintenanceStaffResponse;

            mockMaintenanceLogic.VerifyAll();
            Assert.AreEqual(_createMaintenanceStaffResponse, content);
        }

        [TestMethod]
        public void GetMaintainers_OK()
        {
            List<MaintenanceStaff> maintenances = new List<MaintenanceStaff> { _maintenaceStaff };
            var mockMaintenanceLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockMaintenanceLogic.Setup(x => x.GetMaintenanceStaff()).Returns(maintenances);
            var maintenanceController = new MaintenanceController(mockMaintenanceLogic.Object);
            OkObjectResult expected = new OkObjectResult(new MaintainersResponse(maintenances));
            MaintainersResponse expectedObject = expected.Value as MaintainersResponse;

            OkObjectResult result = maintenanceController.GetMaintainers() as OkObjectResult;
            MaintainersResponse resultObject = result.Value as MaintainersResponse;

            mockMaintenanceLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new CreateMaintenanceStaffResponse(_maintenaceStaff);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new CreateMaintenanceStaffResponse(_maintenaceStaff);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new CreateMaintenanceStaffResponse(_maintenaceStaff);
            var response2 = new CreateMaintenanceStaffResponse(new MaintenanceStaff
            {
                Id = new Guid(),
                Name = "Jane",
                Lastname = "Doe",
                Email = "jane@abc.com",
                Password = "pass456"
            });
            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void MaintainersResponseEquals_NullObject_ReturnsFalse()
        {
            var response = new MaintainersResponse([_maintenaceStaff]);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void MaintainersResponseEquals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new MaintainersResponse([_maintenaceStaff]);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void MaintainersResponseEquals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new MaintainersResponse([_maintenaceStaff]);
            var response2 = new MaintainersResponse([new MaintenanceStaff
            {
                Id = new Guid(),
                Name = "Jane",
                Lastname = "Doe",
                Email = "jane@abc.com",
                Password = "pass456"
            }]);
            Assert.IsFalse(response1.Equals(response2));
        }
    }
}
