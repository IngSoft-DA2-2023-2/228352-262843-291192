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
        public void TestListImporters()
        {
            var mockImporter = new Mock<IImporter>(MockBehavior.Strict);
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            mockImporter.Setup(i => i.Name).Returns("TestImporter");
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);
            importerLogic.Importers.Add(mockImporter.Object);

            var importers = importerLogic.ListImporters();

            mockImporter.VerifyAll();
            Assert.IsTrue(importers.Contains("TestImporter"));
        }

        [TestMethod]
        public void TestImportData()
        {
            Guid managerId = Guid.NewGuid();
            Guid constructionCompanyId = Guid.NewGuid();
            var building = new Building()
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                Name = "Building 1",
                Address = "Address",
                Location = "City",
                ConstructionCompanyId = constructionCompanyId,
                CommonExpenses = 1000
            };
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUserLogic.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(Guid.NewGuid());
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            mockCompanyRepository.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(constructionCompanyId);
            var buildings = new List<Building> { building };
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);
            var mock = new Mock<IImporter>(MockBehavior.Strict);
            mock.Setup(i => i.Name).Returns("defaultJson");
            mock.Setup(i => i.Import(It.IsAny<string>(), (It.IsAny<Guid>()))).Returns(buildings);

            importerLogic.Importers.Add(mock.Object);

            var result = importerLogic.ImportData("defaultJson", "testPath", managerId);

            Assert.AreEqual(buildings, result);
        }

    }

}
