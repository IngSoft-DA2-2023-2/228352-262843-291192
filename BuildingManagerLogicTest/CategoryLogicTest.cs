using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerLogicTest
{
    [TestClass]
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
    }
}
