using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class CategoryControllerTest
    {
        [TestMethod]
        public void CreateCategory_Ok()
        {
            var category = new Category { 
                Id = Guid.NewGuid(), 
                Name  = "Test"
            };
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.CreateCategory(It.IsAny<string>())).Returns(category);
            var controller = new CategoryController(mockCategoryLogic.Object);

            var result = controller.CreateCategory("Test");
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateCategoryResponse;

            mockCategoryLogic.VerifyAll();
            Assert.AreEqual(new CreateCategoryResponse(category), content);
        }
    }
}
