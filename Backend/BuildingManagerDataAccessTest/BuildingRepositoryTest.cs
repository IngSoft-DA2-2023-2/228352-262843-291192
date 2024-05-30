using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccessTest
{
    [TestClass]
    public class BuildingRepositoryTest
    {
        [TestMethod]
        public void CreateBuildingTest()
        {
            var context = CreateDbContext("CreateBuildingTest");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "validmail@outlook.com"
                        }
                    }
                }
            };

            repository.CreateBuilding(building);
            var result = context.Set<Building>().Find(building.Id);

            Assert.AreEqual(building, result);
        }

        private DbContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<BuildingManagerContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new BuildingManagerContext(options);
        }

        [TestMethod]
        public void ListBuildingsTest()
        {
            var context = CreateDbContext("ListBuildingsTest");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "validmail@outlook.com"
                        }
                    }
                }
            };
            repository.CreateBuilding(building);

            var result = repository.ListBuildings();

            Assert.AreEqual(building, result.First());
        }

        [TestMethod]
        public void CreateBuildingWithAlreadyInUseNameTest()
        {
            var context = CreateDbContext("CreateBuildingWithAlreadyInUseNameTest");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };

            context.Set<Building>().Add(building);
            context.SaveChanges();

            Assert.ThrowsException<ValueDuplicatedException>(() => repository.CreateBuilding(building));
        }

        [TestMethod]
        public void CreateBuildingWithSameOwnerEmailTest()
        {
            var context = CreateDbContext("CreateBuildingWithSameOwnerEmailTest");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    }
                }
            };
            context.Set<Building>().Add(building);
            context.SaveChanges();

            var building2 = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 2",
                Address = "Address 2",
                Location = "Location 2",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 2,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Oed",
                            Email = "jhon@gmail.com"
                        }
                    }
                }
            };

            Assert.ThrowsException<ValueDuplicatedException>(() => repository.CreateBuilding(building2));
        }

        [TestMethod]
        public void CreateBuildingWithValidSameOwnerEmailAndNamesTest()
        {
            var context = CreateDbContext("CreateBuildingWithValidSameOwnerEmailAndNames");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    }
                }
            };
            repository.CreateBuilding(building);

            var result = context.Set<Building>().Find(building.Id);

            Assert.AreEqual(building, result);
        }

        [TestMethod]
        public void CreateBuildingWithOwnerThatExistsButDoesntHaveApartmentTest()
        {
            var context = CreateDbContext("CreateBuildingWithOwnerThatExistsButDoesntHaveApartment");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();
            var building = new Building
            {
                Id = buildingId,
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    }
                }
            };
            Building test = repository.CreateBuilding(building);
            repository.DeleteBuilding(test.Id);

            var building2 = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 2",
                Address = "Address 2",
                Location = "Location 2",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 2,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Oed",
                            Email = "jhon@gmail.com"
                        }
                    }
                }
            };

            Assert.ThrowsException<ValueDuplicatedException>(() => repository.CreateBuilding(building2));
        }

        [TestMethod]
        public void CreateBuildingWithSameLocationAndAddressTest()
        {
            var context = CreateDbContext("CreateBuildingWithSameLocationAndAddressTest");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };
            context.Set<Building>().Add(building);
            context.SaveChanges();

            var building2 = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 2",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };

            Assert.ThrowsException<ValueDuplicatedException>(() => repository.CreateBuilding(building2));
        }

        [TestMethod]
        public void DeleteBuildingTest()
        {
            var context = CreateDbContext("DeleteBuildingTest");
            var repository = new BuildingRepository(context);
            var building = new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = Guid.NewGuid(),
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000
            };
            context.Set<Building>().Add(building);
            context.SaveChanges();

            repository.DeleteBuilding(building.Id);
            var result = context.Set<Building>().Find(building.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeleteBuildingThatDoesNotExistTest()
        {
            var context = CreateDbContext("DeleteBuildingThatDoesNotExistTest");
            var repository = new BuildingRepository(context);

            Assert.ThrowsException<ValueNotFoundException>(() => repository.DeleteBuilding(Guid.NewGuid()));
        }

        [TestMethod]
        public void GetManagerIdBySessionTokenTest()
        {
            var context = CreateDbContext("GetManagerIdBySessionToken");
            var repository = new BuildingRepository(context);
            Guid sessionToken = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            UserRepository userRepository = new UserRepository(context);
            Manager manager = new Manager
            {
                Id = managerId,
                Name = "John",
                Lastname = "",
                Email = "test@test.com",
                Password = "Somepass",
                Role = RoleType.MANAGER,
                SessionToken = sessionToken
            };
            userRepository.CreateUser(manager);

            Guid result = repository.GetUserIdBySessionToken(sessionToken);

            Assert.AreEqual(managerId, result);
        }

        [TestMethod]
        public void GetManagerIdBySessionTokenThatDoesNotExistTest()
        {
            var context = CreateDbContext("GetManagerIdBySessionTokenThatDoesNotExist");
            var repository = new BuildingRepository(context);
            Guid sessionToken = Guid.NewGuid();

            Assert.ThrowsException<ValueNotFoundException>(() => repository.GetUserIdBySessionToken(sessionToken));
        }

        [TestMethod]
        public void UpdateBuildingTest()
        {
            var context = CreateDbContext("UpdateBuildingTest");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Building originalBuilding = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jade",
                            LastName = "Doe",
                            Email = "jade@gmail.com"
                        }
                    }
                }
            };
            context.Set<Building>().Add(originalBuilding);
            context.SaveChanges();

            Building buildingUpdated = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 2",
                Address = "Address 2",
                Location = "Location 2",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 2000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 1,
                        Bathrooms = 1,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 1,
                        Number = 2,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jose",
                            LastName = "Doe",
                            Email = "jose@gmail.com"
                        }
                    }
                }
            };

            repository.UpdateBuilding(buildingUpdated);
            var result = context.Set<Building>().Find(originalBuilding.Id);

            Assert.AreEqual(buildingUpdated, result);
        }

        [TestMethod]
        public void UpdateBuildingWithOtherBuildingTest()
        {
            var context = CreateDbContext("UpdateBuildingWithOtherBuilding");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Building originalBuilding = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jade",
                            LastName = "Doe",
                            Email = "jade@gmail.com"
                        }
                    }
                }
            };

            Guid buildingBId = Guid.NewGuid();
            Guid managerBId = Guid.NewGuid();
            Building originalBuilding2 = new Building
            {
                Id = buildingBId,
                ManagerId = managerBId,
                Name = "Building B",
                Address = "Address B",
                Location = "Location B",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JohnB",
                            LastName = "Doe",
                            Email = "jhonB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JaneB",
                            LastName = "Doe",
                            Email = "janeB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JadeB",
                            LastName = "Doe",
                            Email = "jadeB@gmail.com"
                        }
                    }
                }
            };

            repository.CreateBuilding(originalBuilding);
            repository.CreateBuilding(originalBuilding2);

            Building buildingUpdated = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 2",
                Address = "Address 2",
                Location = "Location 2",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 2000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 1,
                        Bathrooms = 1,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "JohnB",
                            LastName = "Doe",
                            Email = "jhonB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 1,
                        Number = 2,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "JaneB",
                            LastName = "Doe",
                            Email = "janeB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "JoseB",
                            LastName = "Doe",
                            Email = "joseB@gmail.com"
                        }
                    }
                }
            };

            repository.UpdateBuilding(buildingUpdated);
            var result = context.Set<Building>().Find(originalBuilding.Id);

            Assert.AreEqual(buildingUpdated, result);
        }

        [TestMethod]
        public void UpdateBuildingWithNameAlreadyInUseTest()
        {
            var context = CreateDbContext("UpdateBuildingWithNameAlreadyInUse");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Building originalBuilding = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jade",
                            LastName = "Doe",
                            Email = "jade@gmail.com"
                        }
                    }
                }
            };

            Guid buildingBId = Guid.NewGuid();
            Guid managerBId = Guid.NewGuid();
            Building originalBuilding2 = new Building
            {
                Id = buildingBId,
                ManagerId = managerBId,
                Name = "Building B",
                Address = "Address B",
                Location = "Location B",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JohnB",
                            LastName = "Doe",
                            Email = "jhonB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JaneB",
                            LastName = "Doe",
                            Email = "janeB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JadeB",
                            LastName = "Doe",
                            Email = "jadeB@gmail.com"
                        }
                    }
                }
            };

            repository.CreateBuilding(originalBuilding);
            repository.CreateBuilding(originalBuilding2);

            Building buildingUpdated = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building B",
                Address = "Address 2",
                Location = "Location 2",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 2000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 1,
                        Bathrooms = 1,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 1,
                        Number = 2,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jose",
                            LastName = "Doe",
                            Email = "jose@gmail.com"
                        }
                    }
                }
            };

            Assert.ThrowsException<ValueDuplicatedException>(() => repository.UpdateBuilding(buildingUpdated));
        }

        [TestMethod]
        public void UpdateBuildingWithSameLocationAndAddressTest()
        {
            var context = CreateDbContext("UpdateBuildingWithSameLocationAndAddress");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();
            Guid managerId = Guid.NewGuid();
            Building originalBuilding = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jade",
                            LastName = "Doe",
                            Email = "jade@gmail.com"
                        }
                    }
                }
            };

            Guid buildingBId = Guid.NewGuid();
            Guid managerBId = Guid.NewGuid();
            Building originalBuilding2 = new Building
            {
                Id = buildingBId,
                ManagerId = managerBId,
                Name = "Building B",
                Address = "Address B",
                Location = "Location B",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JohnB",
                            LastName = "Doe",
                            Email = "jhonB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 2,
                        Number = 2,
                        Rooms = 4,
                        Bathrooms = 3,
                        HasTerrace = false,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JaneB",
                            LastName = "Doe",
                            Email = "janeB@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingBId,
                        Owner = new Owner
                        {
                            Name = "JadeB",
                            LastName = "Doe",
                            Email = "jadeB@gmail.com"
                        }
                    }
                }
            };

            repository.CreateBuilding(originalBuilding);
            repository.CreateBuilding(originalBuilding2);

            Building buildingUpdated = new Building
            {
                Id = buildingId,
                ManagerId = managerId,
                Name = "Building 1",
                Address = "Address B",
                Location = "Location B",
                ConstructionCompanyId = Guid.NewGuid(),
                CommonExpenses = 2000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 1,
                        Bathrooms = 1,
                        HasTerrace = false,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 1,
                        Number = 2,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jane",
                            LastName = "Doe",
                            Email = "jane@gmail.com"
                        }
                    },
                    new Apartment
                    {
                        Floor = 3,
                        Number = 3,
                        Rooms = 2,
                        Bathrooms = 1,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "Jose",
                            LastName = "Doe",
                            Email = "jose@gmail.com"
                        }
                    }
                }
            };

            Assert.ThrowsException<ValueDuplicatedException>(() => repository.UpdateBuilding(buildingUpdated));
        }

        [TestMethod]
        public void GetConstructionCompanyIdFromBuildingIdTest()
        {
            var context = CreateDbContext("GetConstructionCompanyIdFromBuildingIdTest");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid constructionCompanyId = Guid.NewGuid();
            UserRepository userRepository = new UserRepository(context);
            ConstructionCompanyRepository companyRepository = new ConstructionCompanyRepository(context);
            BuildingRepository buildingRepository = new BuildingRepository(context);
            ConstructionCompanyAdmin user = new ConstructionCompanyAdmin
            {
                Id = userId,
                Name = "John",
                Lastname = "",
                Email = "test@test.com",
                Password = "Somepass",
                Role = RoleType.CONSTRUCTIONCOMPANYADMIN,
            };
            userRepository.CreateUser(user);
            companyRepository.CreateConstructionCompany(new ConstructionCompany() { Name = "Company 1", Id = constructionCompanyId }, userId);
            Building building = new Building
            {
                Id = buildingId,
                Name = "Building 1",
                Address = "Address 1",
                Location = "Location 1",
                ConstructionCompanyId = constructionCompanyId,
                CommonExpenses = 1000,
                Apartments = new List<Apartment>
                {
                    new Apartment
                    {
                        Floor = 1,
                        Number = 1,
                        Rooms = 3,
                        Bathrooms = 2,
                        HasTerrace = true,
                        BuildingId = buildingId,
                        Owner = new Owner
                        {
                            Name = "John",
                            LastName = "Doe",
                            Email = "jhon@gmail.com"
                        }
                    },
                }
            };
            buildingRepository.CreateBuilding(building);


            Guid result = repository.GetConstructionCompanyFromBuildingId(buildingId);

            Assert.AreEqual(constructionCompanyId, result);
        }

        [TestMethod]
        public void GetConstructionCompanyIdFromBuildingIdWithInvalidBuildingIdTest()
        {
            var context = CreateDbContext("GetConstructionCompanyIdFromBuildingIdWithInvalidBuildingIdTest");
            var repository = new BuildingRepository(context);
            Guid buildingId = Guid.NewGuid();

            Assert.ThrowsException<ValueNotFoundException>(() => repository.GetConstructionCompanyFromBuildingId(buildingId));
        }
    }
}
