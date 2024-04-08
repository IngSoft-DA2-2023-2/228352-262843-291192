
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class AdminLogicTest
    {
        private Admin _admin;

        [TestInitialize]
        public void Initialize()
        {
            _admin = new Admin()
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
            var adminRespositoryMock = new Mock<IAdminRepository>(MockBehavior.Strict);
            adminRespositoryMock.Setup(x => x.CreateAdmin(It.IsAny<Admin>())).Returns(_admin);
            var adminLogic = new AdminLogic(adminRespositoryMock.Object);

            var result = adminLogic.CreateAdmin(_admin);

            adminRespositoryMock.VerifyAll();
            Assert.AreEqual(_admin, result);

        }
    }
}
