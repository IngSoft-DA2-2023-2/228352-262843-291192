using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class MaintenanceReportDataTest
    {
        [TestMethod]
        public void MaintenanceReportDataOpenRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void MaintenanceReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void MaintenanceReportDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void MaintenanceReportDataAverageTimeRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(averageClosingTime, data.AverageClosingTime);
        }

        [TestMethod]
        public void MaintenanceReportDataMaintainerNameRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(maintainerName, data.MaintainerName);
        }
    }
}
