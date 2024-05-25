using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConstructionCompanyRepositoryTest
    {
        [TestMethod]
        public void CreateConstructionCompanyTest()
        {
            var context = CreateDbContext("CreateConstructionCompanyTest");
            var repository = new ConstructionCompanyRepository(context);
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 1"
            };

            repository.CreateConstructionCompany(constructionCompany);
            var result = context.Set<ConstructionCompany>().Find(constructionCompany.Id);

            Assert.AreEqual(constructionCompany, result);
        }

        [TestMethod]
        public void CreateConstructionCompanyWithDuplicatedNameTest()
        {
            var context = CreateDbContext("CreateConstructionCompanyWithDuplicatedNameTest");
            var repository = new ConstructionCompanyRepository(context);
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 1"
            };
            repository.CreateConstructionCompany(constructionCompany);

            Exception exception = null;
            try
            {
                repository.CreateConstructionCompany(constructionCompany);
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
