using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerIDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        Building DeleteBuilding(Guid buildingId);
        Building UpdateBuilding(Building building);
        List<BuildingResponse> ListBuildings();
        Guid GetConstructionCompanyFromBuildingId(Guid buildingId);
        Building GetBuildingById(Guid buildingId);
        Guid ModifyBuildingManager(Guid managerId, Guid buildingId);
        BuildingDetails GetBuildingDetails(Guid buildingId);
    }
}
