using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class RequestRepositoryTest
    {
        [TestMethod]
        public void CreateRequestTest()
        {
            var context = CreateDbContext("CreateRequestTest");
            var repository = new RequestRepository(context);
            var request = new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN
            };

            repository.CreateRequest(request);
            var result = context.Set<Request>().Find(request.Id);

            Assert.AreEqual(request, result);
        }

        [TestMethod]
        public void GetRequestsTest()
        {
            var context = CreateDbContext("GetRequestsTest");
            var repository = new RequestRepository(context);
            List<Request> requests = [new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN
            }];
            repository.CreateRequest(requests[0]);

            List<Request> result = repository.GetRequests();

            Assert.AreEqual(requests.First(), result.First());
        }

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }
    }
}