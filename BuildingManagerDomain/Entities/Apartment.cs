using System;

namespace BuildingManagerDomain.Entities
{
    public class Apartment
    {
        private int? floor;
        private int? number;
        private int? rooms;
        private int? bathrooms;
        private Owner owner;
        private Guid? buildingId;
        private bool? hasTerrace;

        public int? Floor
        {
            get { return floor; }
            set
            {
                if (value == null || value < 0)
                {
                    throw new ArgumentException("Floor must be greater than 0");
                }
                floor = value;
            }
        }

        public int? Number
        {
            get { return number; }
            set
            {
                if (value == null || value < 0)
                {
                    throw new ArgumentException("Number must be greater than 0");
                }
                number = value;
            }
        }

        public int? Rooms
        {
            get { return rooms; }
            set
            {
                if (value == null || value < 1)
                {
                    throw new ArgumentException("Rooms must be greater than 1");
                }
                rooms = value;
            }
        }

        public int? Bathrooms
        {
            get { return bathrooms; }
            set
            {
                if (value == null || value < 1)
                {
                    throw new ArgumentException("Bathrooms must be greater than 1");
                }
                bathrooms = value;
            }
        }

        public bool? HasTerrace
        {
            get { return hasTerrace; }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("HasTerrace must not be null");
                }
                hasTerrace = value;
            }
        }
        
        public Owner Owner
        {
            get { return owner; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Owner must not be null");
                }
                owner = value;
            }
        }

        public Guid? BuildingId
        {
            get { return buildingId; }
            set
            {
                if (value == null || value == Guid.Empty)
                {
                    throw new ArgumentException("BuildingId must not be empty");
                }
                buildingId = value;
            }
        }

        public override bool Equals(object obj)
        {
            Apartment apartment = (Apartment)obj;
            return apartment.Floor == Floor && 
                   apartment.Number == Number &&
                   apartment.BuildingId == BuildingId &&
                   apartment.Rooms == Rooms &&
                   apartment.Bathrooms == Bathrooms &&
                   apartment.HasTerrace == HasTerrace &&
                   apartment.Owner.Equals(Owner);
        }
    }
}
