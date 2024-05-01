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

            MaintenanceReportData data = new(openRequests);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }
    }
}
