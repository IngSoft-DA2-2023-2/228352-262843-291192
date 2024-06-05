using BuildingManagerDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagerIDataAccess
{
    public interface IBuildingRepository
    {
        Building CreateBuilding(Building building);
        Building DeleteBuilding(Guid buildingId);
        Guid GetUserIdBySessionToken(Guid sessionToken);
        Building UpdateBuilding(Building building);
        List<Building> ListBuildings();
        Guid GetConstructionCompanyFromBuildingId(Guid buildingId);
        Owner GetOwnerFromEmail(string email);
    }
}
