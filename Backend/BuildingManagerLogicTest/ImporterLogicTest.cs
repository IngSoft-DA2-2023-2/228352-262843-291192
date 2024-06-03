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
    }

}
