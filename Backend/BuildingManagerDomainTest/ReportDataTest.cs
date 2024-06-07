using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class ReportDataTest
    {
        [TestMethod]
        public void ReportDataOpenRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(openRequests, data.OpenRequests);
        }

        [TestMethod]
        public void ReportDataCloseRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(closeRequests, data.CloseRequests);
        }

        [TestMethod]
        public void ReportDataInProgressRequestsTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(inProgressRequests, data.InProgressRequests);
        }

        [TestMethod]
        public void ReportDataAverageClosingTimeTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(averageClosingTime, data.AverageClosingTime);
        }

        [TestMethod]
        public void ReportDataMaintainerNameTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(maintainerName, data.MaintainerName);
        }

        [TestMethod]
        public void ReportDataBuildingIdTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(buildingId, data.BuildingId);
        }

        [TestMethod]
        public void ReportDataApartmentFloorTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(apartmentFloor, data.ApartmentFloor);
        }

        [TestMethod]
        public void ReportDataApartmentNumberTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(apartmentNumber, data.ApartmentNumber);
        }

        [TestMethod]
        public void ReportDataApartmentOwnerNameTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(ownerName, data.OwnerName);
        }

        [TestMethod]
        public void ReportDataBuildingNameTest()
        {
            int openRequests = 5;
            int closeRequests = 5;
            int inProgressRequests = 6;
            int averageClosingTime = 10;
            string maintainerName = "John";
            Guid buildingId = new Guid();
            string categoryName = "Electricista";
            int apartmentFloor = 1;
            int apartmentNumber = 101;
            string ownerName = "Jane Doe";
            string buildingName = "Building 1";

            ReportData data = new(openRequests, closeRequests, inProgressRequests, averageClosingTime, maintainerName,
            buildingId, categoryName, apartmentFloor, apartmentNumber, ownerName, buildingName);

            Assert.AreEqual(buildingName, data.BuildingName);
        }

        [TestMethod]
        public void Equals_NullObject_ReturnsFalse()
        {
            ReportData data = new ReportData();

            bool result = data.Equals(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypeObject_ReturnsFalse()
        {
            ReportData data = new ReportData();
            object other = new object();

            bool result = data.Equals(other);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_SameObject_ReturnsTrue()
        {
            ReportData data = new ReportData();

            bool result = data.Equals(data);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_AtLeastOneFieldDifferent_ReturnsFalse()
        {
            ReportData data1 = new ReportData(1, 1, 1, 1, "John", new Guid(), "Electricista", 1, 101, "Jane Doe", "Building 1");
            ReportData data2 = new ReportData(2, 1, 1, 1, "John", new Guid(), "Electricista", 1, 101, "Jane Doe", "Building 1");


            bool result = data1.Equals(data2);

            Assert.IsFalse(result);
        }
    }
}
