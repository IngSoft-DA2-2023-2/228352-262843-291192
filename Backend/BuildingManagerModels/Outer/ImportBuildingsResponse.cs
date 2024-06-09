using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ImportBuildingsResponse
    {
        public List<Building> Buildings { get; set; }

        public ImportBuildingsResponse(List<Building> buildings)
        {
            Buildings = new List<Building>();
            foreach (var building in buildings)
            {
                Buildings.Add(building);
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (ImportBuildingsResponse)obj;

            if (Buildings.Count != other.Buildings.Count)
            {
                return false;
            }

            for (int i = 0; i < Buildings.Count; i++)
            {
                if (!Buildings[i].Equals(other.Buildings[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}