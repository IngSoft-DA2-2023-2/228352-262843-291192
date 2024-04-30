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

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void BuildingsReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId);

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void BuildingsReportDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId);

            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void BuildingsReportDataBuildingIdTest()
        {
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;
            Guid buildingId = Guid.NewGuid();

            BuildingsReportData data = new(openRequests, closeRequests, inProgressRequests, buildingId);

            Assert.AreEqual(buildingId, data.BuildingId);
        }
    }
}
