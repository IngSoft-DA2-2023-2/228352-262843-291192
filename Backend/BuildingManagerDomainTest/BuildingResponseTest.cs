using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class BuildingResponseTest
    {
        [TestMethod]
        public void BuildingResponseNameTest()
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(id, name, address, manager);

            Assert.AreEqual(name, data.Name);
        }

        [TestMethod]
        public void BuildingResponseAddressTest()
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(id, name, address, manager);

            Assert.AreEqual(address, data.Address);
        }

        [TestMethod]
        public void BuildingResponseManagerTest()
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(id, name, address, manager);

            Assert.AreEqual(manager, data.Manager);
        }

        [TestMethod]
        public void BuildingResponseIdTest()
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(id, name, address, manager);

            Assert.AreEqual(id, data.Id);
        }
    }
}
