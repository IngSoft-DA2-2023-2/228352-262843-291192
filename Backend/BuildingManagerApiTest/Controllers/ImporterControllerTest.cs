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
            mockImporterLogic.Setup(r => r.ListImporters()).Returns(importers);
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
            mockImporterLogic.Setup(r => r.ImportData(importerName, path)).Returns(buildings);
            var importerController = new ImporterController(mockImporterLogic.Object);

            var result = importerController.ImportData(importerName, path) as CreatedAtActionResult;
            var resultObject = result.Value as ImportBuildingsResponse;

            mockImporterLogic.VerifyAll();
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(expectedData, resultObject);
        }
    }
}
