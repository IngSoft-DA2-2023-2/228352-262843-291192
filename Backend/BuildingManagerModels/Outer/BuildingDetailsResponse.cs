using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerModels.Outer
{
    public class BuildingDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public decimal CommonExpenses { get; set; }
        public Guid? ManagerId { get; set; }
        public string Manager { get; set; }
        public Guid ConstructionCompanyId { get; set; }
        public string ConstructionCompany { get; set; }
        public List<Apartment> Apartments { get; set; }

        public BuildingDetailsResponse(BuildingDetails buildingDetails)
        {
            Id = buildingDetails.Id;
            Name = buildingDetails.Name;
            Address = buildingDetails.Address;
            Location = buildingDetails.Location;
            CommonExpenses = (decimal)buildingDetails.CommonExpenses;
            ManagerId = buildingDetails.ManagerId;
            Manager = buildingDetails.Manager;
            ConstructionCompanyId = buildingDetails.ConstructionCompanyId;
            ConstructionCompany = buildingDetails.ConstructionCompany;
            Apartments = buildingDetails.Apartments;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (BuildingDetailsResponse)obj;
            return Id == other.Id &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Location == other.Location &&
                   CommonExpenses == other.CommonExpenses &&
                   ManagerId == other.ManagerId &&
                   Manager == other.Manager &&
                   ConstructionCompanyId == other.ConstructionCompanyId &&
                   ConstructionCompany == other.ConstructionCompany &&
                   Apartments == other.Apartments;
        }
    }
}
