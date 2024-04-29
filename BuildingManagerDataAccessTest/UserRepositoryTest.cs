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

            var result = repository.Exists(admin.Id);

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

            var result = repository.Role(admin.Id);

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

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}