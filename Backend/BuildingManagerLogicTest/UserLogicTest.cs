
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
        public void LoginSuccessfully()
        {
            string email = "test@test.com";
            string password = "somepassword";
            Guid token = new("12345678-1234-1234-1234-123456789012");
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(token);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.Login(email, password);

            userRespositoryMock.VerifyAll();
            Assert.AreEqual(token, result);
        }

        [TestMethod]
        public void LogoutSuccessfully()
        {
            Guid token = new("12345678-1234-1234-1234-123456789012");
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.Logout(It.IsAny<Guid>())).Returns(token);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.Logout(token);

            userRespositoryMock.VerifyAll();
            Assert.AreEqual(token, result);
        }

        [TestMethod]
        public void FindUserByIdTest()
        {
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.ExistsFromSessionToken(It.IsAny<Guid>())).Returns(true);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.ExistsFromSessionToken(new Guid());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserRoleTest()
        {
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.RoleFromSessionToken(It.IsAny<Guid>())).Returns(RoleType.ADMIN);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.RoleFromSessionToken(new Guid());

            Assert.AreEqual(RoleType.ADMIN, result);
        }

        [TestMethod]
        public void DeleteUserSuccessfully()
        {
            var manager = new Manager()
            {
                Id = new Guid(),
                Name = "John",
                Email = "abc@example.com",
                Password = "pass123",
                Role = RoleType.MANAGER
            };
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.DeleteUser(It.IsAny<Guid>(), It.IsAny<RoleType>())).Returns(manager);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.DeleteUser(manager.Id, manager.Role);

            userRespositoryMock.VerifyAll();
            Assert.AreEqual(manager, result);
        }

        [TestMethod]
        public void DeleteUserWithInvalidId()
        {
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<Guid>(), It.IsAny<RoleType>())).Throws(new ValueNotFoundException(""));
            var userLogic = new UserLogic(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                userLogic.DeleteUser(new Guid(), RoleType.MANAGER);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void LoginUserWithInvalidEmailAndPassword()
        {
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Throws(new ValueNotFoundException(""));
            var userLogic = new UserLogic(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                userLogic.Login("some email", "some password");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void LogoutUserWithInvalidToken()
        {
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.Logout(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var userLogic = new UserLogic(userRepositoryMock.Object);
            Exception exception = null;

            try
            {
                userLogic.Logout(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetUserRoleFromInvalidTokenTest()
        {
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.RoleFromSessionToken(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var userLogic = new UserLogic(userRespositoryMock.Object);

            Exception exception = null;

            try
            {
                userLogic.RoleFromSessionToken(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRespositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetUserIdFromSessionTokenTest()
        {
            Guid userId = Guid.NewGuid();
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.GetUserIdFromSessionToken(Guid.NewGuid());

            Assert.AreEqual(userId, result);
        }

        [TestMethod]
        public void GetUserIdFromInvalidTokenTest()
        {
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var userLogic = new UserLogic(userRespositoryMock.Object);

            Exception exception = null;

            try
            {
                userLogic.GetUserIdFromSessionToken(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            userRespositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetMaintenanceStaffTest()
        {
            List<MaintenanceStaff> maintenanceStaff =
            [
                new MaintenanceStaff(){ Id = new Guid(),
                   Name =  "John",
                   Lastname = "Doe",
                  Email=  "mail@mail.com",
                   Password =  "pass1234"}

             ];
            var userRespositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRespositoryMock.Setup(x => x.GetMaintenanceStaff()).Returns(maintenanceStaff);
            var userLogic = new UserLogic(userRespositoryMock.Object);

            var result = userLogic.GetMaintenanceStaff();

            Assert.AreEqual(maintenanceStaff, result);
        }
    }
}
