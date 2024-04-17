using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class MaintenanceRepositoryTest
    {

        [TestMethod]
        public void CreateMaintenanceStaffTest()
        {
            var context = CreateDbContext("CreateMaintenanceStaffTest");
            var repository = new MaintenanceRepository(context);
            var maintenanceStaff = new MaintenanceStaff
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "abc@exmaple.com",
                Password = "123456"
            };

            repository.CreateMaintenanceStaff(maintenanceStaff);
            var result = context.Set<MaintenanceStaff>().Find(maintenanceStaff.Id);

            Assert.AreEqual(maintenanceStaff, result);
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