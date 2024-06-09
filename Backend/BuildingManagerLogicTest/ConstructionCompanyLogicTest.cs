using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerLogicTest
{
    [TestClass]
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
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>(), It.IsAny<Guid>())).Returns(constructionCompany);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

            var result = constructionCompanyLogic.CreateConstructionCompany(constructionCompany, Guid.NewGuid());

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual(constructionCompany, result);
        }

        [TestMethod]
        public void CreateConstructionCompanyDuplicatedName()
        {
            var userId = Guid.NewGuid();
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.CreateConstructionCompany(It.IsAny<ConstructionCompany>(), It.IsAny<Guid>())).Throws(new ValueDuplicatedException(""));
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

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
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

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

        [TestMethod]
        public void ModifyConstructionCompanyNameSuccessfully()
        {
            var constructionCompany = new ConstructionCompany
            {
                Id = new Guid(),
                Name = "company"
            };
            string newName = "company2";
            ConstructionCompany modifiedCompany = new()
            {
                Id = constructionCompany.Id,
                Name = newName
            };
            var userId = Guid.NewGuid();
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            constructionCompanyRepositoryMock.Setup(x => x.ModifyConstructionCompanyName(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>())).Returns(modifiedCompany);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

            var result = constructionCompanyLogic.ModifyName(constructionCompany.Id, newName, Guid.NewGuid());

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual(modifiedCompany, result);
        }

        [TestMethod]
        public void ModifyConstructionCompanyNameWithInvalidId()
        {
            var userId = Guid.NewGuid();
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            constructionCompanyRepositoryMock.Setup(x => x.ModifyConstructionCompanyName(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);
            Exception exception = null;

            try
            {
                constructionCompanyLogic.ModifyName(Guid.NewGuid(), "new name", Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void ModifyConstructionCompanyNameToAlreadyExistingName()
        {
            var userId = Guid.NewGuid();
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            userLogicMock.Setup(x => x.GetUserIdFromSessionToken(It.IsAny<Guid>())).Returns(userId);
            constructionCompanyRepositoryMock.Setup(x => x.ModifyConstructionCompanyName(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Guid>())).Throws(new ValueDuplicatedException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);
            Exception exception = null;

            try
            {
                constructionCompanyLogic.ModifyName(Guid.NewGuid(), "existing name", Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void GetConstructionCompanyIdFromUserIdTest()
        {
            Guid userId = Guid.NewGuid();
            Guid companyId = Guid.NewGuid();
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(companyId);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

            var result = constructionCompanyLogic.GetCompanyIdFromUserId(userId);

            Assert.AreEqual(companyId, result);
        }

        [TestMethod]
        public void GetConstructionCompanyIdWithInvalidUserId()
        {
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);
            Exception exception = null;

            try
            {
                constructionCompanyLogic.GetCompanyIdFromUserId(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void AssociateCompanyToUserSuccessfully()
        {
            var userId1 = Guid.NewGuid();
            var constructionCompany = new ConstructionCompany
            {
                Id = new Guid(),
                Name = "company"
            };
            var userId2 = Guid.NewGuid();
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.AssociateCompanyToUser(It.IsAny<Guid>(), It.IsAny<Guid>()));
            constructionCompanyRepositoryMock.Setup(x => x.GetCompanyIdFromUserId(It.IsAny<Guid>())).Returns(constructionCompany.Id);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);
            var companyIdAssociated = constructionCompanyLogic.GetCompanyIdFromUserId(userId2);
            constructionCompanyLogic.AssociateCompanyToUser(constructionCompany.Id, userId2);

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.AreEqual(constructionCompany.Id, companyIdAssociated);
        }

        [TestMethod]
        public void AssociateCompanyToUserWithInvalidValue()
        {
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.AssociateCompanyToUser(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);
            Exception exception = null;

            try
            {
                constructionCompanyLogic.AssociateCompanyToUser(Guid.NewGuid(), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void AssociateCompanyToUserWithUserAlreadyAssociatedToCompany()
        {
            var userId = Guid.NewGuid();
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.AssociateCompanyToUser(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new ValueDuplicatedException(""));
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

            Exception exception = null;
            try
            {
                constructionCompanyLogic.AssociateCompanyToUser(Guid.NewGuid(), Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            constructionCompanyRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void IsUserAssociatedToCompanyTest()
        {
            Guid userId = Guid.NewGuid();
            Guid companyId = Guid.NewGuid();
            var userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.IsUserAssociatedToCompany(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(true);
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, userLogicMock.Object);

            var result = constructionCompanyLogic.IsUserAssociatedToCompany(userId, companyId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetCompanyBuildingsTest()
        {
            Guid companyId = Guid.NewGuid();
            var constructionCompanyRepositoryMock = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
            constructionCompanyRepositoryMock.Setup(x => x.GetCompanyBuildings(It.IsAny<Guid>())).Returns(new List<BuildingResponse>());
            var constructionCompanyLogic = new ConstructionCompanyLogic(constructionCompanyRepositoryMock.Object, null);

            var result = constructionCompanyLogic.GetCompanyBuildings(companyId);

            Assert.IsNotNull(result);
        }
    }
}
