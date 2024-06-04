using System;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class UpdateBuildingManagerRequest
    {
        public Guid ManagerId { get; set; }

        public void Validate()
        {
            if (Guid.Empty.Equals(ManagerId))
            {
                throw new InvalidArgumentException("managerId");
            }
        }
    }
}
