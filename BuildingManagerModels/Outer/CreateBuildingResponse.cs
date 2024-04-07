using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateBuildingResponse
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ConstructionCompany { get; set; }
        public decimal CommonExpenses { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public CreateBuildingResponse(Building building)
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
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CreateBuildingResponse)obj;
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
