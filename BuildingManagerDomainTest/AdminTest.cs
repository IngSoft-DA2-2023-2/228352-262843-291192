using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void AdminIdTest()
        {
            Guid adminId = Guid.NewGuid();
            Admin admin = new Admin { Id = adminId };
            Assert.AreEqual(adminId, admin.Id);
        }

        [TestMethod]
        public void AdminNameTest()
        {
            string name = "John";
            Admin admin = new Admin { Name = name };
            Assert.AreEqual(name, admin.Name);
        }

        [TestMethod]
        public void AdminLastnameTest()
        {
            string lastname = "Doe";
            Admin admin = new Admin { Lastname = lastname };
            Assert.AreEqual(lastname, admin.Lastname);
        }

        [TestMethod]
        public void AdminEmailTest()
        {
            string email = "abc@example.com";
            Admin admin = new Admin { Email = email };
            Assert.AreEqual(email, admin.Email);
        }

        [TestMethod]
        public void AdminPasswordTest()
        {
            string password = "pass123";
            Admin admin = new Admin { Password = password };
            Assert.AreEqual(password, admin.Password);
        }
    }
}