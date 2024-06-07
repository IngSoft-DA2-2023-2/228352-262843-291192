using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDomain.Entities
{
    public class BuildingDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public decimal? CommonExpenses { get; set; }
        public Guid? ManagerId { get; set; }
        public string Manager { get; set; }
        public Guid ConstructionCompanyId { get; set; }
        public string ConstructionCompany { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();

        public BuildingDetails(Guid id, string name, string address, string location, 
                                decimal commonExpenses, Guid managerId, string manager, 
                                Guid constructionCompanyId, string constructionCompany, 
                                List<Apartment> apartments)
        {
            Id = id;
            Name = name;
            Address = address;
            Location = location;
            CommonExpenses = commonExpenses;
            ManagerId = managerId;
            Manager = manager;
            ConstructionCompanyId = constructionCompanyId;
            ConstructionCompany = constructionCompany;
            Apartments = apartments;
        }

        public override bool Equals(object obj)
        {
            var other = (BuildingDetails)obj;
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
                   Name == other.Name &&
                   Address == other.Address &&
                   Location == other.Location &&
                   CommonExpenses == other.CommonExpenses &&
                   ManagerId == other.ManagerId &&
                   Manager == other.Manager &&
                   ConstructionCompanyId == other.ConstructionCompanyId &&
                   ConstructionCompany == other.ConstructionCompany &&
                   apartmentsAreEqual;
        }
    }
}
