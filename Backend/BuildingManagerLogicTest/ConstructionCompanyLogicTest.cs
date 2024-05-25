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
            var userId = Guid.NewGuid();
            var constructionCompany = new ConstructionCompany
            {
                Id = new Guid(),
                Name = "company"
            };
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>(), It.IsAny<Guid>())).Returns(constructionCompany);
            userRepositoryMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userRepositoryMock.Object);

            var result = constructionCompanyLogic.CreateConstructionCompany(constructionCompany, Guid.NewGuid());

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual(constructionCompany, result);
        }

        [TestMethod]
        public void CreateConstructionCompanyDuplicatedName()
        {
            var userId = Guid.NewGuid();
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>(), It.IsAny<Guid>())).Throws(new ValueDuplicatedException(""));
            userRepositoryMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userRepositoryMock.Object);

            Exception exception = null;
            try
            {
                constructionCompanyLogic.CreateConstructionCompany(new ConstructionCompany(), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void CreateConstructionCompanyWithInvalidSessionTokenTest()
        {
            var constructionCompany = new ConstructionCompany
            {
                Id = new Guid(),
                Name = "company"
            };
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userRepositoryMock.Object);

            Exception exception = null;
            try
            {
                constructionCompanyLogic.CreateConstructionCompany(new ConstructionCompany(), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }
    }
}
