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

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}