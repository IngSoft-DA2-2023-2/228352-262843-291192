using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;
using System.Collections.Generic;
using System;

namespace BuildingManagerModels.Inner
{
    public class UpdateBuildingRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ManagerId { get; }

        public string Address { get; set; }

        public string Location { get; set; }

        public string ConstructionCompany { get; set; }

        public decimal CommonExpenses { get; set; }

        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public Building ToEntity()
        {
            return new Building()
            {
                Id = this.Id,
                Name = this.Name,
                ManagerId = this.ManagerId,
                Address = this.Address,
                Location = this.Location,
                ConstructionCompany = this.ConstructionCompany,
                CommonExpenses = this.CommonExpenses,
                Apartments = this.Apartments
            };
        }
    }
}
