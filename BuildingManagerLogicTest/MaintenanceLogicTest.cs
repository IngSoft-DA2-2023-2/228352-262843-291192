
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class MaintenanceLogicTest
    {
        private MaintenanceStaff _maintenanceStaff;

        [TestInitialize]
        public void Initialize()
        {
            _maintenanceStaff = new MaintenanceStaff()
            {
                Id = new Guid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123"
            };
        }

        [TestMethod]
        public void CreateAdminSuccessfully()
        {
            var maintenanceStaffRepositoryMock = new Mock<IMaintenanceRepository>(MockBehavior.Strict);
            maintenanceStaffRepositoryMock.Setup(x => x.CreateMaintenanceStaff(It.IsAny<MaintenanceStaff>())).Returns(_maintenanceStaff);
            var maintenanceLogic = new MaintenaceLogic(maintenanceStaffRepositoryMock.Object);

            var result = maintenanceLogic.CreateMaintenanceStaff(_maintenanceStaff);

            maintenanceStaffRepositoryMock.VerifyAll();
            Assert.AreEqual(_maintenanceStaff, result);
        }
    }
}
