
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class InvitationAnswerTest
    {
        [TestMethod]
        public void InvitationAnswerIdTest()
        {
            var invitationId = new Guid();
            var invitationAnswer = new InvitationAnswer { InvitationId = invitationId };
            Assert.AreEqual(invitationId, invitationAnswer.InvitationId);
        }

        [TestMethod]
        public void InvitationAnswerStatusTest()
        {
            var status = InvitationStatus.PENDING;
            var invitationAnswer = new InvitationAnswer { Status = status };
            Assert.AreEqual(status, invitationAnswer.Status);
        }

        [TestMethod]
        public void InvitationAnswerEmailTest()
        {
            var email = "abc@example.com";
            var invitationAnswer = new InvitationAnswer { Email = email };
            Assert.AreEqual(email, invitationAnswer.Email);
        }

        [TestMethod]
        public void InvitationAnswerPasswordTest()
        {
            var password = "pass123";
            var invitationAnswer = new InvitationAnswer { Password = password };
            Assert.AreEqual(password, invitationAnswer.Password);
        }
    }
}
