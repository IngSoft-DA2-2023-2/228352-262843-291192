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

            BuildingsReportData data = new(openRequests); 
            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        
    }
}
