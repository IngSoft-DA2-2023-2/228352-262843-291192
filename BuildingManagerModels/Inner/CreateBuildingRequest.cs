using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;
using System;
using System.Collections.Generic;

namespace BuildingManagerModels.Inner
{
    public class CreateBuildingRequest
    {
        private string _name;
        private string _address;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("name");
                }
                _name = value;
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("address");
                }
                _address = value;
            }
        }
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
