using System;

namespace BuildingManagerDomain.Entities
{
    public class Apartment
    {
        private int floor;

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
        public int Number { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasTerrace { get; set; }
        public Owner Owner { get; set; }
        public Guid BuildingId { get; set; }
    }
}
