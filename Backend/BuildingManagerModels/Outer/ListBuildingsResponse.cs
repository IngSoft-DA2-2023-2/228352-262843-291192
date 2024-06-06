using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ListBuildingsResponse
    {
        public List<ListBuildingData> Buildings { get; set; }

        public ListBuildingsResponse(List<Building> buildings)
        {
            Buildings = new List<ListBuildingData>();
            foreach (var building in buildings)
            {
                Buildings.Add(new ListBuildingData(
                    building.Name,
                    building.Address,
                    building.Manager
                    ));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ListBuildingsResponse)obj;
            foreach (var building in Buildings)
            {
                foreach (var otherBuilding in other.Buildings)
                {
                    if (building.Name != otherBuilding.Name ||
                    building.Address != otherBuilding.Address)
                        return false;
                }
            }
            return true;
        }
    }
}