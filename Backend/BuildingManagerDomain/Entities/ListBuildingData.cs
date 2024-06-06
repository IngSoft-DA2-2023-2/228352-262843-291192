
using System;

namespace BuildingManagerDomain.Entities
{
    public struct ListBuildingData
    {
        public ListBuildingData(string name, string address, string manager)
        {
            Name = name;
            Address = address;
            Manager = manager;
        }
        public string Name { get; }
        public string Address { get; }
        public string Manager { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ListBuildingData)obj;
            return Name == other.Name &&
            Address == other.Address && Manager == other.Manager;
        }
    }
}
