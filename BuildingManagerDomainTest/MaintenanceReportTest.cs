using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class MaintenanceReportTest
    {
        [TestMethod]
        public void MaintenanceDataMaintenanceDatasTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            MaintenanceData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName);

            MaintenanceReport report = new()
            {
                maintenanceDatas = [data]
            };

            Assert.AreEqual(report.maintenanceDatas.First(), data);
        }
    }
}
