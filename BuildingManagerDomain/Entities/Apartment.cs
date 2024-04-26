using System;

namespace BuildingManagerDomain.Entities
{
    public class Apartment
    {
        private int floor;
        private int number;
        private int rooms;

        public int Floor
        {
            get { return floor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Floor must be greater than 0");
                }
                floor = value;
            }
        }

        public int Number
        {
            get { return number; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number must be greater than 0");
                }
                number = value;
            }
        }

        public int Rooms
        {
            get { return rooms; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Rooms must be greater than 0");
                }
                rooms = value;
            }
        }

        public int Bathrooms { get; set; }
        public bool HasTerrace { get; set; }
        public Owner Owner { get; set; }
        public Guid BuildingId { get; set; }
    }
}
