using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class BuildingControllerTest
    {
        private Building _building;
        private CreateBuildingRequest _createBuildingRequest;
        private CreateBuildingResponse _createBuildingResponse;

        [TestInitialize]
        public void Initialize()
        {
            _building = new Building
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };
            _createBuildingRequest = new CreateBuildingRequest
            {
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };
            _createBuildingResponse = new CreateBuildingResponse(_building);
        }

        [TestMethod]
        public void CreateBuilding_Ok()
        {
            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(_building);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            var result = buildingController.CreateBuilding(_createBuildingRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(_createBuildingResponse, content);
        }

        [TestMethod]
        public void CreateBuildingWithApartment_Ok()
        {
            Apartment apartment = new Apartment
            {
                Floor = 1,
                Number = 1,
                Rooms = 3,
                Bathrooms = 2,
                HasTerrace = true
            };

            Building buildingWithApartment = new Building
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingRequest createBuildingWithApartmentRequest = new CreateBuildingRequest
            {
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingResponse createBuildingWithApartmentResponse = new CreateBuildingResponse(buildingWithApartment);

            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(buildingWithApartment);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            var result = buildingController.CreateBuilding(createBuildingWithApartmentRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(createBuildingWithApartmentResponse, content);
        }
    }
}
