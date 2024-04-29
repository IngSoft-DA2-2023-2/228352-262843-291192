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
            MaintenanceData data = new(openRequests, closeRequests);
            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void MaintenanceDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            MaintenanceData data = new(openRequests, closeRequests);
            Assert.AreEqual(closeRequests, data.CloseRequests);
        }
    }
}
