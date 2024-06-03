using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class ImporterLogicTests
    {

        [TestMethod]
        public void TestListImporters()
        {
            var mockImporter = new Mock<IImporter>(MockBehavior.Strict);
            mockImporter.Setup(i => i.Name).Returns("TestImporter");
            var importerLogic = new ImporterLogic();
            importerLogic.Importers.Add(mockImporter.Object);

            var importers = importerLogic.ListImporters();

            mockImporter.VerifyAll();
            Assert.IsTrue(importers.Contains("TestImporter"));
        }

        [TestMethod]
        public void TestImportData()
        {
            var building = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 1",
                Address = "Address",
                Location = "City",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };
            var buildings = new List<Building> { building };
            var importerLogic = new ImporterLogic();
            var mock = new Mock<IImporter>(MockBehavior.Strict);
            mock.Setup(i => i.Name).Returns("defaultJson");
            mock.Setup(i => i.Import(It.IsAny<string>())).Returns(buildings);

            importerLogic.Importers.Add(mock.Object);

            var result = importerLogic.ImportData("defaultJson", "testPath");

            Assert.AreEqual(buildings, result);
        }

    }

}
