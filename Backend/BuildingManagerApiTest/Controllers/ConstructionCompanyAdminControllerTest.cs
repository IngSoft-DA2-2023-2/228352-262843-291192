
using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class ConstructionCompanyAdminControllerTest
    {
        private ConstructionCompanyAdmin _constructionCompanyAdmin;
        private CreateConstructionCompanyAdminRequest _createConstructionCompanyAdminRequest;
        private CreateConstructionCompanyAdminResponse _createConstructionCompanyAdminResponse;

        [TestInitialize]
        public void Initialize()
        {
            _constructionCompanyAdmin = new ConstructionCompanyAdmin
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Password = "pass123"
            };
            _createConstructionCompanyAdminRequest = new CreateConstructionCompanyAdminRequest
            {
                Name = "John",
                Email = "john@abc.com",
                Password = "pass123"
            };
            _createConstructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse(_constructionCompanyAdmin);

        }
        [TestMethod]
        public void CreateConstructionCompanyAdmin_Ok()
        {
            var mockConstructionCompanyAdminLogic = new Mock<IConstructionCompanyAdminLogic>(MockBehavior.Strict);
            mockConstructionCompanyAdminLogic.Setup(x => x.CreateConstructionCompanyAdmin(It.IsAny<User>(), It.IsAny<Guid>())).Returns(_constructionCompanyAdmin);
            var constructionCompanyAdminController = new ConstructionCompanyAdminController(mockConstructionCompanyAdminLogic.Object);

            var result = constructionCompanyAdminController.CreateConstructionCompanyAdmin(_createConstructionCompanyAdminRequest, Guid.NewGuid());
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateConstructionCompanyAdminResponse;

            mockConstructionCompanyAdminLogic.VerifyAll();
            Assert.AreEqual(_createConstructionCompanyAdminResponse, content);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new CreateConstructionCompanyAdminResponse(_constructionCompanyAdmin);
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new CreateConstructionCompanyAdminResponse(_constructionCompanyAdmin);
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new CreateConstructionCompanyAdminResponse(_constructionCompanyAdmin);
            var response2 = new CreateConstructionCompanyAdminResponse(new ConstructionCompanyAdmin
            {
                Id = new Guid(),
                Name = "Jane",
                Email = "jane@abc.com",
                Password = "pass456"
            });
            Assert.IsFalse(response1.Equals(response2));
        }
    }
}
