using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerModels.Inner
{
    public class CreateBuildingRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ConstructionCompany { get; set; }
        public decimal CommonExpenses { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public Building ToEntity()
        {
            return new Building()
            {
                Name = this.Name,
                Address = this.Address,
                Location = this.Location,
                ConstructionCompany = this.ConstructionCompany,
                CommonExpenses = this.CommonExpenses,
                Apartments = this.Apartments
            };
        }
    }
}
