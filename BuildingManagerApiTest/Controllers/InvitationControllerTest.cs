using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
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
        private InvitationResponse _createInvitationResponse;

        [TestInitialize]
        public void Initialize()
        {
            _invitation = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Status = InvitationStatus.PENDING,
            };
            _createInvitationRequest = new CreateInvitationRequest
            {
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332
            };
            _createInvitationResponse = new InvitationResponse(_invitation);

        }
        [TestMethod]
        public void CreateInvitation_Ok()
        {
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(_invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.CreateInvitation(_createInvitationRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as InvitationResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(_createInvitationResponse, content);
            Assert.AreEqual(content.Status, InvitationStatus.PENDING);
        }

        [TestMethod]
        public void DeleteInvitation_Ok()
        {
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.DeleteInvitation(It.IsAny<Guid>())).Returns(_invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);
            OkObjectResult expected = new OkObjectResult(new InvitationResponse(_invitation));
            InvitationResponse expectedObject = expected.Value as InvitationResponse;

            OkObjectResult result = invitationController.DeleteInvitation(_invitation.Id) as OkObjectResult;
            InvitationResponse resultObject = result.Value as InvitationResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        [TestMethod]
        public void ModifyInvitation_Ok()
        {
            long newDeadline = 17450393329;
            ModifyInvitationRequest modifyInvitationRequest = new ModifyInvitationRequest(newDeadline);
            Invitation modifiedInvitation = new Invitation
            {
                Id = _invitation.Id,
                Email = _invitation.Email,
                Name = _invitation.Name,
                Deadline = newDeadline,
            };
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.ModifyInvitation(It.IsAny<Guid>(), It.IsAny<long>())).Returns(modifiedInvitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);
            OkObjectResult expected = new OkObjectResult(new InvitationResponse(_invitation));
            InvitationResponse expectedObject = expected.Value as InvitationResponse;

            OkObjectResult result = invitationController.ModifyInvitation(_invitation.Id, modifyInvitationRequest) as OkObjectResult;
            InvitationResponse resultObject = result.Value as InvitationResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject.Id, resultObject.Id);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            InvitationResponse response = new InvitationResponse(new Invitation());

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            InvitationResponse response = new InvitationResponse(new Invitation());
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            InvitationResponse response = new InvitationResponse(new Invitation());

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

            InvitationResponse response1 = new InvitationResponse(invitation1);
            InvitationResponse response2 = new InvitationResponse(invitation2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RespondInvitation_Ok()
        {
            var answer = new InvitationAnswer
            {
                InvitationId = _invitation.Id,
                Email = "abc@example.com",
                Status = InvitationStatus.ACCEPTED
            };
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.RespondInvitation(It.IsAny<InvitationAnswer>())).Returns(answer);
            var invitationController = new InvitationController(mockInvitationLogic.Object);
            
            var respondInvitationResponse = new RespondInvitationResponse(answer);

            var result = invitationController.RespondInvitation(_invitation.Id, new RespondInvitationRequest
            {
                Status = InvitationStatus.ACCEPTED,
                Email = "abc@example.com",
                Password = "password"
            });
            var createdAtActionResult = result as OkObjectResult;
            var content = createdAtActionResult.Value as RespondInvitationResponse;
            Assert.AreEqual(respondInvitationResponse, content);

        }

        [TestMethod]
        public void RespondInvitation_NotEqual()
        {
            var answer1 = new InvitationAnswer
            {
                InvitationId = Guid.NewGuid(),
                Email = "abc@example.com",
                Status = InvitationStatus.ACCEPTED
            };

            var answer2 = new InvitationAnswer
            {
                InvitationId = Guid.NewGuid(),
                Email = "def@example.com",
                Status = InvitationStatus.DECLINED
            };

            var response1 = new RespondInvitationResponse(answer1);
            var response2 = new RespondInvitationResponse(answer2);

            Assert.AreNotEqual(response1, response2);
        }
    }
}