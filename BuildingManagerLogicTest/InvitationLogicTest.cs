using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerIDataAccess.Exceptions;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class InvitationLogicTest
    {
        private Invitation _invitation;

        [TestInitialize]
        public void Initialize()
        {
            _invitation = new Invitation()
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = 1745039332
            };
        }

        [TestMethod]
        public void CreateInvitationSuccessfully()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(_invitation);

            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            var result = invitationLogic.CreateInvitation(_invitation);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(_invitation, result);
        }

        [TestMethod]
        public void CreateInvitationWithAlreadyInUseEmail()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Throws(new ValueDuplicatedException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;
            try
            {
                invitationLogic.CreateInvitation(_invitation);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void CreateInvitationWithUserAlreadyUsingEmail()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(true);

            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;
            try
            {
                invitationLogic.CreateInvitation(_invitation);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(DuplicatedValueException));
        }

        [TestMethod]
        public void DeleteInvitationSuccessfully()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.DeleteInvitation(It.IsAny<Guid>())).Returns(_invitation);

            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            var result = invitationLogic.DeleteInvitation(_invitation.Id);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(_invitation, result);
        }

        [TestMethod]
        public void DeleteInvitationWithInvalidId()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.DeleteInvitation(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;
            try
            {
                invitationLogic.DeleteInvitation(_invitation.Id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }
    }
}