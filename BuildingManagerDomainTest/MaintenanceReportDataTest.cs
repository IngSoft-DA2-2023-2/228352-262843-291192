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

            MaintenanceReportData data = new(openRequests, closeRequests);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void MaintenanceReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;

            MaintenanceReportData data = new(openRequests, closeRequests);

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }
    }
}
