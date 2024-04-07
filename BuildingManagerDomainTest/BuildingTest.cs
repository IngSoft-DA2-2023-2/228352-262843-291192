using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class BuildingTest
    {
        [TestMethod]
        public void BuildingIdTest()
        {
            Guid buildingId = Guid.NewGuid();
            Building building = new Building { Id = buildingId };
            Assert.AreEqual(buildingId, building.Id);
        }

        [TestMethod]
        public void BuildingNameTest()
        {
            string name = "Building 1";
            Building building = new Building { Name = name };
            Assert.AreEqual(name, building.Name);
        }

        [TestMethod]
        public void BuildingAddressTest()
        {
            string address = "Adress 1";
            Building building = new Building { Address = address };
            Assert.AreEqual(address, building.Address);
        }
    }
}
