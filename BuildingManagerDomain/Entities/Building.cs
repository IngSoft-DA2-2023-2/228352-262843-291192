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
        public decimal? CommonExpenses { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public override bool Equals(object obj)
        {
            var other = (Building)obj;
            bool apartmentsAreEqual = true;
            if (Apartments.Count != other.Apartments.Count)
            {
                apartmentsAreEqual = false;
            }
            else
            {
                for (int i = 0; i < Apartments.Count; i++)
                {
                    if (!Apartments.Contains(other.Apartments[i]))
                    {
                        apartmentsAreEqual = false;
                        break;
                    }
                }
            }
            return Id == other.Id &&
                   ManagerId == other.ManagerId &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Location == other.Location &&
                   ConstructionCompany == other.ConstructionCompany &&
                   CommonExpenses == other.CommonExpenses &&
                   apartmentsAreEqual;
        }
    }
}
