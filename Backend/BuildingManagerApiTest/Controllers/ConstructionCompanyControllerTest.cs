using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
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
    public class ConstructionCompanyControllerTest
    {
        [TestMethod]
        public void CreateConstructionCompany_Ok()
        {
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            var mockConstructionCompanyLogic = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            mockConstructionCompanyLogic.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>(), It.IsAny<Guid>())).Returns(constructionCompany);
            var controller = new ConstructionCompanyController(mockConstructionCompanyLogic.Object);

            var result = controller.CreateConstructionCompany(new ConstructionCompanyRequest { Name = "Test" }, new Guid());
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as ConstructionCompanyResponse;

            mockConstructionCompanyLogic.VerifyAll();
            Assert.AreEqual(new ConstructionCompanyResponse(constructionCompany), content);
        }

        [TestMethod]
        public void ModifyConstructionCompanyName_Ok()
        {
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Test1"
            };
            ConstructionCompanyRequest modifyNameRequest = new ConstructionCompanyRequest() { Name = "Test2" };
            var mockConstructionCompanyLogic = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            mockConstructionCompanyLogic.Setup(x => x.ModifyName(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>())).Returns(constructionCompany);
            var controller = new ConstructionCompanyController(mockConstructionCompanyLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ConstructionCompanyResponse(constructionCompany));
            ConstructionCompanyResponse expectedObject = expected.Value as ConstructionCompanyResponse;

            OkObjectResult result = controller.ModifyConstructionCompanyName(constructionCompany.Id, modifyNameRequest, Guid.NewGuid()) as OkObjectResult;
            ConstructionCompanyResponse resultObject = result.Value as ConstructionCompanyResponse;

            mockConstructionCompanyLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        [TestMethod]
        public void ModifyConstructionCompanyNameWithInvalidName()
        {
            var constructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "test"
            };
            ConstructionCompanyRequest modifyNameRequest = new ConstructionCompanyRequest() { Name = null };
            var mockConstructionCompanyLogic = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            var controller = new ConstructionCompanyController(mockConstructionCompanyLogic.Object);
            Exception exception = null;

            try
            {
                controller.ModifyConstructionCompanyName(constructionCompany.Id, modifyNameRequest, Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            mockConstructionCompanyLogic.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new ConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test" });
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new ConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test" });
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new ConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test" });
            var response2 = new ConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test2" });
            Assert.IsFalse(response1.Equals(response2));
        }

        [TestMethod]
        public void Validate_EmptyName_ReturnsInvalidArgumentException()
        {
            var request = new ConstructionCompanyRequest { Name = "" };
            Assert.ThrowsException<InvalidArgumentException>(() => request.Validate());
        }
    }
}
