using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDomainTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ManagerTest
    {
        [TestMethod]
        public void ManagerIdTest()
        {
            Guid managerId = Guid.NewGuid();
            Manager manager = new Manager { Id = managerId };
            Assert.AreEqual(managerId, manager.Id);
        }
    }
}