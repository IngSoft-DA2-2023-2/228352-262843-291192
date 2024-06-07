using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class ImporterLogicTests
    {
        [TestMethod]
        public void LoadImportersFromAssemblySuccessfully()
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var mockBuildingRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);
            string assemblyPath = ".\\BuildingManagerLogic.dll";
            int initialCount = importerLogic.Importers.Count;

            importerLogic.LoadImportersFromAssembly(assemblyPath, mockUserRepository.Object, mockBuildingRepository.Object);

            mockUserRepository.VerifyAll();
            mockBuildingRepository.VerifyAll();
            mockUserLogic.VerifyAll();
            mockCompanyRepository.VerifyAll();
            Assert.AreEqual(initialCount + 1, importerLogic.Importers.Count);
        }

        [TestMethod]
        public void TestLoadImportersFromNonexistentAssembly()
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var mockBuildingRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);
            string assemblyPath = "path/to/nonexistent/assembly.dll";

            Exception thrownException = null;
            try
            {
                importerLogic.LoadImportersFromAssembly(assemblyPath, mockUserRepository.Object, mockBuildingRepository.Object);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            mockUserRepository.VerifyAll();
            mockBuildingRepository.VerifyAll();
            mockUserRepository.VerifyAll();
            mockUserLogic.VerifyAll();
            Assert.IsInstanceOfType(thrownException, typeof(FileNotFoundException));
        }

        [TestMethod]
        public void LoadImportersFromAssemblyDuplicateThrowsException()
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var mockBuildingRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            var mockCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var importerLogic = new ImporterLogic(mockUserLogic.Object, mockCompanyRepository.Object);
            string assemblyPath = ".\\BuildingManagerLogic.dll";
            importerLogic.LoadImportersFromAssembly(assemblyPath, mockUserRepository.Object, mockBuildingRepository.Object);

            Exception thrownException = null;
            try
            {
                importerLogic.LoadImportersFromAssembly(assemblyPath, mockUserRepository.Object, mockBuildingRepository.Object);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            mockUserRepository.VerifyAll();
            mockBuildingRepository.VerifyAll();
            mockUserRepository.VerifyAll();
            mockUserLogic.VerifyAll();
            Assert.IsInstanceOfType(thrownException, typeof(Exception));
        }

    }
}
