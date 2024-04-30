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

            BuildingsReportData data = new(openRequests, closeRequests); 

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void BuildingsReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 2;

            BuildingsReportData data = new(openRequests, closeRequests); 

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }
    }
}
