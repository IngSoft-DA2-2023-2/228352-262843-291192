using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDomainTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RequestTest
    {
        [TestMethod]
        public void RequestIdTest()
        {
            Guid requestId = Guid.NewGuid();
            Request request = new() { Id = requestId };
            Assert.AreEqual(requestId, request.Id);
        }

        [TestMethod]
        public void RequestDescriptionTest()
        {
            string description = "some description";
            Request request = new() { Description = description };
            Assert.AreEqual(description, request.Description);
        }
    }
}