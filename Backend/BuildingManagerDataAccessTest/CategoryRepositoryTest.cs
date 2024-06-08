using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void CreateCategoryTest()
        {
            var context = CreateDbContext("CreateCategoryTest");
            var repository = new CategoryRepository(context);
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category 1"
            };

            repository.CreateCategory(category);
            var result = context.Set<Category>().Find(category.Id);

            Assert.AreEqual(category, result);
        }

        [TestMethod]
        public void CreateCategoryWithDuplicatedNameTest()
        {
            var context = CreateDbContext("CreateCategoryWithDuplicatedNameTest");
            var repository = new CategoryRepository(context);
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category 1"
            };
            repository.CreateCategory(category);

            Exception exception = null;
            try
            {
                repository.CreateCategory(category);
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
