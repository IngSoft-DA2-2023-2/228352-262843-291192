using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class BuildingControllerTest
    {
        private Building _building;
        private CreateBuildingRequest _createBuildingRequest;
        private CreateBuildingResponse _createBuildingResponse;
        private DeleteBuildingRequest _deleteBuildingRequest;
        private DeleteBuildingResponse _deleteBuildingResponse;
        private Guid managerId;

        [TestInitialize]
        public void Initialize()
        {
            managerId = Guid.NewGuid();

            _building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };
            _createBuildingRequest = new CreateBuildingRequest
            {
                Name = "Building",
                ManagerId = managerId,
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };
            _createBuildingResponse = new CreateBuildingResponse(_building);
            
            _deleteBuildingRequest = new DeleteBuildingRequest
            {
                BuildingId = _building.Id
            };
            _deleteBuildingResponse = new DeleteBuildingResponse(_building);
        }

        [TestMethod]
        public void CreateBuilding_Ok()
        {
            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(_building);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = managerId.ToString();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object)
            {
                ControllerContext = controllerContext
            };

            var result = buildingController.CreateBuilding(_createBuildingRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(_createBuildingResponse, content);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            CreateBuildingResponse response = new CreateBuildingResponse(new Building());

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            CreateBuildingResponse response = new CreateBuildingResponse(new Building());
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            CreateBuildingResponse response = new CreateBuildingResponse(new Building());

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            Building building1 = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                ManagerId = new Guid("22222222-2222-2222-2222-222222222222"),
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };

            Building building2 = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                ManagerId = new Guid("33333333-3333-3333-3333-333333333333"),
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };

            CreateBuildingResponse response1 = new CreateBuildingResponse(building1);
            CreateBuildingResponse response2 = new CreateBuildingResponse(building2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
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
                ManagerId = managerId,
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
            
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = managerId.ToString();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object)
            {
                ControllerContext = controllerContext
            };

            var result = buildingController.CreateBuilding(createBuildingWithApartmentRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(createBuildingWithApartmentResponse, content);
        }

        [TestMethod]
        public void CreateBuildingWithOwnerApartment_Ok()
        {
            Apartment apartment = new Apartment
            {
                Floor = 1,
                Number = 1,
                Rooms = 3,
                Bathrooms = 2,
                HasTerrace = true,
                Owner = new Owner
                {
                    Name = "John",
                    LastName = "Doe",
                    Email = "jhonDoe@yahoo.com"
                }
            };

            Building buildingWithOwnerApartment = new Building
            {
                Id = new Guid(),
                ManagerId = managerId,
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingRequest createBuildingWithOwnerApartmentRequest = new CreateBuildingRequest
            {
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingResponse createBuildingWithOwnerApartmentResponse = new CreateBuildingResponse(buildingWithOwnerApartment);

            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(buildingWithOwnerApartment);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = managerId.ToString();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object)
            {
                ControllerContext = controllerContext
            };

            var result = buildingController.CreateBuilding(createBuildingWithOwnerApartmentRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;
            
            Assert.AreEqual(createBuildingWithOwnerApartmentResponse, content);
            mockBuildingLogic.VerifyAll();
        }

        [TestMethod]
        public void DeleteBuilding_Ok()
        {
            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.DeleteBuilding(It.IsAny<Guid>())).Returns(_building);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = managerId.ToString();

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object)
            {
                ControllerContext = controllerContext
            };

            var result = buildingController.DeleteBuilding(_deleteBuildingRequest);
            var okResult = result as CreatedAtActionResult;
            var content = okResult.Value as DeleteBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(_deleteBuildingResponse, content);
        }
    }
}
