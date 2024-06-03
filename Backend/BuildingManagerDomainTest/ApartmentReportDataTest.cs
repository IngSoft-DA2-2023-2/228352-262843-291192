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
            string ownerName = "John Doe";

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName);
            Assert.AreEqual(apartmentFloor, data.ApartmentFloor);
        }

        [TestMethod]
        public void ApartmentReportDataNumberTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "John Doe";

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName); 
            Assert.AreEqual(apartmentNumber, data.ApartmentNumber);
        }

        [TestMethod]
        public void ApartmentReportDataOwnerNameTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "John Doe";

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName);
            Assert.AreEqual(ownerName, data.OwnerName);
        }
    }
}
