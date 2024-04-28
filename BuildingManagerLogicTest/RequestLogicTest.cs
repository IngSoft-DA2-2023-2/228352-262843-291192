using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerDomain.Enums;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class RequestLogicTest
    {
        private Request _request;

        [TestInitialize]
        public void Initialize()
        {
            _request = new Request()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "Test",
                State = RequestState.OPEN,
                ApartmentId = new Guid("11111111-1111-1111-1111-111111111111"),
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
            };
        }

        [TestMethod]
        public void CreateRequestSuccessfully()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.CreateRequest(It.IsAny<Request>())).Returns(_request);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.CreateRequest(_request);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(_request, result);
        }
    }
}