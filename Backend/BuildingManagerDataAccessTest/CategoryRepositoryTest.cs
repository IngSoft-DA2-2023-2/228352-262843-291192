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

        [TestMethod]
        public void ListCategoriesTest()
        {
            var context = CreateDbContext("ListCategoriesTest");
            var repository = new CategoryRepository(context);
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category 1"
            };
            repository.CreateCategory(category);
            List<Category> expected = [category];

            var result = repository.ListCategories();

            Assert.AreEqual(expected.First(), result.First());
        }

        [TestMethod]
        public void AssignParentTest()
        {
            var context = CreateDbContext("AssignParentTest");
            var repository = new CategoryRepository(context);
            var category1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category 1"
            };
            var category2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category 2"
            };
            var categoryWithParent = new Category
            {
                Id = category1.Id,
                Name = "Category 1",
                ParentId = category2.Id
            };
            repository.CreateCategory(category1);
            repository.CreateCategory(category2);
            repository.AssignParent(category1.Id, category2.Id);
            var result = context.Set<Category>().Find(category1.Id);

            Assert.AreEqual(categoryWithParent.Id, result.Id);
            Assert.AreEqual(categoryWithParent.Name, result.Name);
            Assert.AreEqual(categoryWithParent.ParentId, result.ParentId);
        }

        [TestMethod]
        public void AssignParentWithInvalidChildIdTest()
        {
            var context = CreateDbContext("AssignParentWithInvalidChildIdTest");
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
                repository.AssignParent(Guid.NewGuid(), category.Id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
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
