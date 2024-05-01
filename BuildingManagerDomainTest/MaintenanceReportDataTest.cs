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

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            MaintenanceReportData data = new MaintenanceReportData();

            bool result = data.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            MaintenanceReportData data = new MaintenanceReportData();
            object other = new object();

            bool result = data.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            MaintenanceReportData data = new MaintenanceReportData();

            bool result = data.Equals(data);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            MaintenanceReportData data1 = new MaintenanceReportData(1, 1, 1, 1, "john");
            MaintenanceReportData data2 = new MaintenanceReportData(2, 1, 1, 1, "john");

            bool result = data1.Equals(data2);

            Assert.IsFalse(result);
        }
    }
}
