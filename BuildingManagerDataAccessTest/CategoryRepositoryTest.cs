using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using Microsoft.EntityFrameworkCore;

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
        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}
