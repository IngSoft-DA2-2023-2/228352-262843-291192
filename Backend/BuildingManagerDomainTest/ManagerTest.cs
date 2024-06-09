using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomainTest
{
    [TestClass]
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

        [TestMethod]
        public void ManagerRoleTest()
        {
            RoleType role = RoleType.MANAGER;
            Manager manager = new() { };
            Assert.AreEqual(role, manager.Role);
        }
        [TestMethod]
        public void ManagerLastnameTest()
        {
            string lastname = "";
            Manager manager = new() { };
            Assert.AreEqual(lastname, manager.Lastname);
        }

        [TestMethod]
        public void ManagerSessionTokenTest()
        {
            Guid sessionToken = new();
            Manager manager = new() { SessionToken = sessionToken };
            Assert.AreEqual(sessionToken, manager.SessionToken);
        }
    }
}