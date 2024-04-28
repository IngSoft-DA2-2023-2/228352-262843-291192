using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;
using BuildingManagerDomain.Enums;

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

        [TestMethod]
        public void RequestStateTest()
        {
            RequestState state = RequestState.OPEN;
            Request request = new() { State = state };
            Assert.AreEqual(state, request.State);
        }

        [TestMethod]
        public void RequestCategoryTest()
        {
            Guid categoryId = new();
            Request request = new() { CategoryId = categoryId };
            Assert.AreEqual(categoryId, request.CategoryId);
        }

        [TestMethod]
        public void RequestApartmentIdTest()
        {
            Guid apartmentId = new();
            Request request = new() { ApartmentId = apartmentId };
            Assert.AreEqual(apartmentId, request.ApartmentId);
        }

        [TestMethod]
        public void RequestMaintainerIdTest()
        {
            Guid maintainerId = new();
            Request request = new() { MaintainerId = maintainerId };
            Assert.AreEqual(maintainerId, request.MaintainerId);
        }
    }
}