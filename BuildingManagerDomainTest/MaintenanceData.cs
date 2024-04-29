using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class MaintenanceDataTest
    {
        [TestMethod]
        public void MaintenanceDataOpenRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;

            string maintainerName = "John";

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);
            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void MaintenanceDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;

            string maintainerName = "John";

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);
            Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void MaintenanceDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;

            string maintainerName = "John";

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void MaintenanceDataAverageClosingTimeTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(averageClosingTime, data.AverageClosingTime);
        }

        [TestMethod]
        public void MaintenanceDataMaintainerNameTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            Assert.AreEqual(maintainerName, data.MaintainerName);
        }
    }
}
