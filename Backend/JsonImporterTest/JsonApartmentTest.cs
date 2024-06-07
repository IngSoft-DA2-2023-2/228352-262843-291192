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
    }
}