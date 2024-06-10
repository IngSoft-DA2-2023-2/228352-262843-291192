using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class OwnerRepositoryTest
    {
        [TestMethod]
        public void CreateOwnerTest()
        {
            var context = CreateDbContext("CreateOwnerTest");
            var repository = new OwnerRepository(context);
            var owner = new Owner
            {
                Name = "John",
                LastName = "Doe",
                Email = "jhon@gmil.com"
            };

            repository.CreateOwner(owner);
            var result = context.Set<Owner>().Find(owner.Email);

            Assert.AreEqual(owner, result);
        }

        [TestMethod]
        public void CreateOwnerWithAlreadyEmailInUseTest()
        {
            var context = CreateDbContext("CreateOwnerWithAlreadyEmailInUseTest");
            var repository = new OwnerRepository(context);
            var owner = new Owner
            {
                Name = "John",
                LastName = "Doe",
                Email = "jhon@gmail.com"
            };
            var owner2 = new Owner
            {
                Name = "Jane",
                LastName = "Doe",
                Email = "jhon@gmail.com"
            };
            repository.CreateOwner(owner);

            Exception exception = null;
            try
            {
                repository.CreateOwner(owner2);
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
