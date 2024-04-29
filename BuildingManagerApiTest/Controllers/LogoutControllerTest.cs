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
    }
}