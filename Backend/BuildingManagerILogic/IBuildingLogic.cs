using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;

namespace BuildingManagerILogic
{
    public interface IBuildingLogic
    {
        public Building CreateBuilding(Building building, Guid sessionToken);
        public Building DeleteBuilding(Guid buildingId, Guid sessionToken);
        public Building UpdateBuilding(Building building);
        public List<Building> ListBuildings();
        public Guid GetConstructionCompanyFromBuildingId(Guid buildingId);
    }
}
