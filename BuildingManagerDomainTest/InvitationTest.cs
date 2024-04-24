using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BuildingManagerDomain.Entities;
using BuildingManagerModels.Inner;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class InvitationTest
    {
        [TestMethod]
        public void InvitationIdTest()
        {
            Guid invitationId = Guid.NewGuid();
            Invitation invitation = new() { Id = invitationId };
            Assert.AreEqual(invitationId, invitation.Id);
        }

        [TestMethod]
        public void InvitationEmailTest()
        {
            string email = "test@test.com";
            Invitation invitation = new() { Email = email };
            Assert.AreEqual(email, invitation.Email);
        }

        [TestMethod]
        public void InvitationNameTest()
        {
            string name = "John";
            Invitation invitation = new() { Name = name };
            Assert.AreEqual(name, invitation.Name);
        }

        [TestMethod]
        public void InvitationDeadlineTest()
        {
            long deadline = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Invitation invitation = new() { Deadline = deadline };
            Assert.AreEqual(deadline, invitation.Deadline);
        }

        [TestMethod]
        public void InvitationWithPastDeadline()
        {
            Exception exception = null;
            try
            {
                var requestWithPastDeadline = new CreateInvitationRequest()
                {
                    Name = "John",
                    Email = "test@test.com",
                    Deadline = DateTimeOffset.UtcNow.AddYears(-3).ToUnixTimeSeconds()
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void InvitationWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateInvitationRequest()
                {
                    Name = null,
                    Email = "test@test.com",
                    Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds()
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }
    }
}