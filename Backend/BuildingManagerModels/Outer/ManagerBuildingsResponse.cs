using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ManagerBuildingsResponse
    {
        public List<ManagerBuildingData> Buildings { get; set; }

        public ManagerBuildingsResponse(List<Building> buildings)
        {
            Buildings = new List<ManagerBuildingData>();
            foreach (var building in buildings)
            {
                Buildings.Add(new ManagerBuildingData(building));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ManagerBuildingsResponse)obj;
            foreach (var data in Buildings)
            {
                foreach (var otherData in other.Buildings)
                {
                    if (data.Name != otherData.Name ||
                        data.Id != otherData.Id)
                        return false;
                }
            }
            return true;
        }
    }
}
