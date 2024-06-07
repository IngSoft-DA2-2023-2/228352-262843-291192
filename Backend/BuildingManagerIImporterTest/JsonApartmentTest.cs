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
            Apartment apartment = new Apartment { Floor = floor };
            Assert.AreEqual(floor, apartment.Floor);
        }

        [TestMethod]
        public void ApartmentNumberTest()
        {
            int number = 1;
            Apartment apartment = new Apartment { Number = number };
            Assert.AreEqual(number, apartment.Number);
        }

        [TestMethod]
        public void ApartmentRoomsTest()
        {
            int rooms = 4;
            Apartment apartment = new Apartment { Rooms = rooms };
            Assert.AreEqual(rooms, apartment.Rooms);
        }

        [TestMethod]
        public void ApartmentHasTerraceTest()
        {
            bool hasTerrace = true;
            Apartment apartment = new Apartment { HasTerrace = hasTerrace };
            Assert.AreEqual(hasTerrace, apartment.HasTerrace);
        }

        [TestMethod]
        public void ApartmentBathroomsTest()
        {
            int bathrooms = 3;
            Apartment apartment = new Apartment { Bathrooms = bathrooms };
            Assert.AreEqual(bathrooms, apartment.Bathrooms);
        }

        [TestMethod]
        public void ApartmentOwnerEmailTest()
        {
            string owner = "some@mail.com";
            Apartment apartment = new Apartment { OwnerEmail = owner };
            Assert.AreEqual(owner, apartment.OwnerEmail);
        }
    }
}