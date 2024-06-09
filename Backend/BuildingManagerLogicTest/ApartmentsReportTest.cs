using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    public class ApartmentsReportTest
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
                    BuildingId = building.Id,
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
                        Id = new Guid("11111111-1111-1111-1111-111111111112"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = building.Id,
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
                        Id = new Guid("11111111-1111-1111-1111-111111111112"),
                        Name = "Electricista"
                    }
                },
                new Request()
                {
                    Id = new Guid(),
                    Description = "description",
                    CategoryId = new Guid("11111111-1111-1111-1111-111111111111"),
                    BuildingId = building.Id,
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
                        Id = new Guid("11111111-1111-1111-1111-111111111112"),
                        Name = "Electricista"
                    }
                }
            ];
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "name", new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", 1, 1, "name lastName", building.Name)];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            var buildingLogicMock = new Mock<IBuildingLogic>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);    
            buildingLogicMock.Setup(x => x.GetBuildingById(It.IsAny<Guid>())).Returns(building);
            var apartmentsReport = new ApartmentsReport(requestRepositoryMock.Object, buildingLogicMock.Object);

            var result = apartmentsReport.GetReport(building.Id, null);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }
    }
}
