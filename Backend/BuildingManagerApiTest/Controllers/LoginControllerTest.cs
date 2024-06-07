using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        private LoginRequest _loginRequest;
        private LoginResponse _loginResponse;
        User user;

        [TestInitialize]
        public void Initialize()
        {
            Guid token = new Guid("11111111-1111-1111-1111-111111111111");
            user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123",
                SessionToken = token
            };
            _loginRequest = new LoginRequest("a@abc.com", "somepassword");
            _loginResponse = new LoginResponse(user);
        }
        [TestMethod]
        public void Login_Ok()
        {
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUserLogic.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            var LoginController = new LoginController(mockUserLogic.Object);

            var result = LoginController.Login(_loginRequest);
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as LoginResponse;

            mockUserLogic.VerifyAll();
            Assert.AreEqual(_loginResponse, content);
        }

        [TestMethod]
        public void LoginRequestWithoutEmailTest()
        {
            Exception exception = null;
            try
            {
                var requestWithoutEmail = new LoginRequest(null, "somepassword");

                requestWithoutEmail.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void LoginRequestWithoutPasswordTest()
        {
            Exception exception = null;
            try
            {
                var requestWithoutPassword = new LoginRequest("test@test.com", null);

                requestWithoutPassword.Validate();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123",
                SessionToken = Guid.NewGuid()
            };
            LoginResponse response = new LoginResponse(user);

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123",
                SessionToken = Guid.NewGuid()
            };
            LoginResponse response = new LoginResponse(user);
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123",
                SessionToken = Guid.NewGuid()
            };
            LoginResponse response = new LoginResponse(user);

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            User user1 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123",
                SessionToken = Guid.NewGuid()
            };

            User user2 = new User()
            {
                Id = Guid.NewGuid(),
                Name = "John1",
                Lastname = "Doe",
                Email = "john@abc.com",
                Password = "pass123",
                SessionToken = Guid.NewGuid()
            };

            LoginResponse response1 = new LoginResponse(user1);
            LoginResponse response2 = new LoginResponse(user2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }
    }
}