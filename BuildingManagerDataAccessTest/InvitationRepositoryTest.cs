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
                Status = InvitationStatus.PENDING
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
                Status = InvitationStatus.ACCEPTED
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
                Status = InvitationStatus.PENDING
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
                Status = InvitationStatus.PENDING
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

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}