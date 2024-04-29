using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class MaintenanceReportTest
    {
        [TestMethod]
        public void MaintenanceReportMaintenanceDatasTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            MaintenanceReport report = new()
            {
                MaintenanceDatas = [data]
            };

            Assert.AreEqual(report.MaintenanceDatas.First(), data);
        }

        [TestMethod]
        public void MaintenanceReportRequestsTest()
        {
            Request request = new();

            MaintenanceReport report = new()
            {
                Requests = [request]
            };

            Assert.AreEqual(report.Requests.First(), request);
        }
    }
}
