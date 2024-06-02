using BuildingManagerApi.Controllers;
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
    }
}
