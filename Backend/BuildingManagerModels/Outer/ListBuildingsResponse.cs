using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ListBuildingsResponse
    {
        public List<BuildingResponse> Buildings { get; set; }

        public ListBuildingsResponse(List<BuildingResponse> buildings)
        {
            Buildings = new List<BuildingResponse>();
            foreach (var building in buildings)
            {
                Buildings.Add(new BuildingResponse(
                    building.Id,
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
                    building.Address != otherBuilding.Address || 
                    building.Manager != otherBuilding.Manager ||
                    building.Id != otherBuilding.Id)
                        return false;
                }
            }
            return true;
        }
    }
}