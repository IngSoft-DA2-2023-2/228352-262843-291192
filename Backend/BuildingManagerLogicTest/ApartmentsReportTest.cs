using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ApartmentsReportTest
    {
        [TestMethod]
        public void GetReportSuccessfully()
        {
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
                        Id = new Guid("11111111-1111-1111-1111-111111111112"),
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
                        Id = new Guid("11111111-1111-1111-1111-111111111112"),
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
                        Id = new Guid("11111111-1111-1111-1111-111111111112"),
                        Name = "Electricista"
                    }
                }
            ];
            List<ReportData> data = [new ReportData(1, 1, 1, 0, "name", new Guid("11111111-1111-1111-1111-111111111111"), "Electricista", 1, 1, null)];
            var requestRepositoryMock = new Mock<IRequestRepository>(MockBehavior.Strict);
            requestRepositoryMock.Setup(x => x.GetRequests()).Returns(requests);
            var apartmentsReport = new ApartmentsReport(requestRepositoryMock.Object);

            var result = apartmentsReport.GetReport(new Guid("11111111-1111-1111-1111-111111111111"), null);

            requestRepositoryMock.VerifyAll();
            Assert.AreEqual(data.First(), result.First());
        }
    }
}
