using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class ApartmentReportDataTest
    {
        [TestMethod]
        public void ApartmentReportDataFloorTest()
        {
            int apartmentFloor = 1;

            ApartmentsReportData data = new(apartmentFloor);
            Assert.AreEqual(apartmentFloor, data.ApartmentFloor);
        }

    }
}
