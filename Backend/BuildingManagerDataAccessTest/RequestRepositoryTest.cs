using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
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
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            var request = new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN
            };
            Guid managerId = Guid.NewGuid();
            userRepository.CreateUser(new Manager
            {
                Id = managerId,
                Name = "name",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            });
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = managerId,
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
            buildingRepository.CreateBuilding(building);
            categoryRepository.CreateCategory(category);
            repository.CreateRequest(request, managerSessionToken);
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
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = managerId,
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
            buildingRepository.CreateBuilding(building);
            List<Request> requests = [new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                Building = building,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
            }];
            userRepository.CreateUser(new User
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            });
            userRepository.CreateUser(new Manager
            {
                Id = managerId,
                Name = "name",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            });
            categoryRepository.CreateCategory(new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            });
            repository.CreateRequest(requests[0], managerSessionToken);

            List<Request> result = repository.GetRequests();

            Assert.AreEqual(requests.First(), result.First());
        }

        [TestMethod]
        public void AssignStaffTest()
        {
            var context = CreateDbContext("AssignStaffTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "manager",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            };
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = manager.Id,
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
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = building.Id,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                ManagerId = manager.Id,
                Building = building,
                Category = category
            };
            userRepository.CreateUser(manager);
            userRepository.CreateUser(staff);
            categoryRepository.CreateCategory(category);
            buildingRepository.CreateBuilding(building);
            repository.CreateRequest(request, managerSessionToken);
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
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            var staff = new MaintenanceStaff
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "manager",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            };
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = manager.Id,
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
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = building.Id,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                ManagerId = manager.Id,
                Building = building,
                Category = category,
                MaintainerStaffId = staff.Id
            };
            userRepository.CreateUser(manager);
            userRepository.CreateUser(staff);
            categoryRepository.CreateCategory(category);
            buildingRepository.CreateBuilding(building);
            repository.CreateRequest(request, managerSessionToken);

            var result = repository.AttendRequest(request.Id, staff.Id);

            Assert.AreEqual(RequestState.ATTENDING, result.State);
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
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
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
        public void AttendRequestTest_RequestFromAnotherUser()
        {
            var context = CreateDbContext("AttendRequestTest_RequestFromAnotherUser");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            var staff = new MaintenanceStaff
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            var staff2 = new MaintenanceStaff
            {
                Id = Guid.NewGuid(),
                Name = "name2",
                Lastname = "lastname2",
                Email = "email2@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "manager",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            };
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = manager.Id,
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
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = building.Id,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                ManagerId = manager.Id,
                Building = building,
                Category = category,
                MaintainerStaffId = staff.Id
            };
            userRepository.CreateUser(manager);
            userRepository.CreateUser(staff);
            userRepository.CreateUser(staff2);
            categoryRepository.CreateCategory(category);
            buildingRepository.CreateBuilding(building);
            repository.CreateRequest(request, managerSessionToken);

            Exception exception = null;
            try
            {
                repository.AttendRequest(request.Id, staff2.Id);
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
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            var staff = new MaintenanceStaff
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "manager",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            };
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = manager.Id,
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
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = building.Id,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                ManagerId = manager.Id,
                Building = building,
                Category = category,
                MaintainerStaffId = staff.Id
            };
            userRepository.CreateUser(manager);
            userRepository.CreateUser(staff);
            categoryRepository.CreateCategory(category);
            buildingRepository.CreateBuilding(building);
            repository.CreateRequest(request, managerSessionToken);

            var result = repository.AttendRequest(request.Id, staff.Id);

            Assert.AreEqual(DateTimeOffset.Now.ToUnixTimeSeconds(), result.AttendedAt);
        }

        [TestMethod]
        public void CompleteRequestTest()
        {
            var context = CreateDbContext("CompleteRequestTest");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            }; var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "manager",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            };
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = manager.Id,
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
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.ATTENDING,
                MaintainerStaffId = staff.Id,
            };
            userRepository.CreateUser(manager);
            userRepository.CreateUser(staff);
            categoryRepository.CreateCategory(category);
            buildingRepository.CreateBuilding(building);
            repository.CreateRequest(request, managerSessionToken);

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
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
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
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerSessionToken = Guid.NewGuid();
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            var staff = new User
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Lastname = "lastname",
                Email = "email@mail.com",
                Password = "password",
                Role = RoleType.MAINTENANCE
            };
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Name = "manager",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Email = "manager@gmail.com",
                Password = "password"
            };
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = manager.Id,
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
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = category.Id,
                BuildingId = building.Id,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.ATTENDING,
            };
            userRepository.CreateUser(manager);
            userRepository.CreateUser(staff);
            categoryRepository.CreateCategory(category);
            buildingRepository.CreateBuilding(building);
            repository.CreateRequest(request, managerSessionToken);

            var result = repository.CompleteRequest(request.Id, 100);

            Assert.AreEqual(DateTimeOffset.Now.ToUnixTimeSeconds(), result.CompletedAt);
        }

        [TestMethod]
        public void GetRequestsByManagerTest()
        {
            var context = CreateDbContext("GetRequestsByManager");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerId = Guid.NewGuid();
            Guid managerSessionToken = Guid.NewGuid();
            Guid managerSessionToken2 = Guid.NewGuid();
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = managerId,
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
            buildingRepository.CreateBuilding(building);
            MaintenanceStaff maintenanceStaff = new MaintenanceStaff
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Lastname = "some",
                Email = "some@mail.com",
                Role = RoleType.MAINTENANCE,
                Password = "password",
                SessionToken = Guid.NewGuid(),
            };
            Category category = new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            };
            List<Request> requests = [new Request
            {
                Id = Guid.NewGuid(),
                Description = "description",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = building.Id,
                Building = building,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = maintenanceStaff.Id,
                ManagerId = managerId,
                Category = category,
                MaintenanceStaff = maintenanceStaff
            },
            new Request {
                Id = Guid.NewGuid(),
                Description = "description2",
                CategoryId = new Guid("11111111-1111-1111-1111-111111111112"),
                BuildingId = building.Id,
                Building = building,
                ApartmentFloor = 1,
                ApartmentNumber = 1,
                State = RequestState.OPEN,
                MaintainerStaffId = maintenanceStaff.Id,
                ManagerId = Guid.NewGuid(),
                Category = category,
                MaintenanceStaff = maintenanceStaff
            }];

            userRepository.CreateUser(new Manager
            {
                Id = managerId,
                Name = "name",
                Email = "email@mail.com",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Password = "password"
            });
            userRepository.CreateUser(new Manager
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken2,
                Email = "manager@gmail.com",
                Password = "password"
            });
            userRepository.CreateUser(maintenanceStaff);
            categoryRepository.CreateCategory(category);
            repository.CreateRequest(requests[0], managerSessionToken);
            repository.CreateRequest(requests[1], managerSessionToken2);

            List<Request> result = repository.GetRequestsByManager(managerSessionToken, "");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(requests.First(), result.First());
        }

        [TestMethod]
        public void GetRequestsAndManagerDoesntExistsTest()
        {
            var context = CreateDbContext("GetRequestsAndManagerDoesntExists");
            var repository = new RequestRepository(context);
            var userRepository = new UserRepository(context);
            var categoryRepository = new CategoryRepository(context);
            var buildingRepository = new BuildingRepository(context);
            Guid managerId = Guid.NewGuid();
            Guid managerSessionToken = Guid.NewGuid();
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                ManagerId = managerId,
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
            buildingRepository.CreateBuilding(building);
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
                CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
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
                Email = "email@mail.com",
                Role = RoleType.MANAGER,
                SessionToken = managerSessionToken,
                Password = "password"
            });
            categoryRepository.CreateCategory(new Category
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name"
            });
            repository.CreateRequest(requests[0], managerSessionToken);
            repository.CreateRequest(requests[1], managerSessionToken);

            Exception exception = null;
            try
            {
                repository.GetRequestsByManager(Guid.NewGuid(), "");
            }
            catch (ValueNotFoundException ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(ValueNotFoundException));
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