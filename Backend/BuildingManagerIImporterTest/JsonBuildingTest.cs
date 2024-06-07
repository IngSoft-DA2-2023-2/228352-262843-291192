using BuildingManagerIImporter;

namespace JsonImporterTest
{
    [TestClass]
    public class JsonBuildingTest
    {
        [TestMethod]
        public void BuildingNameTest()
        {
            string name = "some building name";
            Building building = new Building { Name = name };
            Assert.AreEqual(name, building.Name);
        }

        [TestMethod]
        public void BuildingAddressTest()
        {
            string address = "some address";
            Building building = new Building { Address = address };
            Assert.AreEqual(address, building.Address);
        }

        [TestMethod]
        public void BuildingManagerTest()
        {
            string manager = "some manager";
            Building building = new Building { Manager = manager };
            Assert.AreEqual(manager, building.Manager);
        }

        [TestMethod]
        public void BuildingLocationTest()
        {
            string location = "some location";
            Building building = new Building { Location = location };
            Assert.AreEqual(location, building.Location);
        }

        [TestMethod]
        public void BuildingCommonExpensesTest()
        {
            long expenses = 5000;
            Building building = new Building { CommonExpenses = expenses };
            Assert.AreEqual(expenses, building.CommonExpenses);
        }

        [TestMethod]
        public void BuildingApartmentsTest()
        {
            List<Apartment> apartments = new List<Apartment>(){ 
                new Apartment() {
                    Floor = 2,
                    Number = 1,
                    Rooms = 4,
                    HasTerrace = true,
                    Bathrooms = 3,
                    OwnerEmail = "some@mail.com"
                 }, 
                new Apartment() { 
                    Floor = 3,
                    Number = 2,
                    Rooms = 3,
                    HasTerrace = false,
                    Bathrooms = 2,
                    OwnerEmail = "some@mail2.com"
                } 
                };
            Building building = new Building { Apartments = apartments };
            Assert.AreEqual(apartments, building.Apartments);
        }
    }
}