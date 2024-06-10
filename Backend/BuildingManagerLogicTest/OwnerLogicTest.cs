using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class OwnerLogicTest
    {
        [TestMethod]
        public void CreateOwnerTest()
        {
            Owner owner = new Owner
            {
                Name = "John",
                LastName = "Doe",
                Email = "jhon@gmail.com"
            };
            var ownerRepositoryMock = new Mock<IOwnerRepository>(MockBehavior.Strict);
            ownerRepositoryMock.Setup(x => x.CreateOwner(It.IsAny<Owner>())).Returns(owner);
            var ownerLogic = new OwnerLogic(ownerRepositoryMock.Object);

            var result = ownerLogic.CreateOwner(owner);

            ownerRepositoryMock.VerifyAll();
            Assert.AreEqual(owner, result);
        }

        [TestMethod]
        public void CreateOwnerWithAlreadyEmailInUseTest()
        {
            Owner owner = new Owner
            {
                Name = "John",
                LastName = "Doe",
                Email = "jhon@gmail.com"
            };
            var ownerRepositoryMock = new Mock<IOwnerRepository>(MockBehavior.Strict);
            ownerRepositoryMock.Setup(x => x.CreateOwner(It.IsAny<Owner>())).Throws(new ValueDuplicatedException("email"));
            var ownerLogic = new OwnerLogic(ownerRepositoryMock.Object);

            Assert.ThrowsException<ValueDuplicatedException>(() => ownerLogic.CreateOwner(owner));
            ownerRepositoryMock.VerifyAll();
        }
    }
}
