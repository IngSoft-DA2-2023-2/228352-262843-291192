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
            int openRequests = 2;
            int closeRequests = 1;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName, openRequests, closeRequests);
            Assert.AreEqual(apartmentFloor, data.ApartmentFloor);
        }

        [TestMethod]
        public void ApartmentReportDataNumberTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "John Doe";
            int openRequests = 2;
            int closeRequests = 1;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName, openRequests, closeRequests);
            Assert.AreEqual(apartmentNumber, data.ApartmentNumber);
        }

        [TestMethod]
        public void ApartmentReportDataOwnerNameTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "John Doe";
            int openRequests = 2;
            int closeRequests = 1;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName, openRequests, closeRequests);
            Assert.AreEqual(ownerName, data.OwnerName);
        }

        [TestMethod]
        public void ApartmentReportDataOpenRequestsTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "John Doe";
            int openRequests = 2;
            int closeRequests = 1;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName, openRequests, closeRequests);
            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void ApartmentReportDataCloseRequestsTest()
        {
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "John Doe";
            int openRequests = 2;
            int closeRequests = 1;

            ApartmentsReportData data = new(apartmentFloor, apartmentNumber, ownerName, openRequests, closeRequests);
            Assert.AreEqual(closeRequests, data.CloseRequests);
        }
    }
}
