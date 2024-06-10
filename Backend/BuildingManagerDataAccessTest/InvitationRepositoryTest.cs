using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        [TestMethod]
        public void CreateInvitationTest()
        {
            var context = CreateDbContext("CreateInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };

            repository.CreateInvitation(invitation);
            var result = context.Set<Invitation>().Find(invitation.Id);

            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void CreateInvitationThatAlreadyExistsWithDeclinedStatusTest()
        {
            var context = CreateDbContext("CreateInvitationThatAlreadyExistsWithDeclinedStatusTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.DECLINED,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            long newDeadline = DateTimeOffset.UtcNow.AddYears(4).ToUnixTimeSeconds();
            var invitation2 = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "José",
                Email = "test@test.com",
                Deadline = newDeadline,
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };
            repository.CreateInvitation(invitation2);

            var result = context.Set<Invitation>().Find(invitation.Id);
            Assert.AreEqual(invitation, result);
            Assert.AreEqual(newDeadline, result.Deadline);
            Assert.AreEqual(invitation2.Name, result.Name);
            Assert.AreEqual(invitation2.Role, result.Role);
        }

        [TestMethod]
        public void CreateInvitatioWithCCAdminRoleTypeTest()
        {
            var context = CreateDbContext("CreateInvitatioWithCCAdminRoleTypeTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN
            };

            repository.CreateInvitation(invitation);
            var result = context.Set<Invitation>().Find(invitation.Id);

            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void CreateInvitatioWithAdminRoleTypeThrowExceptionTest()
        {
            var context = CreateDbContext("CreateInvitatioWithAdminRoleTypeThrowExceptionTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.ADMIN
            };

            Exception exception = null;
            try
            {
                repository.CreateInvitation(invitation);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void CreateInvitatioWithMaintenanceRoleTypeThrowExceptionTest()
        {
            var context = CreateDbContext("CreateInvitatioWithAdminRoleTypeThrowExceptionTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MAINTENANCE
            };

            Exception exception = null;
            try
            {
                repository.CreateInvitation(invitation);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void DeleteInvitationTest()
        {
            var context = CreateDbContext("DeleteInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            var result = repository.DeleteInvitation(invitation.Id);

            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void ModifyInvitationTest()
        {
            var context = CreateDbContext("ModifyInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            var result = repository.ModifyInvitation(invitation.Id, DateTimeOffset.UtcNow.AddYears(4).ToUnixTimeSeconds());

            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void CreateInvitationWithDuplicatedEmailTest()
        {
            var context = CreateDbContext("CreateInvitationWithDuplicatedEmailTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            Exception exception = null;
            try
            {
                repository.CreateInvitation(invitation);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueDuplicatedException));
        }

        [TestMethod]
        public void DeleteInvitationWithInvalidIdTest()
        {
            var context = CreateDbContext("DeleteInvitationWithInvalidIdTest");
            var repository = new InvitationRepository(context);

            Exception exception = null;
            try
            {
                repository.DeleteInvitation(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void ModifyInvitationWithInvalidIdTest()
        {
            var context = CreateDbContext("ModifyInvitationWithInvalidIdTest");
            var repository = new InvitationRepository(context);

            Exception exception = null;
            try
            {
                repository.ModifyInvitation(new Guid(), DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void DeleteAcceptedInvitationTest()
        {
            var context = CreateDbContext("DeleteAcceptedInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            Exception exception = null;
            try
            {
                repository.DeleteInvitation(invitation.Id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ModifyAcceptedInvitationTest()
        {
            var context = CreateDbContext("ModifyAcceptedInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            Exception exception = null;
            try
            {
                repository.ModifyInvitation(invitation.Id, DateTimeOffset.UtcNow.AddYears(4).ToUnixTimeSeconds());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ModifyInvitationThatExpiresInMoreThanOneDayTest()
        {
            var context = CreateDbContext("ModifyInvitationThatExpiresInMoreThanOneDayTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            Exception exception = null;
            try
            {
                repository.ModifyInvitation(invitation.Id, DateTimeOffset.UtcNow.AddYears(4).ToUnixTimeSeconds());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void ModifyInvitationWithInvalidNewDeadlineTest()
        {
            var context = CreateDbContext("ModifyInvitationWithInvalidNewDeadlineTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            Exception exception = null;
            try
            {
                repository.ModifyInvitation(invitation.Id, DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void RespondDenyInvitationTest()
        {
            var context = CreateDbContext("RespondInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "Peter",
                Email = "peter@abc.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);
            var invitationAnswer = new InvitationAnswer
            {
                InvitationId = invitation.Id,
                Email = invitation.Email,
                Status = InvitationStatus.DECLINED
            };
            var expectedInvitation = new Invitation
            {
                Id = invitationAnswer.InvitationId,
                Name = invitation.Name,
                Email = invitationAnswer.Email,
                Status = invitationAnswer.Status,
                Deadline = invitation.Deadline,
                Role = invitation.Role
            };

            var result = repository.RespondInvitation(invitationAnswer);

            Assert.AreEqual(expectedInvitation, result);
        }

        [TestMethod]
        public void AcceptInvitationTest()
        {
            var context = CreateDbContext("RespondInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);
            var invitationAnswer = new InvitationAnswer
            {
                InvitationId = invitation.Id,
                Email = invitation.Email,
                Status = InvitationStatus.ACCEPTED,
                Password = "123456"
            };
            var expectedInvitation = new Invitation
            {
                Id = invitationAnswer.InvitationId,
                Name = invitation.Name,
                Email = invitationAnswer.Email,
                Status = invitationAnswer.Status,
                Deadline = invitation.Deadline,
                Role = invitation.Role
            };

            var result = repository.RespondInvitation(invitationAnswer);

            Assert.AreEqual(expectedInvitation, result);
        }

        [TestMethod]
        public void RespondInvitationWithValueNotFoundTest()
        {
            var context = CreateDbContext("RespondInvitationWithValueNotFoundTest");
            var repository = new InvitationRepository(context);
            var invitationAnswer = new InvitationAnswer
            {
                InvitationId = Guid.NewGuid(),
                Email = "nonexisting@hotmail.com",
                Status = InvitationStatus.ACCEPTED,
                Password = "123456"
            };

            Exception exception = null;
            try
            {
                repository.RespondInvitation(invitationAnswer);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void RespondInvitationWithAcceptedInvitationTest()
        {
            var context = CreateDbContext("RespondInvitationWithAcceptedInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "jhon@gmail.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            var invitationAnswer = new InvitationAnswer
            {
                InvitationId = invitation.Id,
                Email = invitation.Email,
                Status = InvitationStatus.ACCEPTED,
                Password = "123456"
            };

            Exception exception = null;
            try
            {
                repository.RespondInvitation(invitationAnswer);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void RespondInvitationWithRejectedInvitationTest()
        {
            var context = CreateDbContext("RespondInvitationWithRejectedInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "jhon@gmail.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds(),
                Status = InvitationStatus.DECLINED,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            var invitationAnswer = new InvitationAnswer
            {
                InvitationId = invitation.Id,
                Email = invitation.Email,
                Status = InvitationStatus.ACCEPTED,
                Password = "123456"
            };

            Exception exception = null;
            try
            {
                repository.RespondInvitation(invitationAnswer);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void RespondInvitationWithExpiredInvitationTest()
        {
            var context = CreateDbContext("RespondInvitationWithExpiredInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = DateTimeOffset.UtcNow.AddHours(-1).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            repository.CreateInvitation(invitation);

            var invitationAnswer = new InvitationAnswer
            {
                InvitationId = invitation.Id,
                Email = invitation.Email,
                Status = InvitationStatus.ACCEPTED,
                Password = "123456"
            };

            Exception exception = null;
            try
            {
                repository.RespondInvitation(invitationAnswer);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void GetInvitationByEmail_ReturnsInvitation_Success()
        {
            var context = CreateDbContext("GetInvitationByEmail_ReturnsInvitation_Success");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            context.Set<Invitation>().Add(invitation);
            context.SaveChanges();

            var result = repository.GetAllInvitations("john@abc.com", null, null);

            Assert.AreEqual(invitation.Email, result[0].Email);
        }

        [TestMethod]
        public void GetAllInvitations_WithNullEmail_ReturnsAllInvitations()
        {
            var context = CreateDbContext("GetAllInvitations_WithNullEmail_ReturnsAllInvitations");
            var repository = new InvitationRepository(context);


            var invitations = repository.GetAllInvitations(null, null, null);

            Assert.AreEqual(context.Set<Invitation>().Count(), invitations.Count);
        }

        [TestMethod]
        public void GetAllInvitations_WithValidEmail_ReturnsMatchingInvitations()
        {
            var context = CreateDbContext("GetAllInvitations_WithValidEmail_ReturnsMatchingInvitations");
            var repository = new InvitationRepository(context);


            string testEmail = "test@test.com";
            var expectedInvitations = context.Set<Invitation>().Where(i => i.Email == testEmail).ToList();
            var invitations = repository.GetAllInvitations(testEmail, null, null);

            Assert.AreEqual(expectedInvitations.Count, invitations.Count);
            CollectionAssert.AreEquivalent(expectedInvitations, invitations);
        }

        [TestMethod]
        public void GetAllInvitations_WithNonexistentEmail_ReturnsEmptyList()
        {
            var context = CreateDbContext("GetAllInvitations_WithNonexistentEmail_ReturnsEmptyList");
            var repository = new InvitationRepository(context);


            string nonexistentEmail = "no-reply@unknown.com";
            var invitations = repository.GetAllInvitations(nonexistentEmail, null, null);

            Assert.AreEqual(0, invitations.Count);
        }

        [TestMethod]
        public void GetAllInvitations_WithExpiredOrNearFilter_ReturnsCorrectlyFilteredInvitations()
        {
            var context = CreateDbContext("GetAllInvitations_WithExpiredOrNearFilter_ReturnsCorrectlyFilteredInvitations");
            var repository = new InvitationRepository(context);
            var unixTimestampNow = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            var unixTimestamp24HoursAhead = unixTimestampNow + 86400;

            var invitation1 = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "john@abc.com",
                Deadline = unixTimestampNow - 100,
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            var invitation2 = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "Jane",
                Email = "jane@abc.com",
                Deadline = unixTimestamp24HoursAhead - 100,
                Status = InvitationStatus.PENDING,
                Role = RoleType.MANAGER
            };
            var invitation3 = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "Doe",
                Email = "doe@abc.com",
                Deadline = unixTimestamp24HoursAhead + 10000,
                Status = InvitationStatus.ACCEPTED,
                Role = RoleType.MANAGER
            };

            context.Set<Invitation>().AddRange(new[] { invitation1, invitation2, invitation3 });
            context.SaveChanges();

            var result = repository.GetAllInvitations(null, true, null);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(i => i.Email == "john@abc.com")); 
            Assert.IsTrue(result.Any(i => i.Email == "jane@abc.com"));
            Assert.IsFalse(result.Any(i => i.Email == "doe@abc.com"));
        }


        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}