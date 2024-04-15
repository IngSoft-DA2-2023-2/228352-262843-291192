using BuildingManagerDataAccess;
using BuildingManagerDataAccess.Context;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class AdminRepositoryTest
    {

        [TestMethod]
        public void CreateAdminTest()
        {
            var context = CreateDbContext("CreateAdminTest");
            var repository = new AdminRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@exmaple.com",
                Password = "123456"
            };

            repository.CreateAdmin(admin);
            var result = context.Set<Admin>().Find(admin.Id);

            Assert.AreEqual(admin, result);

        }

        [TestMethod]
        public void CreateAdminWithDuplicatedEmailTest()
        {
            var context = CreateDbContext("CreateAdminWithDuplicatedEmailTest");
            var repository = new AdminRepository(context);
            var admin = new Admin
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@example.com",
                Password = "123456"
            };
            repository.CreateAdmin(admin);

            Exception exception = null;
            try
            {
                repository.CreateAdmin(admin);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueDuplicatedException));
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