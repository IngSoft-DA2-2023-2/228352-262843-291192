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
            mockUserLogic.VerifyAll();
            mockCompanyRepository.VerifyAll();
            Assert.IsTrue(importerLogic.Importers.Contains(mockImporter.Object));
        }

        [TestMethod]
        public void RegisterDuplicateImporterThrowsException()
        {
            var mockImporter1 = new Mock<IImporter>(MockBehavior.Strict);
            mockImporter1.Setup(i => i.Name).Returns("TestImporter");
            var mockImporter2 = new Mock<IImporter>(MockBehavior.Strict);
            mockImporter2.Setup(i => i.Name).Returns("TestImporter");
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);

            importerLogic.RegisterImporter(mockImporter1.Object);

            Exception thrownException = null;
            try
            {
                importerLogic.RegisterImporter(mockImporter2.Object);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }
            mockImporter1.VerifyAll();
            mockImporter2.VerifyAll();
            mockUserLogic.VerifyAll();
            mockCompanyRepository.VerifyAll();
            Assert.AreEqual("Importer with name TestImporter is already registered.", thrownException.Message);
        }
    }
}
