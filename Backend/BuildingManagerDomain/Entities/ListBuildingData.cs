
using System;

namespace BuildingManagerDomain.Entities
{
    public struct ListBuildingData
    {
        public ListBuildingData(string name)
        {
            Name = name;
        }
        public string Name { get; }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ListBuildingData)obj;
            return Name == other.Name;
        }
    }
}
