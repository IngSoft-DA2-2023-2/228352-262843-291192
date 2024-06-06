using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerDomain.Entities
{
    public class BuildingResponse
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Manager { get; set; }

        public BuildingResponse(string name, string address, string manager)
        {
            Name = name;
            Address = address;
            Manager = manager;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (BuildingResponse)obj;
            return Name == other.Name &&
            Address == other.Address &&
            Manager == other.Manager;
        }
    }
}
