using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateBuildingRequest
    {
        private string _name;
        private string _address;
        private string _location;
        private decimal? _commonExpenses;

        public Guid? ManagerId { get; set; }

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
