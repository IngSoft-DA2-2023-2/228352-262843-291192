using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class CategoriesReportDataTest
    {
        [TestMethod]
        public void CategoriesReportDataCloseRequestsTest()
        {
            string categoryName = "Electricista";
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;

            CategoriesReportData data = new(categoryName, openRequests, closeRequests, inProgressRequests);
            Assert.AreEqual(closeRequests, data.CloseRequests);
        }
    }
}
