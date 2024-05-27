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

        public Guid ManagerId { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public decimal CommonExpenses { get; set; }

        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public Building ToEntity()
        {
            Validate();
            return new Building()
            {
                Id = this.Id,
                Name = this.Name,
                ManagerId = this.ManagerId,
                Address = this.Address,
                Location = this.Location,
                CommonExpenses = this.CommonExpenses,
                Apartments = this.Apartments
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidArgumentException("name");
            }
            if (string.IsNullOrEmpty(Address))
            {
                throw new InvalidArgumentException("address");
            }
            if (string.IsNullOrEmpty(Location))
            {
                throw new InvalidArgumentException("location");
            }
            if (CommonExpenses <= 0)
            {
                throw new InvalidArgumentException("commonExpenses");
            }
            if (Apartments == null || Apartments.Count <= 0)
            {
                throw new InvalidArgumentException("apartments");
            }
        }
    }
}
