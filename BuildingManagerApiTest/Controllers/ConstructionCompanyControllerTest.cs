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
    [ExcludeFromCodeCoverage]
    public class ConstructionCompanyControllerTest
    {
        [TestMethod]
        public void CreateConstructionCompany_Ok()
        {
            var constructionCompany = new ConstructionCompany { 
                Id = Guid.NewGuid(), 
                Name  = "Test"
            };
            var mockConstructionCompanyLogic = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            mockConstructionCompanyLogic.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(constructionCompany);
            var controller = new ConstructionCompanyController(mockConstructionCompanyLogic.Object);

            var result = controller.CreateConstructionCompany(new CreateConstructionCompanyRequest { Name = "Test"});
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateConstructionCompanyResponse;

            mockConstructionCompanyLogic.VerifyAll();
            Assert.AreEqual(new CreateConstructionCompanyResponse(constructionCompany), content);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            var response = new CreateConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test"});
            Assert.IsFalse(response.Equals(null));
        }

        [TestMethod]
        public void Equals_ObjectOfDifferentType_ReturnsFalse()
        {
            var response = new CreateConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test" });
            var differentTypeObject = new object();
            Assert.IsFalse(response.Equals(differentTypeObject));
        }

        [TestMethod]
        public void Equals_ObjectWithDifferentAttributes_ReturnsFalse()
        {
            var response1 = new CreateConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test" });
            var response2 = new CreateConstructionCompanyResponse(new ConstructionCompany { Id = Guid.NewGuid(), Name = "Test2" });
            Assert.IsFalse(response1.Equals(response2));
        }
    }
}
