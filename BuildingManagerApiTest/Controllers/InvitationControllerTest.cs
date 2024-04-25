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
        public void DeleteInvitation_Ok()
        {
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.DeleteInvitation(It.IsAny<Guid>())).Returns(_invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);
            OkObjectResult expected = new OkObjectResult(new CreateInvitationResponse(_invitation));
            CreateInvitationResponse expectedObject = expected.Value as CreateInvitationResponse;

            OkObjectResult result = invitationController.DeleteInvitation(_invitation.Id) as OkObjectResult;
            CreateInvitationResponse resultObject = result.Value as CreateInvitationResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            CreateInvitationResponse response = new CreateInvitationResponse(new Invitation());

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            CreateInvitationResponse response = new CreateInvitationResponse(new Invitation());
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            CreateInvitationResponse response = new CreateInvitationResponse(new Invitation());

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            Invitation invitation1 = new Invitation
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Email = "test@test.com",
                Name = "Invitation",
                Deadline = 1745039332
            };

            Invitation invitation2 = new Invitation
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Email = "abc@abc.com",
                Name = "Invitation",
                Deadline = 1745039332
            };

            CreateInvitationResponse response1 = new CreateInvitationResponse(invitation1);
            CreateInvitationResponse response2 = new CreateInvitationResponse(invitation2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }


    }
}