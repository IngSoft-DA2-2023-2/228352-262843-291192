using BuildingManagerDataAccess;
using BuildingManagerDataAccess.Context;
using BuildingManagerDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

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

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}