using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerModels.Inner;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;

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
                Deadline = 1745039332,
                Role = RoleType.MANAGER
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
        public void CreateInvitationSuccessfullyWithCCAdminRoleTypeTest()
        {
            Invitation invitation = new Invitation()
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = 1745039332,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };

            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);

            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            var result = invitationLogic.CreateInvitation(_invitation);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void CreateInvitationWithAdminRoleTypeTest()
        {
            Invitation invitation = new Invitation()
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = 1745039332,
                Role = RoleType.ADMIN
            };

            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);

            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            Assert.ThrowsException<ArgumentException>(() => invitationLogic.CreateInvitation(invitation));
        }

        [TestMethod]
        public void CreateInvitationWithMaintenanceRoleTypeTest()
        {
            Invitation invitation = new Invitation()
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = 1745039332,
                Role = RoleType.MAINTENANCE
            };

            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

            usersRepositoryMock.Setup(x => x.EmailExists(It.IsAny<string>())).Returns(false);
            invitationRepositoryMock.Setup(x => x.CreateInvitation(It.IsAny<Invitation>())).Returns(invitation);

            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            Assert.ThrowsException<ArgumentException>(() => invitationLogic.CreateInvitation(invitation));
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

        [TestMethod]
        public void DeleteAcceptedInvitation()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.DeleteInvitation(It.IsAny<Guid>())).Throws(new InvalidOperationException(""));
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
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ModifyInvitationSuccessfully()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            long newDeadline = 17450393329;
            Invitation modifiedInvitation = new()
            {
                Id = _invitation.Id,
                Name = _invitation.Name,
                Email = _invitation.Email,
                Deadline = newDeadline
            };
            invitationRepositoryMock.Setup(x => x.ModifyInvitation(It.IsAny<Guid>(), It.IsAny<long>())).Returns(modifiedInvitation);
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            var result = invitationLogic.ModifyInvitation(_invitation.Id, newDeadline);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(modifiedInvitation, result);
        }

        [TestMethod]
        public void ModifyInvitationWithInvalidId()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.ModifyInvitation(It.IsAny<Guid>(), It.IsAny<long>())).Throws(new ValueNotFoundException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;

            try
            {
                invitationLogic.ModifyInvitation(_invitation.Id, 17450393324);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void ModifyAcceptedInvitation()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.ModifyInvitation(It.IsAny<Guid>(), It.IsAny<long>())).Throws(new InvalidOperationException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;

            try
            {
                invitationLogic.ModifyInvitation(_invitation.Id, 17450393324);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ModifyInvitationThatExpiresInMoreThanOneDay()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.ModifyInvitation(It.IsAny<Guid>(), It.IsAny<long>())).Throws(new InvalidOperationException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;

            try
            {
                invitationLogic.ModifyInvitation(_invitation.Id, 17450393324);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ModifyInvitationWithNewDeadlineSmallerThanPrevious()
        {
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.ModifyInvitation(It.IsAny<Guid>(), It.IsAny<long>())).Throws(new InvalidOperationException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;

            try
            {
                invitationLogic.ModifyInvitation(_invitation.Id, 17450393324);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void RespondInvitationSuccessfully()
        {
            var answer = new InvitationAnswer()
            {
                InvitationId = _invitation.Id,
                Status = InvitationStatus.ACCEPTED,
                Email = "john@abc.com",
                Password = "123456"
            };

            var user = new User { Id = new Guid(), Name = "John", Email = "john@abc.com", Role = RoleType.MANAGER, Password = "123456" };

            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            usersRepositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(user);
            invitationRepositoryMock.Setup(x => x.RespondInvitation(It.IsAny<InvitationAnswer>())).Returns(_invitation);
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            var result = invitationLogic.RespondInvitation(answer);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(answer, result);
        }

        [TestMethod]
        public void RespondInvitationWithDeclinedStatus()
        {

            var answer = new InvitationAnswer()
            {
                InvitationId = _invitation.Id,
                Status = InvitationStatus.DECLINED,
                Email = "john@abc.com"
            };

            var invitation = new Invitation()
            {
                Id = new Guid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = 1745039332,
                Status = InvitationStatus.DECLINED
            };
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            usersRepositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(new User());
            invitationRepositoryMock.Setup(x => x.RespondInvitation(It.IsAny<InvitationAnswer>())).Returns(invitation);
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);

            var result = invitationLogic.RespondInvitation(answer);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(answer, result);
        }

        [TestMethod]
        public void RespondToAnInvitationWithNonExistingValue()
        {
            var answer = new InvitationAnswer()
            {
                InvitationId = new Guid(),
                Status = InvitationStatus.DECLINED,
            };
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            var usersRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.RespondInvitation(It.IsAny<InvitationAnswer>())).Throws(new ValueNotFoundException(""));
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, usersRepositoryMock.Object);
            Exception exception = null;
            try
            {
                invitationLogic.RespondInvitation(answer);
            }
            catch(Exception e)
            {
                exception = e;
            }

            invitationRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void InvitationByEmail_ReturnsInvitation_Success()
        {
            var email = "john@abc.com";
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.GetAllInvitations(email, null)).Returns(new List<Invitation> { _invitation});
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, null);

            var result = invitationLogic.GetAllInvitations(email, null);
            var expected = new List<Invitation> { _invitation };

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(expected[0], result[0]);
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
            var invitations = new List<Invitation> { invitation1, invitation2 };
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.GetAllInvitations(null, null)).Returns(invitations);
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, null);

            var result = invitationLogic.GetAllInvitations(null, null);

            invitationRepositoryMock.VerifyAll();
            Assert.AreEqual(invitations, result);

        }

        [TestMethod]
        public void GetAllInvitations_WithExpiredOrNearFilter_ReturnsCorrectlyFilteredInvitations()
        {
            var expiredOrNear = true;
            var unixTimestampNow = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            var unixTimestamp24HoursAhead = unixTimestampNow + 86400;
            var invitation1 = new Invitation
            {
                Id = Guid.NewGuid(),
                Email = "john@abc.com",
                Name = "John",
                Deadline = unixTimestampNow - 1000,
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            var invitation2 = new Invitation
            {
                Id = Guid.NewGuid(),
                Email = "john@abc2.com",
                Name = "John2",
                Deadline = unixTimestampNow + 1000,
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            var invitation3 = new Invitation
            {
                Id = Guid.NewGuid(),
                Email = "jane@abc.com",
                Name = "Jane",
                Deadline = unixTimestamp24HoursAhead + 1000,
                Status = InvitationStatus.ACCEPTED,
                Role = RoleType.MANAGER
            };
            var allInvitations = new List<Invitation> { invitation1, invitation2, invitation3 };
            var expectedFilteredInvitations = new List<Invitation> { invitation1, invitation2 };
            var invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
            invitationRepositoryMock.Setup(x => x.GetAllInvitations(null, expiredOrNear)).Returns(expectedFilteredInvitations);
            var invitationLogic = new InvitationLogic(invitationRepositoryMock.Object, null);

            var result = invitationLogic.GetAllInvitations(null, expiredOrNear);

            invitationRepositoryMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFilteredInvitations.Count, result.Count);
            Assert.IsTrue(result.All(i => i.Deadline <= unixTimestampNow || (i.Deadline > unixTimestampNow && i.Deadline <= unixTimestamp24HoursAhead)));
        }


    }
}