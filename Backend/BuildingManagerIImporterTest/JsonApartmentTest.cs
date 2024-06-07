using BuildingManagerIImporter;

namespace JsonImporterTest
{
    [TestClass]
    public class JsonApartmentTest
    {
        [TestMethod]
        public void ApartmentFloorTest()
        {
            int floor = 2;
            ImporterApartment apartment = new ImporterApartment { Floor = floor };
            Assert.AreEqual(floor, apartment.Floor);
        }

        [TestMethod]
        public void ApartmentNumberTest()
        {
            int number = 1;
            ImporterApartment apartment = new ImporterApartment { Number = number };
            Assert.AreEqual(number, apartment.Number);
        }

        [TestMethod]
        public void ApartmentRoomsTest()
        {
            int rooms = 4;
            ImporterApartment apartment = new ImporterApartment { Rooms = rooms };
            Assert.AreEqual(rooms, apartment.Rooms);
        }

        [TestMethod]
        public void ApartmentHasTerraceTest()
        {
            bool hasTerrace = true;
            ImporterApartment apartment = new ImporterApartment { HasTerrace = hasTerrace };
            Assert.AreEqual(hasTerrace, apartment.HasTerrace);
        }

        [TestMethod]
        public void ApartmentBathroomsTest()
        {
            int bathrooms = 3;
            ImporterApartment apartment = new ImporterApartment { Bathrooms = bathrooms };
            Assert.AreEqual(bathrooms, apartment.Bathrooms);
        }

        [TestMethod]
        public void ApartmentOwnerEmailTest()
        {
            string owner = "some@mail.com";
            ImporterApartment apartment = new ImporterApartment { OwnerEmail = owner };
            Assert.AreEqual(owner, apartment.OwnerEmail);
        }
    }
}