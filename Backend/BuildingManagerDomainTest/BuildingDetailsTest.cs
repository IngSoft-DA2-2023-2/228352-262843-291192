using BuildingManagerDomain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDomainTest
{
    [TestClass]
    public class BuildingDetailsTest
    {
        [TestMethod]
        public void BuildingDetailsNameTest()
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            string address = "address";
            string location = "location";
            decimal commonExpenses = 400;
            Guid managerId = Guid.NewGuid();
            string manager = "manager";
            Guid constructionCompanyId = Guid.NewGuid();
            string constructionCompany = "constructionCompany";
            List<Apartment> apartments = new List<Apartment>();

            BuildingDetails data = new BuildingDetails(id, name, address, location, commonExpenses, managerId, manager, constructionCompanyId, constructionCompany, apartments);

            Assert.AreEqual(id, data.Id);
            Assert.AreEqual(name, data.Name);
            Assert.AreEqual(address, data.Address);
            Assert.AreEqual(location, data.Location);
            Assert.AreEqual(commonExpenses, data.CommonExpenses);
            Assert.AreEqual(managerId, data.ManagerId);
            Assert.AreEqual(manager, data.Manager);
            Assert.AreEqual(constructionCompanyId, data.ConstructionCompanyId);
            Assert.AreEqual(constructionCompany, data.ConstructionCompany);
            Assert.AreEqual(apartments, data.Apartments);
        }
    }
}
