using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerLogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    public class MaintenanceReportControllerTest
    {
        private List<ReportData> _datas;
        private MaintenanceReportResponse _reportResponse;

        [TestInitialize]
        public void Initialize()
        {
            _datas = [new ReportData(3, 2, 1, 7, "John", new Guid(), "Electricista", null, null, null, null)];
            _reportResponse = new MaintenanceReportResponse(_datas);
        }
        [TestMethod]
        public void GetMaintenanceReport_Ok()
        {
            var mockReportLogic = new Mock<IReportLogic>(MockBehavior.Strict);
            mockReportLogic.Setup(x => x.GetReport(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<ReportType>())).Returns(_datas);
            var maintenanceReportController = new MaintenanceReportController(mockReportLogic.Object);
            OkObjectResult expected = new OkObjectResult(new MaintenanceReportResponse(_datas));
            MaintenanceReportResponse expectedObject = expected.Value as MaintenanceReportResponse;

            OkObjectResult result = maintenanceReportController.GetReport(new Guid(), "John") as OkObjectResult;
            MaintenanceReportResponse resultObject = result.Value as MaintenanceReportResponse;

            mockReportLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            MaintenanceReportResponse response = new MaintenanceReportResponse([new ReportData()]);

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            MaintenanceReportResponse response = new MaintenanceReportResponse([new ReportData()]);
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            MaintenanceReportResponse response = new MaintenanceReportResponse([new ReportData()]);

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            List<ReportData> datas1 = [new ReportData(3, 2, 1, 7, "John", new Guid(), "Electricista", null, null, null, null)];
            List<ReportData> datas2 = [new ReportData(3, 3, 1, 7, "John", new Guid(), "Electricista", null, null, null, null)];

            MaintenanceReportResponse response1 = new MaintenanceReportResponse(datas1);
            MaintenanceReportResponse response2 = new MaintenanceReportResponse(datas2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }
    }
}