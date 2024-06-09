using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
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
    public class ManagerControllerTest
    {
        private Manager _manager;
        private Building _building;

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

            _building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = _manager.Id,
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true
                    }
                }
            };
        }

        [TestMethod]
        public void DeleteManager_OK()
        {
            var mockManagerLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockManagerLogic.Setup(x => x.DeleteUser(It.IsAny<Guid>(), It.IsAny<RoleType>())).Returns(_manager);
            var managerController = new ManagerController(mockManagerLogic.Object, mockBuildingLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ManagerResponse(_manager));
            ManagerResponse expectedObject = expected.Value as ManagerResponse;

            OkObjectResult result = managerController.DeleteManager(_manager.Id) as OkObjectResult;
            ManagerResponse resultObject = result.Value as ManagerResponse;

            mockManagerLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        [TestMethod]
        public void GetManagerBuildings_OK()
        {
            List<Building> buildings = new List<Building> { _building };
            var mockManagerLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.GetManagerBuildings(It.IsAny<Guid>())).Returns(buildings);
            var managerController = new ManagerController(mockManagerLogic.Object, mockBuildingLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ManagerBuildingsResponse(buildings));
            ManagerBuildingsResponse expectedObject = expected.Value as ManagerBuildingsResponse;

            OkObjectResult result = managerController.GetManagerBuildings(_manager.Id) as OkObjectResult;
            ManagerBuildingsResponse resultObject = result.Value as ManagerBuildingsResponse;

            mockManagerLogic.VerifyAll();
            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }

        [TestMethod]
        public void ListManagerUsers_OK()
        {
            List<Manager> managers = new List<Manager> { _manager };
            var mockManagerLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockManagerLogic.Setup(x => x.GetManagers()).Returns(managers);
            var managerController = new ManagerController(mockManagerLogic.Object, mockBuildingLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ListManagersResponse(managers));
            ListManagersResponse expectedObject = expected.Value as ListManagersResponse;

            OkObjectResult result = managerController.ListManagerUsers() as OkObjectResult;
            ListManagersResponse resultObject = result.Value as ListManagersResponse;

            mockManagerLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
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

        [TestMethod]
        public void ManagerBuildingsEquals_NullObject_ReturnsFalse()
        {
            var response = new ManagerBuildingsResponse([_building]);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void ManagerBuildingsEquals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new ManagerBuildingsResponse([_building]);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void ManagerBuildingsEquals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new ManagerBuildingsResponse([_building]);
            var response2 = new ManagerBuildingsResponse([new Building
            {
                Id = new Guid(),
                Name = "Another Building",
                Address = "1234 Main St",
                Location = "City",
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true
                    }
                }
            }]);
            Assert.IsFalse(response1.Equals(response2));
        }
    }
}
