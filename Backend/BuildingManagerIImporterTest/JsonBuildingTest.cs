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
            ImporterBuilding building = new ImporterBuilding { Name = name };
            Assert.AreEqual(name, building.Name);
        }

        [TestMethod]
        public void BuildingAddressTest()
        {
            string address = "some address";
            ImporterBuilding building = new ImporterBuilding { Address = address };
            Assert.AreEqual(address, building.Address);
        }

        [TestMethod]
        public void BuildingManagerTest()
        {
            string manager = "some manager";
            ImporterBuilding building = new ImporterBuilding { Manager = manager };
            Assert.AreEqual(manager, building.Manager);
        }

        [TestMethod]
        public void BuildingLocationTest()
        {
            string location = "some location";
            ImporterBuilding building = new ImporterBuilding { Location = location };
            Assert.AreEqual(location, building.Location);
        }

        [TestMethod]
        public void BuildingCommonExpensesTest()
        {
            long expenses = 5000;
            ImporterBuilding building = new ImporterBuilding { CommonExpenses = expenses };
            Assert.AreEqual(expenses, building.CommonExpenses);
        }

        [TestMethod]
        public void BuildingApartmentsTest()
        {
            List<ImporterApartment> apartments = new List<ImporterApartment>(){
                new ImporterApartment() {
                    Floor = 2,
                    Number = 1,
                    Rooms = 4,
                    HasTerrace = true,
                    Bathrooms = 3,
                    OwnerEmail = "some@mail.com"
                 },
                new ImporterApartment() {
                    Floor = 3,
                    Number = 2,
                    Rooms = 3,
                    HasTerrace = false,
                    Bathrooms = 2,
                    OwnerEmail = "some@mail2.com"
                }
                };
            ImporterBuilding building = new ImporterBuilding { Apartments = apartments };
            Assert.AreEqual(apartments, building.Apartments);
        }
    }
}