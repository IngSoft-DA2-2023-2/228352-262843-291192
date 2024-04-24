using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;

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
            invitationRepositoryMock.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(_invitation);
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object);

            var result = invitationLogic.CreateInvitation(_invitation);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(_invitation, result);

        }
    }
}