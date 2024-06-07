using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
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
        public void RegisterImporterSuccessfully()
        {
            var mockImporter = new Mock<IImporter>(MockBehavior.Strict);
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);

            importerLogic.RegisterImporter(mockImporter.Object);

            mockImporter.VerifyAll();
            Assert.IsTrue(importers.Contains("TestImporter"));
            mockCompanyRepository.VerifyAll();
            Assert.IsTrue(importerLogic.Importers.Contains(mockImporter.Object));
        }

    }

}
