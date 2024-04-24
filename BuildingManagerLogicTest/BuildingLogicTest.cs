using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]

    public class BuildingLogicTest
    {
        private Building _building;

        [TestInitialize]
        public void Initialize()
        {
            _building = new Building()
            {
                Id = new Guid(),
                ManagerId = new Guid(),
                Name = "Building 1",
                Address = "Address",
                Location = "City",
                ConstructionCompany = "Company",
                CommonExpenses = 1000
            };
        }

        [TestMethod]
        public void CreateBuildingSuccessfully()
        {
            var buildingRespositoryMock = new Mock<IBuildingRepository>(MockBehavior.Strict);
            buildingRespositoryMock.Setup(x => x.CreateBuilding(It.IsAny<Building>())).Returns(_building);
            var buildingLogic = new BuildingLogic(buildingRespositoryMock.Object);

            var result = buildingLogic.CreateBuilding(_building);

            buildingRespositoryMock.VerifyAll();
            Assert.AreEqual(_building, result);

        }
    }
}
