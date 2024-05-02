using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BuildingManagerDomain.Entities;
using BuildingManagerModels.Inner;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerDomain.Enums;

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
        public void InvitationStatusTest()
        {
            InvitationStatus status = InvitationStatus.PENDING;
            Invitation invitation = new() { Status = status };
            Assert.AreEqual(status, invitation.Status);
        }

        [TestMethod]
        public void InvitationWithoutDeadline()
        {
            Exception exception = null;
            try
            {
                var requestWithoutDeadline = new CreateInvitationRequest()
                {
                    Name = "John",
                    Email = "test@test.com",
                };
                requestWithoutDeadline.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
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
                requestWithPastDeadline.Validate();
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
                requestWithoutName.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void InvitationWithoutEmail()
        {
            Exception exception = null;
            try
            {
                var requestWithoutEmail = new CreateInvitationRequest()
                {
                    Name = "John",
                    Email = null,
                    Deadline = DateTimeOffset.UtcNow.AddYears(3).ToUnixTimeSeconds()
                };
                requestWithoutEmail.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(ArgumentException));
        }
    }
}