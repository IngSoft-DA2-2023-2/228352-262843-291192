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
        private string _location;
        private string _constructionCompany;

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
        public string Location
        {
            get { return _location; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("location");
                }
                _location = value;
            }
        }
        public string ConstructionCompany
        {
            get { return _constructionCompany; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("constructionCompany");
                }
                _constructionCompany = value;
            }
        }
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
