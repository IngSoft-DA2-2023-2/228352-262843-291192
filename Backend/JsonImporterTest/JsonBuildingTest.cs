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

        [TestMethod]
        public void BuildingAddressTest()
        {
            string address = "some address";
            JsonBuilding building = new JsonBuilding { Address = address };
            Assert.AreEqual(address, building.Address);
        }

        [TestMethod]
        public void BuildingManagerTest()
        {
            string manager = "some manager";
            JsonBuilding building = new JsonBuilding { Manager = manager };
            Assert.AreEqual(manager, building.Manager);
        }

        [TestMethod]
        public void BuildingLocationTest()
        {
            string location = "some location";
            JsonBuilding building = new JsonBuilding { Location = location };
            Assert.AreEqual(location, building.Location);
        }

        [TestMethod]
        public void BuildingCommonExpensesTest()
        {
            long expenses = 5000;
            JsonBuilding building = new JsonBuilding { CommonExpenses = expenses };
            Assert.AreEqual(expenses, building.CommonExpenses);
        }

        [TestMethod]
        public void BuildingApartmentsTest()
        {
            List<JsonApartment> apartments = new List<JsonApartment>(){ 
                new JsonApartment() {
                    Floor = 2,
                    Number = 1,
                    Rooms = 4,
                    HasTerrace = true,
                    Bathrooms = 3,
                    OwnerEmail = "some@mail.com"
                 }, 
                new JsonApartment() { 
                    Floor = 3,
                    Number = 2,
                    Rooms = 3,
                    HasTerrace = false,
                    Bathrooms = 2,
                    OwnerEmail = "some@mail2.com"
                } 
                };
            JsonBuilding building = new JsonBuilding { Apartments = apartments };
            Assert.AreEqual(apartments, building.Apartments);
        }
    }
}