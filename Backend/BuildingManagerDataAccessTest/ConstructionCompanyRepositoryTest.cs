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
            var userId = Guid.NewGuid();
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 1"
            };
            repository.CreateConstructionCompany(constructionCompany, userId);

            Exception exception = null;
            try
            {
                repository.CreateConstructionCompany(constructionCompany, userId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueDuplicatedException));
        }

        [TestMethod]
        public void CreateConstructionCompanyWithDuplicatedUserTest()
        {
            var context = CreateDbContext("CreateConstructionCompanyWithDuplicatedUserTest");
            var repository = new ConstructionCompanyRepository(context);
            var userId = Guid.NewGuid();
            var constructionCompany1 = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 1"
            };
            repository.CreateConstructionCompany(constructionCompany1, userId);
            var constructionCompany2 = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 2"
            };

            Exception exception = null;
            try
            {
                repository.CreateConstructionCompany(constructionCompany2, userId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueDuplicatedException));
        }

        [TestMethod]
        public void ModifyConstructionCompanyNameTest()
        {
            var context = CreateDbContext("ModifyConstructionCompanyNameTest");
            var repository = new ConstructionCompanyRepository(context);
            var userId = Guid.NewGuid();
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "company 1"
            };
            constructionCompany.Name = "company 2";
            repository.CreateConstructionCompany(constructionCompany, userId);

            var result = repository.ModifyConstructionCompanyName(constructionCompany.Id, "company 2", userId);

            Assert.AreEqual(constructionCompany, result);
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
