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
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
            mockCategoryLogic.Setup(x => x.CreateCategory(It.IsAny<Category>())).Returns(category);
            var controller = new CategoryController(mockCategoryLogic.Object);

            var result = controller.CreateCategory(new CreateCategoryRequest { Name = "Debt"});
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateCategoryResponse;

            mockCategoryLogic.VerifyAll();
            Assert.AreEqual(new CreateCategoryResponse(category), content);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test"});
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test" });
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test" });
            var response2 = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test2" });
            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void CreateCategoryWithDuplicatedName()
        {
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.CreateCategory(It.IsAny<Category>())).Throws(new DuplicatedValueException(new Exception(),""));
            var categoryController = new CategoryController(mockCategoryLogic.Object);

            var result = categoryController.CreateCategory(new CreateCategoryRequest { Name = "Debt" });

            mockCategoryLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreateCategoryWithNullAttribute()
        {
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.CreateCategory(It.IsAny<Category>())).Throws(new InvalidArgumentException("name"));
            var categoryController = new CategoryController(mockCategoryLogic.Object);

            var result = categoryController.CreateCategory(new CreateCategoryRequest{ Name = "Debt" });

            mockCategoryLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreateCategoryServerError()
        {
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.CreateCategory(It.IsAny<Category>())).Throws(new Exception());
            var categoryController = new CategoryController(mockCategoryLogic.Object);

            var result = categoryController.CreateCategory(new CreateCategoryRequest{ Name = "Debt" });

            mockCategoryLogic.VerifyAll();
            Assert.AreEqual(500, (result as StatusCodeResult).StatusCode);

        }

        [TestMethod]
        public void Validate_EmptyName_ReturnsInvalidArgumentException()
        {
            var request = new CreateCategoryRequest { Name = "" };
            Assert.ThrowsException<InvalidArgumentException>(() => request.Validate());
        }

    }
}
