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

        [TestMethod]
        public void CreateBuildingWithoutLocation()
        {
            Exception exception = null;
            try
            {
                var requestWithoutLocation = new CreateBuildingRequest()
                {
                    Name = "Building 1",
                    Address = "Address 1",
                    Location = null,
                    ConstructionCompany = "Company 1",
                    CommonExpenses = 1000
                };
            } catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutConstructionCompany()
        {
            Exception exception = null;
            try
            {
                var requestWithoutConstructionCompany = new CreateBuildingRequest()
                {
                    Name = "Building 1",
                    Address = "Address 1",
                    Location = "Location 1",
                    ConstructionCompany = null,
                    CommonExpenses = 1000
                };
            } catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithoutCommonExpenses()
        {
            Exception exception = null;
            try
            {
                var requestWithoutCommonExpenses = new CreateBuildingRequest()
                {
                    Name = "Building 1",
                    Address = "Address 1",
                    Location = "Location 1",
                    ConstructionCompany = "Company 1",
                    CommonExpenses = 0
                };
            } catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithNegativeCommonExpenses()
        {
            Exception exception = null;
            try
            {
                var requestWithNegativeCommonExpenses = new CreateBuildingRequest()
                {
                    Name = "Building 1",
                    Address = "Address 1",
                    Location = "Location 1",
                    ConstructionCompany = "Company 1",
                    CommonExpenses = -1000
                };
            } catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsInstanceOfType(exception, typeof(InvalidArgumentException));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentFloorTest()
        {
            int floor = 3;
            Apartment apartment = new Apartment { Floor = floor };
            Assert.AreEqual(floor, apartment.Floor);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Floor == floor));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentNumberTest()
        {
            int number = 3;
            Apartment apartment = new Apartment { Number = number };
            Assert.AreEqual(number, apartment.Number);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Number == number));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentRoomsTest()
        {
            int rooms = 3;
            Apartment apartment = new Apartment { Rooms = rooms };
            Assert.AreEqual(rooms, apartment.Rooms);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Rooms == rooms));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentBathroomsTest()
        {
            int bathrooms = 1;
            Apartment apartment = new Apartment { Bathrooms = bathrooms };
            Assert.AreEqual(bathrooms, apartment.Bathrooms);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Bathrooms == bathrooms));
        }

        [TestMethod]
        public void CreateBuildingWithTerraceTest()
        {
            bool hasTerrace = true;
            Apartment apartment = new Apartment { HasTerrace = hasTerrace };
            Assert.AreEqual(hasTerrace, apartment.HasTerrace);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.HasTerrace == hasTerrace));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentOwnerTest()
        {
            Owner owner = new Owner();
            Apartment apartment = new Apartment { Owner = owner };
            Assert.AreEqual(owner, apartment.Owner);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Owner == owner));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentOwnerNameTest()
        {
            string name = "Owner 1";
            Owner owner = new Owner { Name = name };
            Assert.AreEqual(name, owner.Name);

            Apartment apartment = new Apartment { Owner = owner };
            Assert.AreEqual(owner, apartment.Owner);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Owner.Name == name));
        }

        [TestMethod]
        public void CreateBuildingWithApartmentOwnerLastNameTest()
        {
            string lastName = "Owner 1";
            Owner owner = new Owner { LastName = lastName };
            Assert.AreEqual(lastName, owner.LastName);

            Apartment apartment = new Apartment { Owner = owner };
            Assert.AreEqual(owner, apartment.Owner);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Owner.LastName == lastName));
        }

        [TestMethod]
        public void CreateBuildingWithApartementOwnerEmailTest()
        {
            string email = "ownerEmail@gmail.com";
            Owner owner = new Owner { Email = email };
            Assert.AreEqual(email, owner.Email);

            Apartment apartment = new Apartment { Owner = owner };
            Assert.AreEqual(owner, apartment.Owner);

            Building building = new Building();
            building.Apartments.Add(apartment);

            Assert.IsNotNull(building.Apartments.Find(a => a.Owner.Email == email));
        }
    }
}
