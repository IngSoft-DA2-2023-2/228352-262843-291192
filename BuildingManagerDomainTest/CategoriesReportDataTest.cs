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

        [TestMethod]
        public void CategoriesReportDataOpenRequestsTest()
        {
            string categoryName = "Electricista";
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;

            CategoriesReportData data = new(categoryName, openRequests, closeRequests, inProgressRequests);
            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void CategoriesReportDataInProgressRequestsTest()
        {
            string categoryName = "Electricista";
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;

            CategoriesReportData data = new(categoryName, openRequests, closeRequests, inProgressRequests);
            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void CategoriesReportDataCategoryNameTest()
        {
            string categoryName = "Electricista";
            int openRequests = 5;
            int closeRequests = 2;
            int inProgressRequests = 1;

            CategoriesReportData data = new(categoryName, openRequests, closeRequests, inProgressRequests);
            Assert.AreEqual(categoryName, data.CategoryName);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            CategoriesReportData data = new CategoriesReportData();

            bool result = data.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            CategoriesReportData data = new CategoriesReportData();

            bool result = data.Equals(new object());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            CategoriesReportData data = new CategoriesReportData();

            bool result = data.Equals(data);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_DifferentObject_ReturnsFalse()
        {
            CategoriesReportData data = new CategoriesReportData(Guid.NewGuid().ToString(), 1, 2, 3);
            CategoriesReportData other = new CategoriesReportData(Guid.NewGuid().ToString(), 1, 3, 2);

            bool result = data.Equals(other);

            Assert.IsFalse(result);
        }

    }
}
