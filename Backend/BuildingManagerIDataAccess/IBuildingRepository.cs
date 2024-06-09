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
        Owner GetOwnerFromEmail(string email);
        Building GetBuildingById(Guid buildingId);
        Guid ModifyBuildingManager(Guid managerId, Guid buildingId);
        BuildingDetails GetBuildingDetails(Guid buildingId);
        List<Building> GetManagerBuildings(Guid managerId);
        bool CheckIfBuildingExists(Building building);
    }
}
