using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.CustomExceptions;
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
                Role = RoleType.MANAGER
            };
            _createInvitationRequest = new CreateInvitationRequest
            {
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Role = RoleType.MANAGER
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
        public void CreateContstructionCompanyAdminInvitationOk()
        {
            Invitation invitation = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            CreateInvitationRequest createInvitationRequest = new CreateInvitationRequest
            {
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            InvitationResponse createInvitationResponse = new InvitationResponse(invitation);

            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.CreateInvitation(createInvitationRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as InvitationResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(createInvitationResponse, content);
            Assert.AreEqual(content.Status, InvitationStatus.PENDING);
        }

        [TestMethod]
        public void CreateInvitationWithAdminRoleType_ThrowsInvalidArgumentException()
        {
            Invitation invitation = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Status = InvitationStatus.PENDING,
                Role = RoleType.ADMIN
            };
            CreateInvitationRequest createInvitationRequest = new CreateInvitationRequest
            {
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Role = RoleType.ADMIN
            };
            InvitationResponse createInvitationResponse = new InvitationResponse(invitation);

            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            Assert.ThrowsException<InvalidArgumentException>(() => invitationController.CreateInvitation(createInvitationRequest));
        }

        [TestMethod]
        public void CreateInvitationWithMaintenanceRoleType_ThrowsInvalidArgumentException()
        {
            Invitation invitation = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Status = InvitationStatus.PENDING,
                Role = RoleType.MAINTENANCE
            };
            CreateInvitationRequest createInvitationRequest = new CreateInvitationRequest
            {
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Role = RoleType.MAINTENANCE
            };
            InvitationResponse createInvitationResponse = new InvitationResponse(invitation);

            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            Assert.ThrowsException<InvalidArgumentException>(() => invitationController.CreateInvitation(createInvitationRequest));
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

        [TestMethod]
        public void InvitationByEmail_Ok()
        {
            var email = "john@abc.com";
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.GetAllInvitations(email,null, null)).Returns(new List<Invitation> { _invitation });
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.GetAllInvitations(email,null, null);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as ListInvitationsResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(content, new ListInvitationsResponse(new List<Invitation> { _invitation }));
        }

        [TestMethod]
        public void GetAllInvitations_Ok()
        {
            Invitation invitation1 = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = 1745039332,
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            Invitation invitation2 = new Invitation
            {
                Id = new Guid(),
                Email = "john@abc2.com",
                Name = "John2",
                Deadline = 1755039332,
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            var  invitations = new List<Invitation> { invitation1, invitation2 };
            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.GetAllInvitations(null,null, null)).Returns(invitations);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.GetAllInvitations(null,null, null);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as ListInvitationsResponse;
            
            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(new ListInvitationsResponse(invitations), content);
        }

        [TestMethod]
        public void GetAllInvitations_WithExpiredOrNearFilter_Ok()
        {
            var expiredOrNear = true;
            var invitations = new List<Invitation>
            {
                new Invitation
                {
                    Id = Guid.NewGuid(),
                            Email = "john@abc.com",
                    Name = "John",
                    Deadline = 1745039332,
                    Status = InvitationStatus.PENDING,
                    Role = RoleType.MANAGER
               },
                new Invitation
                {
                   Id = Guid.NewGuid(),
                    Email = "jane@abc.com",
                 Name = "Jane",
                  Deadline = 1745039332,
                    Status = InvitationStatus.ACCEPTED,
                   Role = RoleType.MANAGER
             }
         };

            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.GetAllInvitations(null, expiredOrNear, null)).Returns(invitations);
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.GetAllInvitations(null, expiredOrNear, null);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as ListInvitationsResponse;

            mockInvitationLogic.VerifyAll();
            Assert.IsNotNull(content);
            Assert.AreEqual(invitations.Count, content.Invitations.Count);
            Assert.IsTrue(content.Invitations.All(i => i.Status == InvitationStatus.PENDING || i.Deadline <= DateTime.UtcNow.AddMinutes(5).Ticks));
        }

        [TestMethod]
        public void GetAllInvitations_WithStatusFilter_Ok()
        {
            var status = (int)InvitationStatus.PENDING;
            var invitations = new List<Invitation>
    {
        new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "john@abc.com",
            Name = "John",
            Deadline = 1745039332,
            Status = InvitationStatus.PENDING,
            Role = RoleType.MANAGER
        },
        new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "jane@abc.com",
            Name = "Jane",
            Deadline = 1745039332,
            Status = InvitationStatus.ACCEPTED,
            Role = RoleType.MANAGER
        }
    };

            var mockInvitationLogic = new Mock<IInvitationLogic>(MockBehavior.Strict);
            mockInvitationLogic.Setup(x => x.GetAllInvitations(null, null, status)).Returns(invitations.Where(i => i.Status == (InvitationStatus)status).ToList());
            var invitationController = new InvitationController(mockInvitationLogic.Object);

            var result = invitationController.GetAllInvitations(null, null, status);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as ListInvitationsResponse;

            mockInvitationLogic.VerifyAll();
            Assert.AreEqual(new ListInvitationsResponse(invitations.Where(i => i.Status == (InvitationStatus)status).ToList()), content);
        }


    }
}