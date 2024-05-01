using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess.Exceptions;
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
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            List<Request> requests = [new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
            }];
            userRepository.CreateUser(new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Role = RoleType.MAINTENANCE
            });
            categoryRepository.CreateCategory(new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            });
            repository.CreateRequest(requests[0]);

            List<Request> result = repository.GetRequests();

            Assert.AreEqual(requests.First(), result.First());
        }

        [TestMethod]
        public void AssignStaffTest()
        {
            var context = CreateDbContext("AssignStaffTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
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
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
            userRepository.CreateUser(staff);
            repository.CreateRequest(request);

            var result = repository.AssignStaff(request.Id, staff.Id);

            Assert.AreEqual(staff.Id, result.MaintainerStaffId);
        }

        [TestMethod]
        public void AssignStaffTest_StaffNotFound()
        {
            var context = CreateDbContext("AssignStaffTest_StaffNotFound");
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
            Exception exception = null;
            try
            {
                repository.AssignStaff(request.Id, Guid.NewGuid());
            }
            catch (ValueNotFoundException ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));

        }

        [TestMethod]
        public void AttendRequestTest()
        {
            var context = CreateDbContext("AttendRequestTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
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
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
            userRepository.CreateUser(staff);
            repository.CreateRequest(request);

            var result = repository.AttendRequest(request.Id, staff.Id);

            Assert.AreEqual(RequestState.ATTENDING, result.State);
            Assert.AreEqual(staff.Id, result.MaintainerStaffId);
        }

        [TestMethod]
        public void AttendRequestTest_RequestNotFound()
        {
            var context = CreateDbContext("AttendRequestTest_RequestNotFound");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
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
            userRepository.CreateUser(staff);
            Exception exception = null;
            try
            {
                repository.AttendRequest(request.Id, staff.Id);
            }
            catch (ValueNotFoundException ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void AttendRequestAttendAtNowTest()
        {
            var context = CreateDbContext("AttendRequestAttendAtNowTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
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
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
            userRepository.CreateUser(staff);
            repository.CreateRequest(request);

            var result = repository.AttendRequest(request.Id, staff.Id);

            Assert.AreEqual(DateTimeOffset.Now.ToUnixTimeSeconds(), result.AttendedAt);
        }

        [TestMethod]
        public void CompleteRequestTest()
        {
            var context = CreateDbContext("CompleteRequestTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var request = new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.ATTENDING
            };
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
            userRepository.CreateUser(staff);
            repository.CreateRequest(request);

            var result = repository.CompleteRequest(request.Id, 100);

            Assert.AreEqual(RequestState.CLOSE, result.State);
            Assert.AreEqual(100, result.Cost);
        }

        [TestMethod]
        public void CompleteRequestTest_RequestNotFound()
        {
            var context = CreateDbContext("CompleteRequestTest_RequestNotFound");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
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
            userRepository.CreateUser(staff);
            Exception exception = null;
            try
            {
                repository.CompleteRequest(request.Id, 100);
            }
            catch (ValueNotFoundException ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
        }

        [TestMethod]
        public void CompleteRequestCompletedAtNowTest()
        {
            var context = CreateDbContext("CompleteRequestCompletedAtNowTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var request = new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.ATTENDING
            };
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MAINTENANCE
            };
            userRepository.CreateUser(staff);
            repository.CreateRequest(request);

            var result = repository.CompleteRequest(request.Id, 100);

            Assert.AreEqual(DateTimeOffset.Now.ToUnixTimeSeconds(), result.CompletedAt);
        }

        [TestMethod]
        public void GetRequestsByManagerTest()
        {
            var context = CreateDbContext("GetRequestsTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            Guid managerId = Guid.NewGuid();
            Guid managerSessionToken = Guid.NewGuid();
            List<Request> requests = [new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                ManagerId = managerId

            },
            new Request {
                Id = Guid.NewGuid(),
                Description = "description2",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111112"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111112"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111112"),
                ManagerId = Guid.NewGuid()
            }];
            
            userRepository.CreateUser(new Manager
            {
                Id = managerId,
                Name = "name",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken
            });
            categoryRepository.CreateCategory(new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            });
            repository.CreateRequest(requests[0]);
            repository.CreateRequest(requests[1]);

            List<Request> result = repository.GetRequestsByManager(managerSessionToken);

            Assert.AreEqual(1, result.Count);
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