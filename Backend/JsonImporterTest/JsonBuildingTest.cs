using JsonImporter;

namespace JsonImporterTest
{
    [TestClass]
    public class JsonBuildingTest
    {
        [TestMethod]
        public void BuildingNameTest()
        {
            string name = "some building name";
            JsonBuilding building = new JsonBuilding { Name = name };
            Assert.AreEqual(name, building.Name);
        }
    }
}