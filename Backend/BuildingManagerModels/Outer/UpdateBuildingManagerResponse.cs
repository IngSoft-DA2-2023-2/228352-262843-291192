using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class UpdateBuildingManagerResponse
    {
        public Guid ManagerId { get; set; }
        public Guid BuildingId { get; set; }    

        public UpdateBuildingManagerResponse(Guid managerId, Guid buildingId)
        {
            ManagerId = managerId;
            BuildingId = buildingId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            UpdateBuildingManagerResponse other = (UpdateBuildingManagerResponse)obj;
            return BuildingId == other.BuildingId && ManagerId == other.ManagerId;
        }
    }
}
