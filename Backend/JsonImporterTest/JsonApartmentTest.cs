using JsonImporter;

namespace JsonImporterTest
{
    [TestClass]
    public class JsonApartmentTest
    {
        [TestMethod]
        public void ApartmentFloorTest()
        {
            int floor = 2;
            JsonApartment apartment = new JsonApartment { Floor = floor };
            Assert.AreEqual(floor, apartment.Floor);
        }

        [TestMethod]
        public void ApartmentNumberTest()
        {
            int number = 1;
            JsonApartment apartment = new JsonApartment { Number = number };
            Assert.AreEqual(number, apartment.Number);
        }

        [TestMethod]
        public void ApartmentRoomsTest()
        {
            int rooms = 4;
            JsonApartment apartment = new JsonApartment { Rooms = rooms };
            Assert.AreEqual(rooms, apartment.Rooms);
        }
    }
}