using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.CustomExceptions;
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
        private Guid managerId;
        private Guid sessionToken;

        [TestInitialize]
        public void Initialize()
        {
            managerId = Guid.NewGuid();
            sessionToken = Guid.NewGuid();

            _building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompanyId = Guid.NewGuid(),
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
            };
            _createBuildingRequest = new CreateBuildingRequest
            {
                Name = "Building",
                ManagerId = managerId,
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
            };
            _createBuildingResponse = new CreateBuildingResponse(_building);
        }

        [TestMethod]
        public void CreateBuilding_Ok()
        {
            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>(), It.IsAny<Guid>())).Returns(_building);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            var result = buildingController.CreateBuilding(_createBuildingRequest, sessionToken);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(_createBuildingResponse, content);
        }

        [TestMethod]
        public void ListBuildings_Ok()
        {
            List<Building> buildings = [_building];

            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.ListBuildings()).Returns(buildings);
            var listBuildingsController = new BuildingController(mockBuildingLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ListBuildingsResponse(buildings));
            ListBuildingsResponse expectedObject = expected.Value as ListBuildingsResponse;

            OkObjectResult result = listBuildingsController.ListBuildings() as OkObjectResult;
            ListBuildingsResponse resultObject = result.Value as ListBuildingsResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }

        [TestMethod]
        public void CreateBuildingWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateBuildingRequest()
                {
                    Name = null,
                    ManagerId = managerId,
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 1000
                };
                requestWithoutName.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutAddress()
        {
            Exception exception = null;
            try
            {
                var requestWithoutAddress = new CreateBuildingRequest()
                {
                    Name = "Building",
                    ManagerId = managerId,
                    Address = null,
                    Location = "City",
                    CommonExpenses = 1000
                };
                requestWithoutAddress.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutLocation()
        {
            Exception exception = null;
            try
            {
                var requestWithoutLocation = new CreateBuildingRequest()
                {
                    Name = "Building",
                    ManagerId = managerId,
                    Address = "1234 Main St",
                    Location = null,
                    CommonExpenses = 1000
                };
                requestWithoutLocation.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutConstructionCompany()
        {
            Exception exception = null;
            try
            {
                var requestWithoutConstructionCompany = new CreateBuildingRequest()
                {
                    Name = "Building",
                    ManagerId = managerId,
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 1000
                };
                requestWithoutConstructionCompany.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutCommonExpenses()
        {
            Exception exception = null;
            try
            {
                var requestWithoutCommonExpenses = new CreateBuildingRequest()
                {
                    Name = "Building",
                    ManagerId = managerId,
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 0
                };
                requestWithoutCommonExpenses.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutApartments()
        {
            Exception exception = null;
            try
            {
                var requestWithoutApartments = new CreateBuildingRequest()
                {
                    Name = "Building",
                    ManagerId = managerId,
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 1000,
                    Apartments = null
                };
                requestWithoutApartments.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
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
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };

            Building building2 = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                ManagerId = new Guid("33333333-3333-3333-3333-333333333333"),
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompanyId = Guid.NewGuid(),
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
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingRequest createBuildingWithApartmentRequest = new CreateBuildingRequest
            {
                Name = "Building",
                Address = "1234 Main St",
                ManagerId = managerId,
                Location = "City",
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingResponse createBuildingWithApartmentResponse = new CreateBuildingResponse(buildingWithApartment);

            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>(), It.IsAny<Guid>())).Returns(buildingWithApartment);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            var result = buildingController.CreateBuilding(createBuildingWithApartmentRequest, sessionToken);
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
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingRequest createBuildingWithOwnerApartmentRequest = new CreateBuildingRequest
            {
                Name = "Building",
                ManagerId = managerId,
                Address = "1234 Main St",
                Location = "City",
                CommonExpenses = 1000,
                Apartments = new List<Apartment> { apartment }
            };

            CreateBuildingResponse createBuildingWithOwnerApartmentResponse = new CreateBuildingResponse(buildingWithOwnerApartment);

            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.CreateBuilding(It.IsAny<Building>(), It.IsAny<Guid>())).Returns(buildingWithOwnerApartment);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            var result = buildingController.CreateBuilding(createBuildingWithOwnerApartmentRequest, sessionToken);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateBuildingResponse;

            Assert.AreEqual(createBuildingWithOwnerApartmentResponse, content);
            mockBuildingLogic.VerifyAll();
        }

        [TestMethod]
        public void DeleteBuilding_Ok()
        {
            var mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.DeleteBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(_building);
            var buildingController = new BuildingController(mockBuildingLogic.Object);
            OkObjectResult expected = new OkObjectResult(new DeleteBuildingResponse(_building));
            DeleteBuildingResponse expectedObject = expected.Value as DeleteBuildingResponse;

            OkObjectResult result = buildingController.DeleteBuilding(_building.Id, sessionToken) as OkObjectResult;
            DeleteBuildingResponse resultObject = result.Value as DeleteBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        [TestMethod]
        public void UpdateBuilding_Ok()
        {
            Building buildingWithFullInformation = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
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
            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.UpdateBuilding(It.IsAny<Building>())).Returns(buildingWithFullInformation);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            UpdateBuildingRequest updateBuildingRequest = new UpdateBuildingRequest
            {
                Id = Guid.NewGuid(),
                Name = "New Building Name",
                ManagerId = buildingWithFullInformation.ManagerId,
                Address = "New Building Address",
                Location = "New City",
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 1,
                        Bathrooms = 2,
                        HasTerrace = true
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 2,
                        HasTerrace = false
                    }
                }
            };
            UpdateBuildingResponse updateBuildingResponse = new UpdateBuildingResponse(buildingWithFullInformation);

            var result = buildingController.UpdateBuilding(buildingWithFullInformation.Id, updateBuildingRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as UpdateBuildingResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(updateBuildingResponse, content);
        }

        [TestMethod]
        public void UpdateBuildingWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new UpdateBuildingRequest()
                {
                    Name = null,
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 1000
                };
                requestWithoutName.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void UpdateBuildingWithoutAddress()
        {
            Exception exception = null;
            try
            {
                var requestWithoutAddress = new UpdateBuildingRequest()
                {
                    Name = "Building",
                    Address = null,
                    Location = "City",
                    CommonExpenses = 1000
                };
                requestWithoutAddress.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void UpdateBuildingWithoutLocation()
        {
            Exception exception = null;
            try
            {
                var requestWithoutLocation = new UpdateBuildingRequest()
                {
                    Name = "Building",
                    Address = "1234 Main St",
                    Location = null,
                    CommonExpenses = 1000
                };
                requestWithoutLocation.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void UpdateBuildingWithoutCommonExpenses()
        {
            Exception exception = null;
            try
            {
                var requestWithoutCommonExpenses = new UpdateBuildingRequest()
                {
                    Name = "Building",
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 0
                };
                requestWithoutCommonExpenses.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void UpdateBuildingWithoutApartments()
        {
            Exception exception = null;
            try
            {
                var requestWithoutApartments = new UpdateBuildingRequest()
                {
                    Name = "Building",
                    Address = "1234 Main St",
                    Location = "City",
                    CommonExpenses = 1000,
                    Apartments = null
                };
                requestWithoutApartments.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void UpdateBuildingManager_Ok()
        {
            Building building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
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
                    }
                }
            };
            Guid newManagerId = Guid.NewGuid();
            Mock<IBuildingLogic> mockBuildingLogic = new Mock<IBuildingLogic>(MockBehavior.Strict);
            mockBuildingLogic.Setup(x => x.ModifyBuildingManager(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(newManagerId);
            BuildingController buildingController = new BuildingController(mockBuildingLogic.Object);

            UpdateBuildingManagerRequest updateBuildingManagerRequest = new UpdateBuildingManagerRequest
            {
                ManagerId = newManagerId,
            };

            OkObjectResult expected = new OkObjectResult(new UpdateBuildingManagerResponse(newManagerId, building.Id));
            UpdateBuildingManagerResponse expectedObject = expected.Value as UpdateBuildingManagerResponse;

            OkObjectResult result = buildingController.UpdateBuildingManager(building.Id, updateBuildingManagerRequest) as OkObjectResult;
            UpdateBuildingManagerResponse resultObject = result.Value as UpdateBuildingManagerResponse;

            mockBuildingLogic.VerifyAll();
            Assert.AreEqual(expected.StatusCode, result.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }
    }
}
