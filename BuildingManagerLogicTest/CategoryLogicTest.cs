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
    [ExcludeFromCodeCoverage]
    public class CategoryLogicTest
    {
        [TestMethod]
        public void CreateCategorySuccessfully()
        {
            var category = new Category {
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
    }
}
