using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IConstructionCompanyAdminLogic
    {
        public User CreateConstructionCompanyAdmin(User user, Guid sessionToken);
        public ConstructionCompany GetConstructionCompany(Guid sessionToken);
        public List<BuildingResponse> GetBuildingsFromCCAdmin(Guid userId, Guid sessionToken);
    }
}
