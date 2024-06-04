using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserRepositoryTest
    {

        [TestMethod]
        public void CreateAdminTest()
        {
            var context = CreateDbContext("CreateAdminTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@exmaple.com",
                Password = "123456",
            };

            repository.CreateUser(admin);
            var result = context.Set<User>().Find(admin.Id);

            Assert.AreEqual(RoleType.ADMIN, result.Role);
        }

        [TestMethod]
        public void CreateMaintenanceStaffTest()
        {
            var context = CreateDbContext("CreateMaintenanceStaffTest");
            var repository = new UserRepository(context);
            var maintenanceSatff = new MaintenanceStaff
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@exmaple.com",
                Password = "123456",
            };

            repository.CreateUser(maintenanceSatff);
            var result = context.Set<User>().Find(maintenanceSatff.Id);

            Assert.AreEqual(RoleType.MAINTENANCE, result.Role);
        }

        [TestMethod]
        public void CreateUserWithDuplicatedEmailTest()
        {
            var context = CreateDbContext("CreateAdminWithDuplicatedEmailTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
            };
            repository.CreateUser(admin);

            Exception exception = null;
            try
            {
                repository.CreateUser(admin);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueDuplicatedException));
        }

        [TestMethod]
        public void CheckIfUserExistsTest()
        {
            var context = CreateDbContext("CheckIfUSerExistsTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
            };
            repository.CreateUser(admin);
            Guid token = repository.Login(admin.Email, admin.Password);

            var result = repository.ExistsFromSessionToken(token);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfEmailExistsTest()
        {
            var context = CreateDbContext("CheckIfEmailExistsTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
            };
            repository.CreateUser(admin);

            var result = repository.EmailExists(admin.Email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUserRoleTest()
        {
            var context = CreateDbContext("GetUserRoleTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
            };
            repository.CreateUser(admin);
            Guid token = repository.Login(admin.Email, admin.Password);

            var result = repository.RoleFromSessionToken(token);

            Assert.AreEqual(RoleType.ADMIN, result);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var context = CreateDbContext("DeleteUserTest");
            var repository = new UserRepository(context);
            var manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "",
                Email = "test@test.com",
                Password = "Somepass",
                Role = RoleType.MANAGER,
            };
            repository.CreateUser(manager);

            var result = repository.DeleteUser(manager.Id, manager.Role);

            Assert.AreEqual(manager, result);
        }

        [TestMethod]
        public void DeleteUserWithInvalidIdTest()
        {
            var context = CreateDbContext("DeleteUserWithInvalidIdTest");
            var repository = new UserRepository(context);

            Exception exception = null;
            try
            {
                repository.DeleteUser(new Guid(), RoleType.MANAGER);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void GetUserRoleWithInvalidSessionTokenTest()
        {
            var context = CreateDbContext("GetUserRoleWithInvalidSessionTokenTest");
            var repository = new UserRepository(context);
            Exception exception = null;

            try
            {
                repository.RoleFromSessionToken(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void DeleteUserWithIncorrectRoleTest()
        {
            var context = CreateDbContext("DeleteUserWithIncorrectRoleTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "test@test.com",
                Password = "Somepass",
                Role = RoleType.ADMIN,
            };
            repository.CreateUser(admin);

            Exception exception = null;
            try
            {
                repository.DeleteUser(admin.Id, RoleType.MANAGER);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void LoginSuccessfullyTest()
        {
            var context = CreateDbContext("LoginSuccessfullyTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@exmaple.com",
                Password = "123456",
            };
            repository.CreateUser(admin);

            Guid token = repository.Login(admin.Email, admin.Password);

            var result = context.Set<User>().Find(admin.Id);

            Assert.AreEqual(token, result.SessionToken);
        }

        [TestMethod]
        public void LoginWithInvalidEmailAndPasswordTest()
        {
            var context = CreateDbContext("LoginWithInvalidEmailAndPasswordTest");
            var repository = new UserRepository(context);

            Exception exception = null;
            try
            {
                repository.Login("test@test.com", "some password");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void LogoutSuccessfullyTest()
        {
            var context = CreateDbContext("LogoutSuccessfullyTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@exmaple.com",
                Password = "123456",
            };
            repository.CreateUser(admin);

            Guid loginToken = repository.Login(admin.Email, admin.Password);
            Guid logoutToken = repository.Logout(loginToken);

            var result = context.Set<User>().Find(admin.Id).SessionToken;

            Assert.AreEqual(loginToken, logoutToken);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LogoutWithInvalidSessionTokenTest()
        {
            var context = CreateDbContext("LogoutWithInvalidSessionTokenTest");
            var repository = new UserRepository(context);

            Exception exception = null;
            try
            {
                repository.Logout(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void GetUserIdFromSessionTokenTest()
        {
            var context = CreateDbContext("GetUserIdFromSessionTokenTest");
            var repository = new UserRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
            };
            repository.CreateUser(admin);
            Guid token = repository.Login(admin.Email, admin.Password);

            var result = repository.GetUserIdFromSessionToken(token);

            Assert.AreEqual(admin.Id, result);
        }

        [TestMethod]
        public void GetUserIdWithInvalidSessionTokenTest()
        {
            var context = CreateDbContext("GetUserIdWithInvalidSessionTokenTest");
            var repository = new UserRepository(context);
            Exception exception = null;

            try
            {
                repository.GetUserIdFromSessionToken(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void GetManagerIdFromEmailSuccessfullyTest()
        {
            var context = CreateDbContext("GetManagerIdFromEmailSuccessfullyTest");
            var repository = new UserRepository(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
                Role = RoleType.MANAGER
            };
            repository.CreateUser(user);

            var result = repository.GetManagerIdFromEmail(user.Email);

            Assert.AreEqual(user.Id, result);
        }

        [TestMethod]
        public void GetManagerIdFromEmaiNotFoundTest()
        {
            var context = CreateDbContext("GetManagerIdFromEmaiNotFoundTest");
            var repository = new UserRepository(context);

            Exception exception = null;
            try
            {
                repository.GetManagerIdFromEmail("nonexistent@example.com");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
            Assert.AreEqual("Manager with email nonexistent@example.com not found.", exception.Message);
        }

        [TestMethod]
        public void GetUserFromEmailIsNotManagerTest()
        {
            var context = CreateDbContext("GetUserFromEmailIsNotManagerTest");
            var repository = new UserRepository(context);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456",
                Role = RoleType.MAINTENANCE
            };
            repository.CreateUser(user);

            Exception exception = null;
            try
            {
                repository.GetManagerIdFromEmail(user.Email);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
            Assert.AreEqual("User with email abc@example.com is not a manager.", exception.Message);
        }

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}