using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIImporters;
using BuildingManagerILogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BuildingManagerLogic
{
    public class JsonImporter : IImporter
    {
        public string Name => "DefaultJson";

        private IUserRepository _userRepository;
        private IBuildingRepository _buildingRepository;

        public JsonImporter(IUserRepository userRepository, IBuildingRepository buildingRepository) 
        {
            _userRepository = userRepository;
            _buildingRepository = buildingRepository;
        }

        public List<Building> Import(string path, Guid companyId)
        {
            string jsonString = File.ReadAllText(path);

            var jsonBuildings = JsonSerializer.Deserialize<List<JsonBuilding>>(jsonString);

            List<Building> buildings = new List<Building>();

            foreach (var jsonBuilding in jsonBuildings)
            {
                Building building = new Building
                {
                    Id = Guid.NewGuid(),
                    Name = jsonBuilding.Name,
                    Address = $"({jsonBuilding.Address.MainStreet} {jsonBuilding.Address.DoorNumber}, {jsonBuilding.Address.SecondaryStreet})",
                    Location = $"({jsonBuilding.Gps.Latitude},{jsonBuilding.Gps.Longitude})",
                    CommonExpenses = jsonBuilding.CommonExpenses,
                    ConstructionCompanyId = companyId,
                    ManagerId = string.IsNullOrEmpty(jsonBuilding.ManagerEmail) ? (Guid?)null : _userRepository.GetManagerIdFromEmail(jsonBuilding.ManagerEmail),
                    Apartments = new List<Apartment>()
                };

                foreach (var jsonApartment in jsonBuilding.Apartments)
                {
                    Owner owner = _buildingRepository.GetOwnerFromEmail(jsonApartment.OwnerEmail);
                    Apartment apartment = new Apartment
                    {
                        Floor = jsonApartment.Floor,
                        Number = jsonApartment.DoorNumber,
                        Rooms = jsonApartment.Rooms,
                        Bathrooms = jsonApartment.Bathrooms,
                        HasTerrace = jsonApartment.HasTerrace,
                        Owner = owner
                    };

                    building.Apartments.Add(apartment);
                }

                buildings.Add(building);
            }

            return buildings;
        }

        private class JsonBuilding
        {
            public string Name { get; set; }
            public JsonAddress Address { get; set; }
            public string ManagerEmail { get; set; }
            public JsonGps Gps { get; set; }
            public decimal CommonExpenses { get; set; }
            public List<JsonApartment> Apartments { get; set; }
        }

        private class JsonAddress
        {
            public string MainStreet { get; set; }
            public int DoorNumber { get; set; }
            public string SecondaryStreet { get; set; }
        }

        private class JsonGps
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        private class JsonApartment
        {
            public int Floor { get; set; }
            public int DoorNumber { get; set; }
            public int Rooms { get; set; }
            public bool HasTerrace { get; set; }
            public int Bathrooms { get; set; }
            public string OwnerEmail { get; set; }
        }
    }
}
