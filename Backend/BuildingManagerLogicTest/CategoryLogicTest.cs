using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class CategoryLogicTest
    {
        [TestMethod]
        public void CreateCategorySuccessfully()
        {
            var category = new Category
            {
                Id = new Guid(),
                Name = "Category"
            };
            var categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(x => x.CreateCategory(It.IsAny<Category>())).Returns(category);
            var categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            var result = categoryLogic.CreateCategory(category);

            categoryRepositoryMock.VerifyAll();
            Assert.AreEqual(category, result);
        }

        [TestMethod]
        public void CreateCategoryDuplicatedName()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(x => x.CreateCategory(It.IsAny<Category>())).Throws(new ValueDuplicatedException(""));
            var categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            Exception exception = null;
            try
            {
                categoryLogic.CreateCategory(new Category());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            categoryRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void ListCategoriesTest()
        {
            List<Category> categories =
            [
                new Category {
                Id = new Guid(),
                Name = "Category"
            }
             ];
            var categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(x => x.ListCategories()).Returns(categories);
            var categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            var result = categoryLogic.ListCategories();

            Assert.AreEqual(categories, result);
        }

        [TestMethod]
        public void AssignParentTest()
        {
            Category category2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category2"
            };
            Category category1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category1",
                ParentId = category2.Id
            };
            var categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(x => x.AssignParent(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(category1);
            var categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            var result = categoryLogic.AssignParent(category1.Id, category2.Id);

            Assert.AreEqual(category1, result);
        }

        [TestMethod]
        public void AssingParentWithInvalidId()
        {
            var categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(x => x.AssignParent(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            Exception exception = null;
            try
            {
                categoryLogic.AssignParent(Guid.NewGuid(), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            categoryRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }
    }
}
