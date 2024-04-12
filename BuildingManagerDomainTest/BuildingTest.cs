using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BuildingManagerDomain.Entities;
using BuildingManagerModels.Inner;
using BuildingManagerModels.CustomExceptions;

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
        public void BuildingManagerIdTest()
        {
            Guid managerId = Guid.NewGuid();
            Building building = new Building { ManagerId = managerId };
            Assert.AreEqual(managerId, building.ManagerId);
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
            string address = "Address 1";
            Building building = new Building { Address = address };
            Assert.AreEqual(address, building.Address);
        }

        [TestMethod]
        public void BuildingLocationTest()
        {
            string location = "Location 1";
            Building building = new Building { Location = location };
            Assert.AreEqual(location, building.Location);
        }

        [TestMethod]
        public void BuildingConstructionCompanyTest()
        {
            string constructionCompany = "Company 1";
            Building building = new Building { ConstructionCompany = constructionCompany };
            Assert.AreEqual(constructionCompany, building.ConstructionCompany);
        }

        [TestMethod]
        public void BuildingCommonExpensesTest()
        {
            decimal commonExpenses = 1000;
            Building building = new Building { CommonExpenses = commonExpenses };
            Assert.AreEqual(commonExpenses, building.CommonExpenses);
        }

        [TestMethod]
        public void BuildingApartmentsTest()
        {
            Building building = new Building();

            Apartment apartment = new Apartment();
            building.Apartments.Add(apartment);

            Assert.AreEqual(1, building.Apartments.Count);
        }

        [TestMethod]
        public void CreateBuildingWithoutName()
        {
            Exception exception = null;
            try
            {
                var requestWithoutName = new CreateBuildingRequest()
                {
                    Name = null,
                    Address = "Address 1",
                    Location = "Location 1",
                    ConstructionCompany = "Company 1",
                    CommonExpenses = 1000
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutAddress()
        {
            Exception exception = null;
            try
            {
                var requestWithoutAddress = new CreateBuildingRequest()
                {
                    Name = "Building 1",
                    Address = null,
                    Location = "Location 1",
                    ConstructionCompany = "Company 1",
                    CommonExpenses = 1000
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }
    }
}
