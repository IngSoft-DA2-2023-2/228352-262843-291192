using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MaintenanceReportTest
    {
        [TestMethod]
        public void GetReportSuccessfully()
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
                    },
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
                    },
                    AttendedAt = 1714665140,
                    CompletedAt =1714672340,
                    Cost = 100
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
                    },
                }
            ];
            int time = (1714672340 - 1714665140) / 3600;
            List<ReportData> data = [new ReportData(1, 1, 1, time, "name", new Guid("11111111-1111-1111-1111-111111111111"), "name", 1, 1, "name lastName", building.Name)];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            var maintenanceReport = new MaintenanceReport(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = maintenanceReport.GetReport(new Guid("11111111-1111-1111-1111-111111111111"), "name");

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }
    }
}
