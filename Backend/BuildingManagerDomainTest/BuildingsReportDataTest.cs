using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class BuildingsReportDataTest
    {
        [TestMethod]
        public void BuildingsReportDataOpenRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();
            string buildingName = "Building 1";

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId, buildingName);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void BuildingsReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();
            string buildingName = "Building 1";

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId, buildingName);

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void BuildingsReportDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();
            string buildingName = "Building 1";

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId, buildingName);

            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void BuildingsReportDataBuildingIdTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();
            string buildingName = "Building 1";

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId, buildingName);

            Assert.AreEqual(buildingId, data.BuildingId);
        }

        [TestMethod]
        public void BuildingsReportDataBuildingNameTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();
            string buildingName = "Building 1";

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId, buildingName);

            Assert.AreEqual(buildingName, data.BuildingName);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            BuildingsReportData data = new BuildingsReportData();

            bool result = data.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            BuildingsReportData data = new BuildingsReportData();
            object other = new object();

            bool result = data.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            BuildingsReportData data = new BuildingsReportData();

            bool result = data.Equals(data);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            BuildingsReportData data1 = new BuildingsReportData(1, 1, 1, new Guid(), "Building 1");
            BuildingsReportData data2 = new BuildingsReportData(2, 1, 1, new Guid(), "Building 1");

            bool result = data1.Equals(data2);

            Assert.IsFalse(result);
        }
    }
}
