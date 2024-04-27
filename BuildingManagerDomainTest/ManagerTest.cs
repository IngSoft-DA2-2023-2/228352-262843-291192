using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;
using BuildingManagerDomain.Enums;

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

        [TestMethod]
        public void ManagerNameTest()
        {
            string name = "John";
            Manager manager = new Manager { Name = name };
            Assert.AreEqual(name, manager.Name);
        }

        [TestMethod]
        public void ManagerEmailTest()
        {
            string email = "abc@example.com";
            Manager manager = new() { Email = email };
            Assert.AreEqual(email, manager.Email);
        }

        [TestMethod]
        public void ManagerPasswordTest()
        {
            string password = "pass123";
            Manager manager = new() { Password = password };
            Assert.AreEqual(password, manager.Password);
        }
    }
}