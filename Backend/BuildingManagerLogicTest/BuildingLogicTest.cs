using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
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
        private Guid sessionToken;
        private Guid companyId;
        private Guid userId;

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
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };

            sessionToken = Guid.NewGuid();
            companyId = Guid.NewGuid();
            userId = Guid.NewGuid();
        }

        [TestMethod]
        public void CreateBuildingSuccessfully()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            constructionCompanyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            buildingRespositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(_building);
            buildingRespositoryMock.Setup(x => x.GetUserIdBySessionToken(It.IsAny<Guid>())).Returns(userId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);

            var result = buildingLogic.CreateBuilding(_building, sessionToken);

            buildingRespositoryMock.VerifyAll();
            constructionCompanyLogicMock.VerifyAll();
            Assert.AreEqual(_building, result);

        }

        [TestMethod]
        public void ListBuildingsSuccessfully()
        {
            var buildingsList = new List<Building> { _building };
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(_building);
            buildingRespositoryMock.Setup(x => x.ListBuildings()).Returns(buildingsList);
            buildingRespositoryMock.Setup(x => x.GetUserIdBySessionToken(It.IsAny<Guid>())).Returns(userId);
            constructionCompanyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);
            buildingLogic.CreateBuilding(_building, sessionToken);

            var result = buildingLogic.ListBuildings();

            buildingRespositoryMock.VerifyAll();
            constructionCompanyLogicMock.VerifyAll();
            Assert.AreEqual(buildingsList, result);

        }

        [TestMethod]
        public void CreateBuildingWithAlreadyInUseName()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            constructionCompanyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            buildingRespositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Throws(new ValueDuplicatedException(""));
            buildingRespositoryMock.Setup(x => x.GetUserIdBySessionToken(It.IsAny<Guid>())).Returns(userId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);
            Exception exception = null;
            try
            {
                buildingLogic.CreateBuilding(_building, sessionToken);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentWithSameFloorAndNumberTest()
        {
            Building buildingWithApartmentWithSameFloorAndNumber = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 1",
                Address = "Address",
                Location = "City",
                ConstructionCompanyId = Guid.NewGuid(),
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
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            constructionCompanyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);
            buildingRespositoryMock.Setup(x => x.GetUserIdBySessionToken(It.IsAny<Guid>())).Returns(userId);
            Exception exception = null;

            try
            {
                buildingLogic.CreateBuilding(buildingWithApartmentWithSameFloorAndNumber, sessionToken);
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
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            constructionCompanyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.GetUserIdBySessionToken(It.IsAny<Guid>())).Returns(userId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);
            Exception exception = null;

            Building buildingWithSameOwnerEmail = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 2",
                Address = "Address",
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
                buildingLogic.CreateBuilding(buildingWithSameOwnerEmail, sessionToken);
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
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.DeleteBuilding(It.IsAny<Guid>())).Returns(_building);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);

            var result = buildingLogic.DeleteBuilding(_building.Id, sessionToken);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(_building, result);
        }

        [TestMethod]
        public void GetUserIdBySessionTokenTest()
        {
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.GetUserIdBySessionToken(It.IsAny<Guid>())).Returns(userId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);

            var result = buildingLogic.GetUserIdBySessionToken(sessionToken);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(userId, result);
        }

        [TestMethod]
        public void GetConstructionCompanyIdFromBuildingIdTest()
        {
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.GetConstructionCompanyFromBuildingId(It.IsAny<Guid>())).Returns(companyId);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);

            var result = buildingLogic.GetConstructionCompanyFromBuildingId(_building.Id);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(companyId, result);
        }

        [TestMethod]
        public void GetConstructionCompanyFromInvalidBuildingIdTest()
        {
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.GetConstructionCompanyFromBuildingId(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);

            Exception exception = null;

            try
            {
                buildingLogic.GetConstructionCompanyFromBuildingId(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            buildingRespositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
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
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.UpdateBuilding(It.IsAny<Building>())).Returns(buildingUpdated);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);

            var result = buildingLogic.UpdateBuilding(buildingUpdated);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(buildingUpdated, result);
        }

        [TestMethod]
        public void UpdateBuildingWithApartmentWithSameFloorAndNumberTest()
        {
            Building buildingUpdated = new Building
            {
                Id = _building.Id,
                ManagerId = _building.ManagerId,
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
                        Floor = 1,
                        Number = 1,
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
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);
            Exception exception = null;

            try
            {
                buildingLogic.UpdateBuilding(buildingUpdated);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void UpdateBuildingWithApartmentWithSameOwnerEmailTest()
        {
            Building buildingUpdated = new Building
            {
                Id = _building.Id,
                ManagerId = _building.ManagerId,
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
                            Email = "jane@gmail.com"
                        }
                    }
                }
            };
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object, constructionCompanyLogicMock.Object);
            Exception exception = null;

            try
            {
                buildingLogic.UpdateBuilding(buildingUpdated);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }
    }
}