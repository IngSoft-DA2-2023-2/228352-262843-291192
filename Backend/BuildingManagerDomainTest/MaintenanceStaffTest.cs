using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class MaintenanceStaffTest
    {
        [TestMethod]
        public void MaintenanceStaffIdTest()
        {
            Guid maintenanceId = Guid.NewGuid();
            MaintenanceStaff maintenanceStaff = new MaintenanceStaff { Id = maintenanceId };
            Assert.AreEqual(maintenanceId, maintenanceStaff.Id);
        }

        [TestMethod]
        public void MaintenanceStaffNameTest()
        {
            string name = "John";
            MaintenanceStaff maintenanceStaff = new MaintenanceStaff { Name = name };
            Assert.AreEqual(name, maintenanceStaff.Name);
        }

        [TestMethod]
        public void MaintenanceStaffLastnameTest()
        {
            string lastname = "Doe";
            MaintenanceStaff maintenanceStaff = new MaintenanceStaff { Lastname = lastname };
            Assert.AreEqual(lastname, maintenanceStaff.Lastname);
        }

        [TestMethod]
        public void MaintenanceStaffEmailTest()
        {
            string email = "abc@example.com";
            MaintenanceStaff maintenanceStaff = new MaintenanceStaff { Email = email };
            Assert.AreEqual(email, maintenanceStaff.Email);
        }

        [TestMethod]
        public void MaintenanceStaffPasswordTest()
        {
            string password = "pass123";
            MaintenanceStaff maintenanceStaff = new MaintenanceStaff { Password = password };
            Assert.AreEqual(password, maintenanceStaff.Password);
        }

        [TestMethod]
        public void MaintenanceStaffSessionTokenTest()
        {
            Guid sessionToken = new();
            MaintenanceStaff maintenanceStaff = new() { SessionToken = sessionToken };
            Assert.AreEqual(sessionToken, maintenanceStaff.SessionToken);
        }

        [TestMethod]
        public void CreatMaintenanceStaffWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateMaintenanceStaffRequest()
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
        public void CreateMaintenanceStaffWithoutLastname()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateMaintenanceStaffRequest()
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
        public void CreateMaintenanceStaffWithoutEmail()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateMaintenanceStaffRequest()
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
        public void CreateMaintenanceStaffWithoutPassword()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateMaintenanceStaffRequest()
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