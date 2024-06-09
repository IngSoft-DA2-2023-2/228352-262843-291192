using BuildingManagerDomain.Entities;
using System.Collections.Generic;

namespace BuildingManagerModels.Outer
{
    public class MaintainersResponse
    {
        public List<MaintainerData> Maintainers { get; set; }

        public MaintainersResponse(List<MaintenanceStaff> maintenanceStaff)
        {
            Maintainers = new List<MaintainerData>();
            foreach (var maintainer in maintenanceStaff)
            {
                Maintainers.Add(new MaintainerData(maintainer));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (MaintainersResponse)obj;
            foreach (var data in Maintainers)
            {
                foreach (var otherData in other.Maintainers)
                {
                    if (data.Name != otherData.Name ||
                        data.Id != otherData.Id ||
                        data.Email != otherData.Email ||
                        data.Lastname != otherData.Lastname)
                        return false;
                }
            }
            return true;
        }
    }
}
