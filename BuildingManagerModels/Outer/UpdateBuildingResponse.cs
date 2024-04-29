using BuildingManagerDomain.Entities;
using System.Collections.Generic;
using System;

namespace BuildingManagerModels.Outer
{
    public class UpdateBuildingResponse
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ConstructionCompany { get; set; }
        public decimal CommonExpenses { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public UpdateBuildingResponse(Building building)
        {
            Id = building.Id;
            ManagerId = building.ManagerId;
            Name = building.Name;
            Address = building.Address;
            Location = building.Location;
            ConstructionCompany = building.ConstructionCompany;
            CommonExpenses = building.CommonExpenses;
            Apartments = building.Apartments;
        }

        public override bool Equals(object obj)
        {
            var other = (UpdateBuildingResponse)obj;
            return Id == other.Id &&
                   ManagerId == other.ManagerId &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Location == other.Location &&
                   ConstructionCompany == other.ConstructionCompany &&
                   CommonExpenses == other.CommonExpenses &&
                   Apartments == other.Apartments;
        }
    }
}