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
        List<Building> ListBuildings();
        Guid GetConstructionCompanyFromBuildingId(Guid buildingId);
        Building GetBuildingById(Guid buildingId);
    }
}
