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
            var sessionToken = Guid.NewGuid();

            repository.CreateConstructionCompany(constructionCompany, sessionToken);
            var companyResult = context.Set<ConstructionCompany>().Find(constructionCompany.Id);
            var companyAdminAssociationResult = context.Set<CompanyAdminAssociation>().Find(sessionToken, constructionCompany.Id);

            Assert.AreEqual(constructionCompany, companyResult);
            Assert.AreEqual(constructionCompany.Id, companyAdminAssociationResult.ConstructionCompanyId);
            Assert.AreEqual(sessionToken, companyAdminAssociationResult.ConstructionCompanyAdminId);
        }

        [TestMethod]
        public void CreateConstructionCompanyWithDuplicatedNameTest()
        {
            var context = CreateDbContext("CreateConstructionCompanyWithDuplicatedNameTest");
            var repository = new ConstructionCompanyRepository(context);
            var sessionToken = Guid.NewGuid();
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 1"
            };
            repository.CreateConstructionCompany(constructionCompany, sessionToken);

            Exception exception = null;
            try
            {
                repository.CreateConstructionCompany(constructionCompany, sessionToken);
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
