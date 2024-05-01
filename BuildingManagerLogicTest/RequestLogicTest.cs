using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerIDataAccess.Exceptions;
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
                Id = new Guid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                ManagerId = new Guid("11111111-1111-1111-1111-111111111111")
            };
        }

        [TestMethod]
        public void CreateRequestSuccessfully()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.CreateRequest(It.IsAny<Request>(), It.IsAny<Guid>())).Returns(_request);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);
            Guid managerSessionId = Guid.NewGuid();
            var result = requestLogic.CreateRequest(_request, managerSessionId);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(_request, result);
        }

        [TestMethod]
        public void CreateRequestWithInvalidCategoryIdOrApartmentIdTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.CreateRequest(It.IsAny<Request>(), It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);
            Exception exception = null;

            try
            {
                requestLogic.CreateRequest(_request, Guid.NewGuid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetRequestsSuccessfullyTest()
        {
            List<Request> requests = new List<Request> { _request };
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.GetRequests();

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(requests, result);
        }

        [TestMethod]
        public void AssignStaffSuccessfullyTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.AssignStaff(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(_request);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.AssignStaff(_request.Id, new Guid());

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(_request, result);
        }

        [TestMethod]
        public void AssignStaffWithInvalidRequestIdTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.AssignStaff(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);
            Exception exception = null;

            try
            {
                requestLogic.AssignStaff(_request.Id, new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void AttendRequestSuccessfullyTest()
        {
            var request = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.ATTENDING,
                MaintainerStaffId = new Guid(),
                AttendedAt = 1714544162
            };
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.AttendRequest(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(request);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.AttendRequest(request.Id, (Guid)request.MaintainerStaffId);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void AttendRequestWithInvalidRequestIdTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.AttendRequest(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);
            Exception exception = null;

            try
            {
                requestLogic.AttendRequest(_request.Id, new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void CompleteRequestSuccessfullyTest()
        {
            var request = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.CLOSE,
                MaintainerStaffId = new Guid(),
                AttendedAt = 1714544162,
                CompletedAt = 1714544162,
                Cost = 100
            };
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.CompleteRequest(It.IsAny<Guid>(), It.IsAny<int>())).Returns(request);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.CompleteRequest(request.Id, request.Cost);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void CompleteRequestWithInvalidRequestIdTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.CompleteRequest(It.IsAny<Guid>(), It.IsAny<int>())).Throws(new ValueNotFoundException(""));
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);
            Exception exception = null;

            try
            {
                requestLogic.CompleteRequest(_request.Id, 100);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetAssignedRequestsSuccessfullyTest()
        {
            List<Request> requests = new List<Request> { _request };
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetAssignedRequests(It.IsAny<Guid>())).Returns(requests);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.GetAssignedRequests(new Guid());

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(requests, result);
        }

        [TestMethod]
        public void GetAssignedRequestsWithInvalidSessionTokenTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetAssignedRequests(It.IsAny<Guid>())).Throws(new ValueNotFoundException(""));
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);
            Exception exception = null;

            try
            {
                requestLogic.GetAssignedRequests(new Guid());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetRequestsByManagerSuccessfullyTest()
        {
            Guid managerId = Guid.NewGuid();
            var request = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.ATTENDING,
                MaintainerStaffId = new Guid(),
                AttendedAt = 1714544162,
                ManagerId = managerId
            };

            List<Request> requests = new List<Request> { request };
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequestsByManager(It.IsAny<Guid>())).Returns(requests);
            var requestLogic = new RequestLogic(requestRepositoryMock.Object);

            var result = requestLogic.GetRequestsByManager(managerId);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(requests, result);
        }
    }
}