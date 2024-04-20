using System;

namespace BuildingManagerDomain.Entities
{
    public class Apartment
    {
        public int Floor { get; set; }
        public int Number { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasTerrace { get; set; }
        public Owner Owner { get; set; }
        public Guid BuildingId { get; set; }
    }
}
