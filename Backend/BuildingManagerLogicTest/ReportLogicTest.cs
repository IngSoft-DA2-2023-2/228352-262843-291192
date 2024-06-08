using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class ReportLogicTest
    {
        [TestMethod]
        public void GetMaintenancesReportSuccessfully()
        {
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                Apartments = new List<Apartment>(){
                    new Apartment(){
                        Floor = 1,
                        Number = 1,
                        Bathrooms = 1,
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        HasTerrace = false,
                        Owner = new Owner(){
                            Name = "name",
                            LastName = "lastName",
                            Email = "email@gmail.com",
                        },
                        Rooms = 1,
                    }
                }
            };
            List<Request> requests =
            [new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.OPEN,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category()
                    {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "name"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.CLOSE,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category()
                    {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "name"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.PENDING,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category()
                    {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "name"
                    }
                }
            ];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "name", new Guid("11111111-1111-1111-1111-111111111111"), "name", 1, 1, "name lastName", building.Name)];
            var report = new ReportLogic(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = report.GetReport(new Guid("11111111-1111-1111-1111-111111111111"), "", ReportType.MAINTENANCE);
            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }

        [TestMethod]
        public void GetBuildingsReportSuccessfully()
        {
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                Apartments = new List<Apartment>(){
                    new Apartment(){
                        Floor = 1,
                        Number = 1,
                        Bathrooms = 1,
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        HasTerrace = false,
                        Owner = new Owner(){
                            Name = "name",
                            LastName = "lastName",
                            Email = "email@gmail.com",
                        },
                        Rooms = 1,
                    }
                }
            };
            List<Request> requests =
            [new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.OPEN,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category()
                    {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "name"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.CLOSE,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category()
                    {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "name"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.PENDING,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category()
                    {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "name"
                    }
                }
            ];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "name", new Guid("11111111-1111-1111-1111-111111111111"), "name", 1, 1, "name lastName", building.Name)];
            var report = new ReportLogic(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = report.GetReport(null, "", ReportType.BUILDINGS);
            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }

        [TestMethod]
        public void GetCategoriesReportSuccessfully()
        {
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                Apartments = new List<Apartment>(){
                    new Apartment(){
                        Floor = 1,
                        Number = 1,
                        Bathrooms = 1,
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        HasTerrace = false,
                        Owner = new Owner(){
                            Name = "name",
                            LastName = "lastName",
                            Email = "email@gmail.com",
                        },
                        Rooms = 1,
                    }
                }
            };
            List<Request> requests =
            [new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.OPEN,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.CLOSE,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.PENDING,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                }
            ];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "name", new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", 1, 1, "name lastName", building.Name)];
            var report = new ReportLogic(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = report.GetReport(new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", ReportType.CATEGORIES);
            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }

        [TestMethod]
        public void GetCategoriesReportWithoutMaintenanceStaffTesst()
        {
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                Apartments = new List<Apartment>(){
                    new Apartment(){
                        Floor = 1,
                        Number = 1,
                        Bathrooms = 1,
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        HasTerrace = false,
                        Owner = new Owner(){
                            Name = "name",
                            LastName = "lastName",
                            Email = "email@gmail.com",
                        },
                        Rooms = 1,
                    }
                }
            };
            List<Request> requests =
            [new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.OPEN,
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.CLOSE,
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.PENDING,
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                }
            ];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "", new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", 1, 1, "name lastName", building.Name)];
            var report = new ReportLogic(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = report.GetReport(new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", ReportType.CATEGORIES);
            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
            Assert.AreEqual(data.First().CategoryName, result.First().CategoryName);
        }

        [TestMethod]
        public void GetReportWithInvalidIdentifierTest()
        {
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            var report = new ReportLogic(requestRepositoryMock.Object, buildingLogicMock.Object);
            Exception exception = null;

            try
            {
                report.GetReport(null, "", ReportType.MAINTENANCE);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            requestRepositoryMock.VerifyAll();
            Assert.IsInstanceOfType(exception, typeof(NotFoundException));
        }

        [TestMethod]
        public void GetApartmentsReportSuccessfully()
        {
            Building building = new Building
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "name",
                Address = "address",
                Apartments = new List<Apartment>(){
                    new Apartment(){
                        Floor = 1,
                        Number = 1,
                        Bathrooms = 1,
                        BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                        HasTerrace = false,
                        Owner = new Owner(){
                            Name = "name",
                            LastName = "lastName",
                            Email = "email@gmail.com",
                        },
                        Rooms = 1,
                    }
                }
            };
            List<Request> requests =
            [new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.OPEN,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.CLOSE,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = new Guid("11111111-1111-1111-1111-111111111111"),
                    ApartmentFloor = 1,
                    ApartmentNumber = 1,
                    State = RequestState.PENDING,
                    MaintainerStaffId = new Guid("11111111-1111-1111-1111-111111111111"),
                    MaintenanceStaff = new MaintenanceStaff(){
                        Name = "name",
                        Role = RoleType.MAINTENANCE,
                        Id = new Guid("11111111-1111-1111-1111-111111111111")
                    },
                    Category = new Category(){
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "Electricista"
                    }
                }
            ];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "name", new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", 1, 1, "name lastName", building.Name)];
            var report = new ReportLogic(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = report.GetReport(new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", ReportType.APARTMENTS);
            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }
    }
}
