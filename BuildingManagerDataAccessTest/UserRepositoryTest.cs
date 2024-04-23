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

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}