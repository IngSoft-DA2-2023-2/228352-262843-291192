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

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            MaintenanceData data = new MaintenanceData();

            bool result = data.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            MaintenanceData data = new MaintenanceData();
            object other = new object();

            bool result = data.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            MaintenanceData data = new MaintenanceData();

            bool result = data.Equals(data);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            MaintenanceData data1 = new MaintenanceData(1,1,1,1,"John");
            MaintenanceData data2 = new MaintenanceData(2,1,1,1,"John");


            bool result = data1.Equals(data2);

            Assert.IsFalse(result);
        }
    }
}
