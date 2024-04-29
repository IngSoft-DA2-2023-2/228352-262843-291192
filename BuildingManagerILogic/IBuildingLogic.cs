using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerILogic
{
    public interface IBuildingLogic
    {
        public Building CreateBuilding(Building building);
        public Building DeleteBuilding(Guid buildingId);
        public Guid GetManagerIdBySessionToken(Guid sessionToken);
    }
}
