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
    public class CategoryControllerTest
    {
        [TestMethod]
        public void CreateCategory_Ok()
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Children = []
            };
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.CreateCategory(It.IsAny<Category>())).Returns(category);
            var controller = new CategoryController(mockCategoryLogic.Object);

            var result = controller.CreateCategory(new CreateCategoryRequest { Name = "Test" });
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateCategoryResponse;

            mockCategoryLogic.VerifyAll();
            Assert.AreEqual(new CreateCategoryResponse(category), content);
        }

        [TestMethod]
        public void ListCategories_OK()
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Children = []
            };
            List<Category> categories = new List<Category> { category };
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.ListCategories()).Returns(categories);
            var controller = new CategoryController(mockCategoryLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ListCategoriesResponse(categories));
            ListCategoriesResponse expectedObject = expected.Value as ListCategoriesResponse;

            OkObjectResult result = controller.GetCategories() as OkObjectResult;
            ListCategoriesResponse resultObject = result.Value as ListCategoriesResponse;

            mockCategoryLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }

        [TestMethod]
        public void AssignParent_Ok()
        {
            var category1 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                Children = []
            };
            var category2 = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                Children = []
            };
            var categoryWithParent = new Category
            {
                Id = category1.Id,
                Name = category1.Name,
                ParentId = category2.Id,
                Parent = category2,
                Children = []
            };
            var assignParentResponse = new CategoryResponse(categoryWithParent);
            var mockCategoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mockCategoryLogic.Setup(x => x.AssignParent(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(categoryWithParent);
            var controller = new CategoryController(mockCategoryLogic.Object);

            var result = controller.AssignParent(category1.Id, category2.Id);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as CategoryResponse;

            mockCategoryLogic.VerifyAll();
            Assert.AreEqual(assignParentResponse, content);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test", Children = [] });
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test", Children = [] });
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test", Children = [] });
            var response2 = new CreateCategoryResponse(new Category { Id = Guid.NewGuid(), Name = "Test2", Children = [] });
            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void Validate_EmptyName_ReturnsInvalidArgumentException()
        {
            var request = new CreateCategoryRequest { Name = "" };
            Assert.ThrowsException<InvalidArgumentException>(() => request.Validate());
        }

        [TestMethod]
        public void ListCategoriesEquals_NullObject_ReturnsFalse()
        {
            var response = new ListCategoriesResponse([new Category { Id = Guid.NewGuid(), Name = "Test", Children = [] }]);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void ListCategoriesEquals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new ListCategoriesResponse([new Category { Id = Guid.NewGuid(), Name = "Test", Children = [] }]);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void ListCategoriesEquals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new ListCategoriesResponse([new Category { Id = Guid.NewGuid(), Name = "Test", Children = [] }]);
            var response2 = new ListCategoriesResponse([new Category { Id = Guid.NewGuid(), Name = "Test2", Children = [] }]);
            Assert.IsFalse(response1.Equals(response2));
        }

    }
}
