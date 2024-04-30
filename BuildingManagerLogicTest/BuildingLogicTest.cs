using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]

    public class BuildingLogicTest
    {
        private Building _building;

        [TestInitialize]
        public void Initialize()
        {
            _building = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 1",
                Address = "Address",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };
        }

        [TestMethod]
        public void CreateBuildingSuccessfully()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(_building);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);

            var result = buildingLogic.CreateBuilding(_building);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(_building, result);

        }

        [TestMethod]
        public void CreateBuildingWithAlreadyInUseName()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Throws(new ValueDuplicatedException(""));
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);
            Exception exception = null;
            try
            {
                buildingLogic.CreateBuilding(_building);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentWithSameFloorAndNumberTest() {             
            Building buildingWithApartmentWithSameFloorAndNumber = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 1",
                Address = "Address",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 2,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 1,
                        Rooms = 1,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Eod",
                            Email = "jhoneod@gmail.com"
                        }
                    }
                }
            };
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);
            Exception exception = null;

            try
            {
                buildingLogic.CreateBuilding(buildingWithApartmentWithSameFloorAndNumber);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentWithSameOwnerEmailTest()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);
            Exception exception = null;

            Building buildingWithSameOwnerEmail = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 2",
                Address = "Address",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
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
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 3,
                        Rooms = 1,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Eod",
                            Email = "jhon@gmail.com"
                        }
                    }
                }
            };

            try
            {
                buildingLogic.CreateBuilding(buildingWithSameOwnerEmail);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void DeleteBuildingSuccessfully()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.DeleteBuilding(It.IsAny<Guid>())).Returns(_building);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);

            var result = buildingLogic.DeleteBuilding(_building.Id);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(_building, result);
        }

        [TestMethod]
        public void GetManagerIdBySessionTokenTest()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.GetManagerIdBySessionToken(It.IsAny<Guid>())).Returns(_building.ManagerId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);

            var result = buildingLogic.GetManagerIdBySessionToken(_building.ManagerId);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(_building.ManagerId, result);
        }

        [TestMethod]
        public void UpdateBuildingSuccessfully()
        {
            Building buildingUpdated = new Building
            {
                Id = _building.Id,
                ManagerId = _building.ManagerId,
                Name = "Building",
                Address = "1234 Main St",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
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
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "Jade",
                            LastName = "Doe",
                            Email = "jade@gmail.com"
                        }
                    }
                }
            };
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.UpdateBuilding(It.IsAny<Building>())).Returns(buildingUpdated);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);

            var result = buildingLogic.UpdateBuilding(buildingUpdated);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(buildingUpdated, result);
        }
    }
}