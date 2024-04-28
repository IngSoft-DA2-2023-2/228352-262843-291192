using BuildingManagerApi.Controllers;
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
        Guid token;

        [TestInitialize]
        public void Initialize()
        {
            token = new Guid("11111111-1111-1111-1111-111111111111");
            _loginRequest = new LoginRequest("a@abc.com", "somepassword");
            _loginResponse = new LoginResponse(token);
        }
        [TestMethod]
        public void Login_Ok()
        {
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUserLogic.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(token);
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
            LoginResponse response = new LoginResponse(new Guid());

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            LoginResponse response = new LoginResponse(new Guid());
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            LoginResponse response = new LoginResponse(new Guid());

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            Guid token1 = new Guid("11111111-1111-1111-1111-111111111111");
            Guid token2 = new Guid("22222222-2222-2222-2222-222222222222");

            LoginResponse response1 = new LoginResponse(token1);
            LoginResponse response2 = new LoginResponse(token2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }
    }
}