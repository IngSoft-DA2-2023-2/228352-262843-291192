using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class RequestControllerTest
    {
        private Request _request;
        private CreateRequestRequest _createRequestRequest;
        private RequestResponse _requestResponse;
        private Building _building;

        [TestInitialize]
        public void Initialize()
        {
            _building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = new Guid("11111111-1111-1111-1111-111111111111"),
                ConstructionCompanyId = new Guid("11111111-1111-1111-1111-111111111111"),
                CommonExpenses = 100,
                Location = "location",
                Apartments = [
                    new Apartment
                    {
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        Floor = 1,
                        Number = 1,
                        Owner = new Owner
                        {
                            Name = "name",
                            LastName = "lastname",
                            Email = "some@mail.com"
                        },
                        Bathrooms = 1,
                        HasTerrace = true,
                        Rooms = 1
                    }
                ]
            };
            _request = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                ManagerId = new Guid("11111111-1111-1111-1111-111111111111"),
                Building = _building
            };
            _createRequestRequest = new CreateRequestRequest
            {
                Description = _request.Description,
                CategoryId = _request.CategoryId,
                BuildingId = _request.BuildingId,
                ApartmentFloor = _request.ApartmentFloor,
                ApartmentNumber = _request.ApartmentNumber,
                ManagerId = _request.ManagerId,
            };
            _requestResponse = new RequestResponse(_request);

        }
        [TestMethod]
        public void CreateRequest_Ok()
        {
            var mockRequestLogic = new Mock<IRequestLogic>(MockBehavior.Strict);
            mockRequestLogic.Setup(x => x.CreateRequest(It.IsAny<Request>(), It.IsAny<Guid>())).Returns(_request);
            var requestController = new RequestController(mockRequestLogic.Object);

            var result = requestController.CreateRequest(_createRequestRequest, Guid.NewGuid());
            var createdAtActionResult = result as CreatedAtActionResult;
            var content = createdAtActionResult.Value as RequestResponse;

            mockRequestLogic.VerifyAll();
            Assert.AreEqual(_requestResponse, content);
            Assert.AreEqual(content.State, RequestState.OPEN);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            RequestResponse response = new RequestResponse(new Request());

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            RequestResponse response = new RequestResponse(new Request());
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            RequestResponse response = new RequestResponse(new Request());

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            Request request1 = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                Building = _building
            };

            Request request2 = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "different description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                Building = _building
            };

            RequestResponse response1 = new RequestResponse(request1);
            RequestResponse response2 = new RequestResponse(request2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AssignStaff_Ok()
        {
            var request = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = new Guid(),
                ManagerId = new Guid("11111111-1111-1111-1111-111111111111"),
                Building = _building
            };
            var mockRequestLogic = new Mock<IRequestLogic>(MockBehavior.Strict);
            mockRequestLogic.Setup(x => x.AssignStaff(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(request);
            var requestController = new RequestController(mockRequestLogic.Object);

            var result = requestController.AssignStaff(_request.Id, (Guid)request.MaintainerStaffId);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as RequestResponse;

            mockRequestLogic.VerifyAll();
            Assert.AreEqual(_requestResponse, content);
        }

        [TestMethod]
        public void AttendRequest_Ok()
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
                AttendedAt = 1714544162,
                Building = _building
            };
            var requestResponse = new RequestResponse(request);
            var mockRequestLogic = new Mock<IRequestLogic>(MockBehavior.Strict);
            mockRequestLogic.Setup(x => x.AttendRequest(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(request);
            var requestController = new RequestController(mockRequestLogic.Object);

            var result = requestController.AttendRequest(request.Id, (Guid)request.MaintainerStaffId);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as RequestResponse;

            mockRequestLogic.VerifyAll();
            Assert.AreEqual(requestResponse, content);
        }

        [TestMethod]
        public void CompleteRequest_Ok()
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
                Cost = 100,
                Building = _building
            };
            var requestResponse = new RequestResponse(request);
            var mockRequestLogic = new Mock<IRequestLogic>(MockBehavior.Strict);
            mockRequestLogic.Setup(x => x.CompleteRequest(It.IsAny<Guid>(), It.IsAny<int>())).Returns(request);
            var requestController = new RequestController(mockRequestLogic.Object);

            var result = requestController.CompleteRequest(request.Id, request.Cost);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as RequestResponse;

            mockRequestLogic.VerifyAll();
            Assert.AreEqual(requestResponse, content);
        }

        [TestMethod]
        public void GetAssignedRequests_Ok()
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
                AttendedAt = 1714544162,
                Building = _building
            };
            var requestResponse = new RequestResponse(request);
            var mockRequestLogic = new Mock<IRequestLogic>(MockBehavior.Strict);
            mockRequestLogic.Setup(x => x.GetAssignedRequests(It.IsAny<Guid>())).Returns(new List<Request> { request });
            var requestController = new RequestController(mockRequestLogic.Object);

            var result = requestController.GetAssignedRequests((Guid)request.MaintainerStaffId);
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as List<Request>;

            mockRequestLogic.VerifyAll();
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual(request, content[0]);
        }

        [TestMethod]
        public void GetRequestsByManager_Ok()
        {
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = new Guid("11111111-1111-1111-1111-111111111111"),
                ConstructionCompanyId = new Guid("11111111-1111-1111-1111-111111111111"),
                CommonExpenses = 100,
                Location = "location",
                Apartments = [
                    new Apartment
                    {
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        Floor = 1,
                        Number = 1,
                        Owner = new Owner
                        {
                            Name = "name",
                            LastName = "lastname",
                            Email = "some@mail.com"
                        },
                        Bathrooms = 1,
                        HasTerrace = true,
                        Rooms = 1
                    }
                ]
            };
            var request = new Request
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                ManagerId = new Guid("11111111-1111-1111-1111-111111111111"),
                Category = new Category
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    Name = "Electricista"
                },
                Building = building
            };
            var requestResponse = new RequestResponse(request);
            var mockRequestLogic = new Mock<IRequestLogic>(MockBehavior.Strict);
            mockRequestLogic.Setup(x => x.GetRequestsByManager(It.IsAny<Guid>(), It.IsAny<string>())).Returns(new List<Request> { request });
            var requestController = new RequestController(mockRequestLogic.Object);

            var result = requestController.GetRequestsByManager(request.BuildingId, "Electricista");
            var okObjectResult = result as OkObjectResult;
            var content = okObjectResult.Value as List<Request>;

            mockRequestLogic.VerifyAll();
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual(request, content[0]);
        }
    }
}