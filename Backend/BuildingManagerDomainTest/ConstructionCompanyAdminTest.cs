using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using System.Diagnostics.CodeAnalysis;
using BuildingManagerDomain.Enums;
using BuildingManagerModels.Inner;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class ConstructionCompanyAdminTest
    {
        [TestMethod]
        public void ConstructionCompanyAdminIdTest()
        {
            Guid constructionCompanyAdminId = Guid.NewGuid();
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin { Id = constructionCompanyAdminId };
            Assert.AreEqual(constructionCompanyAdminId, constructionCompanyAdmin.Id);
        }

        [TestMethod]
        public void ConstructionCompanyAdminNameTest()
        {
            string constructionCompanyAdminName = "John";
            ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin { Name = constructionCompanyAdminName };
            Assert.AreEqual(constructionCompanyAdminName, constructionCompanyAdmin.Name);
        }

        [TestMethod]
        public void ConstructionCompanyAdminRoleTest()
        {
            RoleType role = RoleType.CONSTRUCTIONCOMPANYADMIN;
            ConstructionCompanyAdmin constructionCompanyAdmin = new() { };
            Assert.AreEqual(role, constructionCompanyAdmin.Role);
        }

        [TestMethod]
        public void ConstructionCompanyAdminEmailTest()
        {
            string email = "abc@example.com";
            ConstructionCompanyAdmin constructionCompanyAdmin = new() { Email = email };
            Assert.AreEqual(email, constructionCompanyAdmin.Email);
        }

        [TestMethod]
        public void ConstructionCompanyAdminPasswordTest()
        {
            string password = "pass123";
            ConstructionCompanyAdmin constructionCompanyAdmin = new() { Password = password };
            Assert.AreEqual(password, constructionCompanyAdmin.Password);
        }

         [TestMethod]
        public void ConstructionCompanyAdminLastnameTest()
        {
            string lastname = "";
            ConstructionCompanyAdmin constructionCompanyAdmin = new() { };
            Assert.AreEqual(lastname, constructionCompanyAdmin.Lastname);
        }

        [TestMethod]
        public void ConstructionCompanyAdminSessionTokenTest()
        {
            Guid sessionToken = new();
            ConstructionCompanyAdmin constructionCompanyAdmin = new() { SessionToken = sessionToken };
            Assert.AreEqual(sessionToken, constructionCompanyAdmin.SessionToken);
        }

        [TestMethod]
        public void CreateConstructionCompanyAdminWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateConstructionCompanyAdminRequest()
                {
                    Name = null,
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
        public void CreateConstructionCompanyAdminWithoutEmail()
        {
            Exception exception = null;
            try
            {
                var requestWithoutEmail = new CreateConstructionCompanyAdminRequest()
                {
                    Name = "John",
                    Email = null,
                    Password = "password"
                };
                requestWithoutEmail.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateConstructionCompanyAdminWithoutPassword()
        {
            Exception exception = null;
            try
            {
                var requestWithoutPassword = new CreateConstructionCompanyAdminRequest()
                {
                    Name = "John",
                    Email = "somepass",
                    Password = null
                };
                requestWithoutPassword.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }
    }
}