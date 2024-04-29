using BuildingManagerModels.CustomExceptions;
using System;

namespace BuildingManagerModels.Inner
{
    public class DeleteBuildingRequest
    {
        private Guid buildingId;

        public Guid BuildingId
        {
            get { return buildingId; }
            set
            {
                if (value == Guid.Empty)
                {
                    throw new InvalidArgumentException("buildingId");
                }
                buildingId = value;
            }
        }

        public Guid ToEntity()
        {
            return buildingId;
        }
    }
}
