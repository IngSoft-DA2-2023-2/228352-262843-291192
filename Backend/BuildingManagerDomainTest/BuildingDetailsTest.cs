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
            string name = "test";
            string address = "address";
            string location = "location";
            decimal commonExpenses = 400;
            string manager = "manager";
            string constructionCompany = "constructionCompany";
            List<Apartment> apartments = new List<Apartment>();

            BuildingDetails data = new BuildingDetails(name, address, location, commonExpenses, manager, constructionCompany, apartments);

            Assert.AreEqual(name, data.Name);
            Assert.AreEqual(address, data.Address);
            Assert.AreEqual(location, data.Location);
            Assert.AreEqual(commonExpenses, data.CommonExpenses);
            Assert.AreEqual(manager, data.Manager);
            Assert.AreEqual(constructionCompany, data.ConstructionCompany);
            Assert.AreEqual(apartments, data.Apartments);
        }
    }
}
