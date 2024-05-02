using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingManagerModels.Inner
{
    public class CreateBuildingRequest
    {
        private string _name;
        private Guid _managerId;
        private string _address;
        private string _location;
        private string _constructionCompany;
        private decimal? _commonExpenses;

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

        public Guid ManagerId
        {
            get { return _managerId; }
            set
            {
                if (value == Guid.Empty)
                {
                    throw new InvalidArgumentException("managerId");
                }
                _managerId = value;
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
        public decimal? CommonExpenses
        {
            get { return _commonExpenses; }
            set
            {
                if (value == null || value <= 0)
                {
                    throw new InvalidArgumentException("commonExpenses");
                }
                _commonExpenses = value;
            }
        }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public Building ToEntity()
        {
            Validate();
            return new Building()
            {
                Name = this.Name,
                ManagerId = this.ManagerId,
                Address = this.Address,
                Location = this.Location,
                ConstructionCompany = this.ConstructionCompany,
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
            if (ManagerId == Guid.Empty)
            {
                throw new InvalidArgumentException("managerId");
            }
            if (string.IsNullOrEmpty(Address))
            {
                throw new InvalidArgumentException("address");
            }
            if (string.IsNullOrEmpty(Location))
            {
                throw new InvalidArgumentException("location");
            }
            if (string.IsNullOrEmpty(ConstructionCompany))
            {
                throw new InvalidArgumentException("constructionCompany");
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
