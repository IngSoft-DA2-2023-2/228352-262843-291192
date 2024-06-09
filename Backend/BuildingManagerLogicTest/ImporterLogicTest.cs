using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIImporter;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class ImporterLogicTest
    {
        [TestMethod]
        public void ListImportersSuccessfully()
        {
            List<IImporter> importers = new List<IImporter>();
            Mock<IImporter> mockJsonImporter = new Mock<IImporter>(MockBehavior.Strict);
            mockJsonImporter.Setup(x => x.Name).Returns("DefaultJson");
            Mock<IImporter> mockXmlImporter = new Mock<IImporter>(MockBehavior.Strict);
            mockXmlImporter.Setup(x => x.Name).Returns("DefaultXml");
            importers.Add(mockJsonImporter.Object);
            importers.Add(mockXmlImporter.Object);
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var companyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(userLogicMock.Object, companyLogicMock.Object, buildingLogicMock.Object);

            var result = importerLogic.ListImporters();

            userLogicMock.VerifyAll();
            companyLogicMock.VerifyAll();
            buildingLogicMock.VerifyAll();
            Assert.AreEqual(importers[0].Name, result[0].Name);
            Assert.AreEqual(importers[1].Name, result[1].Name);
        }

        [TestMethod]
        public void ListImportersNamesSuccessfully()
        {
            List<IImporter> importers = new List<IImporter>();
            Mock<IImporter> mockJsonImporter = new Mock<IImporter>(MockBehavior.Strict);
            mockJsonImporter.Setup(x => x.Name).Returns("DefaultJson");
            Mock<IImporter> mockXmlImporter = new Mock<IImporter>(MockBehavior.Strict);
            mockXmlImporter.Setup(x => x.Name).Returns("DefaultXml");
            importers.Add(mockJsonImporter.Object);
            importers.Add(mockXmlImporter.Object);
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var companyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(userLogicMock.Object, companyLogicMock.Object, buildingLogicMock.Object);

            var result = importerLogic.ListImportersNames();

            userLogicMock.VerifyAll();
            companyLogicMock.VerifyAll();
            buildingLogicMock.VerifyAll();
            Assert.AreEqual(importers[0].Name, result[0]);
            Assert.AreEqual(importers[1].Name, result[1]);
        }

        [TestMethod]
        public void ImportSuccessfully()
        {
            Guid sessionToken = Guid.NewGuid();
            Guid ccadminId = Guid.NewGuid();
            Guid companyId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Guid buildingId = Guid.NewGuid();
            Owner owner = new Owner()
            {
                Email = "juan.perez@gmail.com",
                LastName = "lastname",
                Name = "name"
            };
            Building building = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Lastorres",
                Address = "Av.6deDiciembre 3030, Av.EloyAlfaro",
                Location = "(-0.176,-78.48)",
                ConstructionCompanyId = companyId,
                CommonExpenses = 5000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        BuildingId =buildingId,
                        Floor = 1,
                        Number = 101,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = false,
                        Owner = owner
                    }
                }
            };
            List<Building> buildings = new List<Building> { building };
            string importerName = "DefaultJson";
            string data = "{\"edificios\":[{\"nombre\":\"Lastorres\",\"direccion\":{\"calle_principal\":\"Av.6deDiciembre\",\"numero_puerta\":3030,\"calle_secundaria\":\"Av.EloyAlfaro\"},\"encargado\":null,\"gps\":{\"latitud\":-0.176,\"longitud\":-78.48},\"gastos_comunes\":5000,\"departamentos\":[{\"piso\":1,\"numero_puerta\":101,\"habitaciones\":3,\"conTerraza\":false,\"baños\":2,\"propietarioEmail\":\"juan.perez@gmail.com\"}]}]}";
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var companyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(userLogicMock.Object, companyLogicMock.Object, buildingLogicMock.Object);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(ccadminId);
            companyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            buildingLogicMock.Setup(x => x.GetOwnerFromEmail(It.IsAny<string>())).Returns(owner);
            userLogicMock.Setup(x => x.GetManagerIdFromEmail(It.IsAny<string>())).Returns(managerId);
            buildingLogicMock.Setup(x => x.CheckIfBuildingExists(It.IsAny<Building>())).Returns(false);
            buildingLogicMock.Setup(x => x.CreateBuilding(It.IsAny<Building>(), sessionToken)).Returns(building);

            var result = importerLogic.ImportData(importerName, data, sessionToken);
            building.Id = result[0].Id;
            building.Apartments[0].BuildingId = building.Id;
            userLogicMock.VerifyAll();
            companyLogicMock.VerifyAll();
            buildingLogicMock.VerifyAll();
            Assert.AreEqual(buildings[0], result[0]);
        }

        [TestMethod]
        public void ImportWithInvalidImporterName()
        {
            Guid sessionToken = Guid.NewGuid();
            Guid ccadminId = Guid.NewGuid();
            Guid companyId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Guid buildingId = Guid.NewGuid();
            Owner owner = new Owner()
            {
                Email = "juan.perez@gmail.com",
                LastName = "lastname",
                Name = "name"
            };
            Building building = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Lastorres",
                Address = "Av.6deDiciembre 3030, Av.EloyAlfaro",
                Location = "(-0.176,-78.48)",
                ConstructionCompanyId = companyId,
                CommonExpenses = 5000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        BuildingId =buildingId,
                        Floor = 1,
                        Number = 101,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = false,
                        Owner = owner
                    }
                }
            };
            List<Building> buildings = new List<Building> { building };
            string importerName = "DefaultJson";
            string data = "{\"edificios\":[{\"nombre\":\"Lastorres\",\"direccion\":{\"calle_principal\":\"Av.6deDiciembre\",\"numero_puerta\":3030,\"calle_secundaria\":\"Av.EloyAlfaro\"},\"encargado\":null,\"gps\":{\"latitud\":-0.176,\"longitud\":-78.48},\"gastos_comunes\":5000,\"departamentos\":[{\"piso\":1,\"numero_puerta\":101,\"habitaciones\":3,\"conTerraza\":false,\"baños\":2,\"propietarioEmail\":\"juan.perez@gmail.com\"}]}]}";

            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var companyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(userLogicMock.Object, companyLogicMock.Object, buildingLogicMock.Object);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(ccadminId);
            companyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);

            Exception exception = null;
            try
            {
                importerLogic.ImportData("InvalidImporter", data, sessionToken);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userLogicMock.VerifyAll();
            companyLogicMock.VerifyAll();
            buildingLogicMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }
    }
}
