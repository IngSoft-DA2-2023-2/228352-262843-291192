using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class ReportDataTest
    {
        [TestMethod]
        public void ReportDataOpenRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName, buildingId); Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void ReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName, buildingId); Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void ReportDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName, buildingId);
            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void ReportDataAverageClosingTimeTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName, buildingId);
            Assert.AreEqual(averageClosingTime, data.AverageClosingTime);
        }

        [TestMethod]
        public void ReportDataMaintainerNameTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName, buildingId);

            Assert.AreEqual(maintainerName, data.MaintainerName);
        }

        [TestMethod]
        public void ReportDataBuildingIdTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName, buildingId);
            Assert.AreEqual(buildingId, data.BuildingId);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            ReportData data = new ReportData();

            bool result = data.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            ReportData data = new ReportData();
            object other = new object();

            bool result = data.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            ReportData data = new ReportData();

            bool result = data.Equals(data);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            ReportData data1 = new ReportData(1, 1, 1, 1, "John", new Guid());
            ReportData data2 = new ReportData(2, 1, 1, 1, "John", new Guid());


            bool result = data1.Equals(data2);

            Assert.IsFalse(result);
        }
    }
}
