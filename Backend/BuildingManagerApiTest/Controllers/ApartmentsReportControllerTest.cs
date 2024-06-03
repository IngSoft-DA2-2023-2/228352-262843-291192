using BuildingManagerApi.Controllers;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BuildingManagerApiTest.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ApartmentsReportControllerTest
    {
        private List<ReportData> _data;
        private ApartmentsReportResponse _reportResponse;

        [TestInitialize]
        public void Initialize()
        {
            _data = [new ReportData(3, 2, 1, 7, "John", new Guid(), "Electricista", 1, 1, "Jane")];
            _reportResponse = new ApartmentsReportResponse(_data);
        }

        [TestMethod]
        public void GetApartmentsReport_Ok()
        {
            var mockReportLogic = new Mock<IReportLogic>(MockBehavior.Strict);
            mockReportLogic.Setup(x => x.GetReport(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<ReportType>())).Returns(_data);
            var apartmentsReportController = new ApartmentsReportController(mockReportLogic.Object);
            OkObjectResult expected = new OkObjectResult(new ApartmentsReportResponse(_data));
            ApartmentsReportResponse expectedObject = expected.Value as ApartmentsReportResponse;

            OkObjectResult result = apartmentsReportController.GetReport(new Guid()) as OkObjectResult;
            ApartmentsReportResponse resultObject = result.Value as ApartmentsReportResponse;

            mockReportLogic.VerifyAll();
            Assert.AreEqual(result.StatusCode, expected.StatusCode);
            Assert.AreEqual(expectedObject, resultObject);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            ApartmentsReportResponse response = new ApartmentsReportResponse([]);

            bool result = response.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            ApartmentsReportResponse response = new ApartmentsReportResponse([new ReportData(3, 2, 1, 7, "John", new Guid(), "Electricista", 1, 101, "Jane")]);
            object other = new object();

            bool result = response.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            ApartmentsReportResponse response = new ApartmentsReportResponse([new ReportData(3, 2, 1, 7, "John", new Guid(), "Electricista", 1, 101, "Jane")]);

            bool result = response.Equals(response);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            List<ReportData> data1 = [new ReportData(3, 2, 1, 7, "John", new Guid(), "Electricista", 2, 101, "Jane")];
            List<ReportData> data2 = [new ReportData(3, 2, 1, 7, "John", new Guid(), "Plomero", 1, 101, "Jane")];
            ApartmentsReportResponse response1 = new ApartmentsReportResponse(data1);
            ApartmentsReportResponse response2 = new ApartmentsReportResponse(data2);

            bool result = response1.Equals(response2);

            Assert.IsFalse(result);
        }
    }
}
