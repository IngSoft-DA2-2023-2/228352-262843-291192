using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BuildingManagerDomain.Entities;

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
    }
}