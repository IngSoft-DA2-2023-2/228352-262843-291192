
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerDomain.Enums;
using System.Diagnostics.CodeAnalysis;
using BuildingManagerILogic;

namespace BuildingManagerLogicTest
{
    [TestClass]

    public class ConstructionCompanyAdminLogicTest
    {
        [TestMethod]
        public void CreateConstructionCompanyAdminSuccessfully()
        {
            var user = new ConstructionCompanyAdmin()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "abc@example.com",
                Password = "pass123"
            };
            var sessionToken = Guid.NewGuid();
            var creatorId = Guid.NewGuid();
            var companyId = Guid.NewGuid();
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyLogicMock = new Mock<IConstructionCompanyLogic>(MockBehavior.Strict);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(creatorId);
            userLogicMock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(user);
            constructionCompanyLogicMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            constructionCompanyLogicMock.Setup(x => x.AssociateCompanyToUser(It.IsAny<Guid>(), It.IsAny<Guid>()));
            var constructionCompanyAdminLogic = new ConstructionCompanyAdminLogic(constructionCompanyLogicMock.Object, userLogicMock.Object);

            var result = constructionCompanyAdminLogic.CreateConstructionCompanyAdmin(user, sessionToken);

            userLogicMock.VerifyAll();
            constructionCompanyLogicMock.VerifyAll();
            Assert.AreEqual(user, result);
            Assert.AreEqual(RoleType.CONSTRUCTIONCOMPANYADMIN, result.Role);
        }
    }
}
