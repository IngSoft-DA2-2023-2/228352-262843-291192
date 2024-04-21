using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class InvitationControllerTest
    {
        private Invitation _invitation;
        private CreateInvitationRequest _createInvitationRequest;
        private CreateInvitationResponse _createInvitationResponse;

        [TestInitialize]
        public void Initialize()
        {
            _invitation = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332
            };
            _createInvitationRequest = new CreateInvitationRequest
            {
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332
            };
            _createInvitationResponse = new CreateInvitationResponse(_invitation);

        }
        [TestMethod]
        public void CreateInvitation_Ok()
        {
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(_invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.CreateInvitation(_createInvitationRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as CreateInvitationResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(_createInvitationResponse, content);
        }

        [TestMethod]
        public void CreateInvitationWithMissingField()
        {
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Throws(new ArgumentException());
            var invitationController = new InvitationController(mockInvitationLogic.Object);
            var result = invitationController.CreateInvitation(_createInvitationRequest);

            mockInvitationLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


    }
}