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

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void MaintenanceDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests);

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void MaintenanceDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;

            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests);
            
            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }
    }
}
