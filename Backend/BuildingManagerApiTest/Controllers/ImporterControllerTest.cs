using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class ImporterControllerTest
    {
        [TestMethod]
        public void GetImporters_ReturnsListOfImporters()
        {
            var importers = new ListImportersResponse( new List<string> { "JSON", "XML"});
            var mockImporterLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockImporterLogic.Setup(r => r.ListImporters()).Returns(new List<string> { "JSON", "XML" });
            var importerController = new ImporterController(mockImporterLogic.Object);

            var result = importerController.GetImporters() as OkObjectResult;
            var resultObject = result.Value as ListImportersResponse;

            mockImporterLogic.VerifyAll();
            Assert.AreEqual(importers, resultObject);
        }

        [TestMethod]
        public void ImportData_CreatesDataAndReturnsCorrectResponse()
        {
            var importerName = "TestImporter";
            var path = "test/path";
            var buildings = new List<Building>
            {
                new Building
                {
                    Id = Guid.NewGuid(),
                    ManagerId = Guid.NewGuid(),
                    Name = "Edificio Central",
                    Address = "123 Calle Principal",
                    Location = "Montevideo",
                    ConstructionCompanyId = Guid.NewGuid(),
                    CommonExpenses = 1500.00m,
                    Apartments = new List<Apartment>
                    {
                        new Apartment { Owner = new Owner() },
                    }
                },
            };
            var expectedData = new ImportBuildingsResponse(buildings);
            var mockImporterLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            var comanyAdminId = new Guid();
            mockImporterLogic.Setup(r => r.ImportData(importerName, path, comanyAdminId)).Returns(buildings);
            var importerController = new ImporterController(mockImporterLogic.Object);

            var result = importerController.ImportData(importerName, path, comanyAdminId) as CreatedAtActionResult;
            var resultObject = result.Value as ImportBuildingsResponse;

            mockImporterLogic.VerifyAll();
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(expectedData, resultObject);
        }

        [TestMethod]
        public void Equals_ReturnsFalse_WhenOtherIsNull()
        {
            var response1 = new ImportBuildingsResponse(new List<Building>());
            ImportBuildingsResponse response2 = null;

            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void Equals_ReturnsFalse_WhenOtherIsDifferentType()
        {
            var response1 = new ImportBuildingsResponse(new List<Building>());
            var notResponse = new object();

            Assert.IsFalse(response1.Equals(notResponse));
        }

        [TestMethod]
        public void Equals_ReturnsFalse_WhenBuildingsCountDiffers()
        {
            var response1 = new ImportBuildingsResponse(new List<Building> { new Building() });
            var response2 = new ImportBuildingsResponse(new List<Building>());

            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void Equals_ReturnsFalse_WhenBuildingsDiffer()
        {
            var building1 = new Building { Id = Guid.NewGuid(), Name = "Building 1" };
            var building2 = new Building { Id = Guid.NewGuid(), Name = "Building 2" };

            var response1 = new ImportBuildingsResponse(new List<Building> { building1 });
            var response2 = new ImportBuildingsResponse(new List<Building> { building2 });

            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void Equals_ReturnsTrue_WhenBuildingsAreEqual()
        {
            var building1 = new Building { Id = Guid.NewGuid(), Name = "Building 1" };
            var building2 = new Building { Id = building1.Id, Name = building1.Name };

            var response1 = new ImportBuildingsResponse(new List<Building> { building1 });
            var response2 = new ImportBuildingsResponse(new List<Building> { building2 });

            Assert.IsTrue(response1.Equals(response2));
        }

        [TestMethod]
        public void ListImportersResponse_Equals_ReturnsFalse_WhenOtherIsNull()
        {
            var response1 = new ListImportersResponse(new List<string> { "JSON", "XML" });
            ListImportersResponse response2 = null;

            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void ListImportersResponse_Equals_ReturnsFalse_WhenOtherIsDifferentType()
        {
            var response1 = new ListImportersResponse(new List<string> { "JSON", "XML" });
            var notResponse = new object();

            Assert.IsFalse(response1.Equals(notResponse));
        }

        [TestMethod]
        public void ListImportersResponse_Equals_ReturnsFalse_WhenImportersCountDiffers()
        {
            var response1 = new ListImportersResponse(new List<string> { "JSON", "XML" });
            var response2 = new ListImportersResponse(new List<string> { "JSON" });

            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void ListImportersResponse_Equals_ReturnsFalse_WhenImportersDiffer()
        {
            var response1 = new ListImportersResponse(new List<string> { "JSON", "XML" });
            var response2 = new ListImportersResponse(new List<string> { "JSON", "CSV" });

            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void ListImportersResponse_Equals_ReturnsTrue_WhenImportersAreEqual()
        {
            var response1 = new ListImportersResponse(new List<string> { "JSON", "XML" });
            var response2 = new ListImportersResponse(new List<string> { "JSON", "XML" });

            Assert.IsTrue(response1.Equals(response2));
        }
    }
}
