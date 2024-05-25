using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IConstructionCompanyLogic
    {
        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, Guid sessionToken);
    }
}
