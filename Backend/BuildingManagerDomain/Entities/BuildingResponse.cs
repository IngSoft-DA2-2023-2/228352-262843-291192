using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDomain.Entities
{
    public class BuildingResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Manager { get; set; }

        public BuildingResponse(Guid id, string name, string address, string manager)
        {
            Id = id;
            Name = name;
            Address = address;
            Manager = manager;
        }

        public override bool Equals(object obj)
        {
            var other = (BuildingResponse)obj;
            return Id == other.Id &&
                   Name == other.Name &&
                   Address == other.Address &&
                   Manager == other.Manager;
        }
    }
}
