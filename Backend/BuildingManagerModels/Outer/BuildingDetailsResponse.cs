using BuildingManagerDomain.Entities;
using System.Collections.Generic;

namespace BuildingManagerModels.Outer
{
    public class BuildingDetailsResponse
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public decimal CommonExpenses { get; set; }
        public string Manager { get; set; }
        public string ConstructionCompany { get; set; }
        public List<Apartment> Apartments { get; set; }

        public BuildingDetailsResponse(BuildingDetails buildingDetails)
        {
            Name = buildingDetails.Name;
            Address = buildingDetails.Address;
            Location = buildingDetails.Location;
            CommonExpenses = (decimal)buildingDetails.CommonExpenses;
            Manager = buildingDetails.Manager;
            ConstructionCompany = buildingDetails.ConstructionCompany;
            Apartments = buildingDetails.Apartments;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (BuildingDetailsResponse)obj;
            return Name == other.Name &&
                   Address == other.Address &&
                   Location == other.Location &&
                   CommonExpenses == other.CommonExpenses &&
                   Manager == other.Manager &&
                   ConstructionCompany == other.ConstructionCompany &&
                   Apartments == other.Apartments;
        }
    }
}
