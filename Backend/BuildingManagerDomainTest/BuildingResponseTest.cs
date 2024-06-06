using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class BuildingResponseTest
    {
        [TestMethod]
        public void BuildingResponseNameTest()
        {
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(name, address, manager);

            Assert.AreEqual(name, data.Name);
        }

        [TestMethod]
        public void BuildingResponseAddressTest()
        {
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(name, address, manager);

            Assert.AreEqual(address, data.Address);
        }

        [TestMethod]
        public void BuildingResponseManagerTest()
        {
            string name = "test";
            string address = "address";
            string manager = "manager";

            BuildingResponse data = new BuildingResponse(name, address, manager);

            Assert.AreEqual(manager, data.Manager);
        }
    }
}
