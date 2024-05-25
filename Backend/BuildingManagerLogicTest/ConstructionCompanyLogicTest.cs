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
    public class ConstructionCompanyLogicTest
    {
        [TestMethod]
        public void CreateConstructionCompanySuccessfully()
        {
            var constructionCompany = new ConstructionCompany {
                Id = new Guid(),
                Name = "company"
            };
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>())).Returns(constructionCompany);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object);

            var result = constructionCompanyLogic.CreateConstructionCompany(constructionCompany);

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual(constructionCompany, result);
        }
    }
}
