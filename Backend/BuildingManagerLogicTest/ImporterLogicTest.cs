using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIImporter;
using BuildingManagerILogic;
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
    }
}
