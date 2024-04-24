
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerDomain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]

    public class UserLogicTest
    {
        [TestMethod]
        public void CreateAdminSuccessfully()
        {
            var admin = new Admin()
            {
                Id = new Guid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "pass123"
            };
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.CreateUser(It.IsAny<Admin>())).Returns(admin);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.CreateUser(admin);

            userRespositoryMock.VerifyAll();
            Assert.AreEqual(admin, result);
            Assert.AreEqual(RoleType.ADMIN, result.Role);
        }
        [TestMethod]
        public void CreateStaffSuccessfully()
        {
            var maintenanceStaff = new MaintenanceStaff()
            {
                Id = new Guid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "pass123"
            };
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.CreateUser(It.IsAny<MaintenanceStaff>())).Returns(maintenanceStaff);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.CreateUser(maintenanceStaff);

            userRespositoryMock.VerifyAll();
            Assert.AreEqual(maintenanceStaff, result);

            Assert.AreEqual(RoleType.MAINTENANCE, result.Role);
        }

        [TestMethod]
        public void CreateUserWithAlreadyInUseEmail()
        {
            var user = new User()
            {
                Id = new Guid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "pass123"
            };
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).Throws(new ValueDuplicatedException(""));
            var userLogic = new UserLogic(userRespositoryMock.Object);
            Exception exception = null;
            try
            {
                userLogic.CreateUser(user);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRespositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void FindUserByIdTest()
        {
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.Exists(It.IsAny<Guid>())).Returns(true);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.Exists(new Guid());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserRoleTest()
        {
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.Role(It.IsAny<Guid>())).Returns(RoleType.ADMIN);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.Role(new Guid());

            Assert.AreEqual(RoleType.ADMIN, result);
        }
    }
}
