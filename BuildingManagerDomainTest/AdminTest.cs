using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;

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

        [TestMethod]
        public void CreateAdminWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateAdminRequest()
                {
                    Name = null,
                    Lastname = "Doe",
                    Email = "abc@email.com",
                    Password = "password"
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
        public void CreateAdminWithoutLastname()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateAdminRequest()
                {
                    Name = "John",
                    Lastname = null,
                    Email = "abc@email.com",
                    Password = "password"
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
        public void CreateAdminWithoutEmail()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateAdminRequest()
                {
                    Name = "John",
                    Lastname = "Doe",
                    Email = null,
                    Password = "password"
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
        public void CreateAdminWithoutPassword()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateAdminRequest()
                {
                    Name = "John",
                    Lastname = "Doe",
                    Email = "abc@email.com",
                    Password = null
                };
                requestWithoutName.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }
    }
}