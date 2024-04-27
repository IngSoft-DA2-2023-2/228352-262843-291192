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
                Status = InvitationStatus.PENDING
            };

            repository.CreateInvitation(invitation);
            var result = context.Set<Invitation>().Find(invitation.Id);

            Assert.AreEqual(invitation, result);
        }

        [TestMethod]
        public void CheckIfInvitationWasAcceptedTest()
        {
            var context = CreateDbContext("CheckIfInvitationWasAcceptedTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED
            };
            repository.CreateInvitation(invitation);

            var result = repository.IsAccepted(invitation.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfInvitationWasAcceptedWithInvalidIdTest()
        {
            var context = CreateDbContext("CheckIfInvitationWasAcceptedWithInvalidIdTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED
            };
            Exception exception = null;

            try
            {
                var result = repository.IsAccepted(invitation.Id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
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
                Status = InvitationStatus.PENDING
            };
            repository.CreateInvitation(invitation);

            var result = repository.DeleteInvitation(invitation.Id);

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
                Status = InvitationStatus.PENDING
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
        public void DeleteAcceptedInvitationTest()
        {
            var context = CreateDbContext("DeleteAcceptedInvitationTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED
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
        public void CheckIfInvitationExpiresInMoreThanOneDayTest()
        {
            var context = CreateDbContext("CheckIfInvitationExpiresInMoreThanOneDayTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED
            };
            repository.CreateInvitation(invitation);

            var result = repository.ExpiresInMoreThanOneDay(invitation.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfNewDeadlineIsValidTest()
        {
            var context = CreateDbContext("CheckIfNewDeadlineIsValidTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED
            };
            repository.CreateInvitation(invitation);

            var result = repository.IsDeadlineExtensionValid(invitation.Id, DateTimeOffset.UtcNow.AddDays(2).ToUnixTimeSeconds());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckIfInvitationExpiresInMoreThanOneDayWithInvalidIdTest()
        {
            var context = CreateDbContext("CheckIfInvitationExpiresInMoreThanOneDayWithInvalidIdTest");
            var repository = new InvitationRepository(context);
            var invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Email = "test@test.com",
                Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds(),
                Status = InvitationStatus.ACCEPTED
            };
            Exception exception = null;

            try
            {
                var result = repository.ExpiresInMoreThanOneDay(invitation.Id);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
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