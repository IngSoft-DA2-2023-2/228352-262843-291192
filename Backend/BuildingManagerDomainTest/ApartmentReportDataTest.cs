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
            int apartmentNumber = 101;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber);
            Assert.AreEqual(apartmentFloor, data.ApartmentFloor);
        }

        [TestMethod]
        public void ApartmentReportDataNumberTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber);
            Assert.AreEqual(apartmentNumber, data.ApartmentNumber);
        }
    }
}
