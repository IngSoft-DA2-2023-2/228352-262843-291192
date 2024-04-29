using System;
using System.Collections.Generic;

namespace BuildingManagerDomain.Entities
{
    public class Building
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ConstructionCompany { get; set; }
        public decimal CommonExpenses { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public override bool Equals(object obj)
        {
            var other = (Building)obj;
            return Id == other.Id &&
                   ManagerId == other.ManagerId &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Location == other.Location &&
                   ConstructionCompany == other.ConstructionCompany &&
                   CommonExpenses == other.CommonExpenses &&
                   Apartments == other.Apartments;
        }
    }
}
