using BuildingManagerApi.Controllers;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class LogoutControllerTest
    {
        Guid token;

        [TestInitialize]
        public void Initialize()
        {
            token = new Guid("11111111-1111-1111-1111-111111111111");
        }
        [TestMethod]
        public void Logout_Ok()
        {
            var mockUserLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            mockUserLogic.Setup(x => x.Logout(It.IsAny<Guid>())).Returns(token);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = token.ToString();
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };
            var logoutController = new LogoutController(mockUserLogic.Object)
            {
                ControllerContext = controllerContext
            };

            OkObjectResult result = logoutController.Logout() as OkObjectResult;
            LogoutResponse resultObject = result.Value as LogoutResponse;

            mockUserLogic.VerifyAll();
            Assert.AreEqual(token, resultObject.Token);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            LogoutResponse response = new LogoutResponse(new Guid());

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            LogoutResponse response = new LogoutResponse(new Guid());
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            LogoutResponse response = new LogoutResponse(new Guid());

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            Guid token1 = new Guid("11111111-1111-1111-1111-111111111111");
            Guid token2 = new Guid("22222222-2222-2222-2222-222222222222");

            LogoutResponse response1 = new LogoutResponse(token1);
            LogoutResponse response2 = new LogoutResponse(token2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }
    }
}