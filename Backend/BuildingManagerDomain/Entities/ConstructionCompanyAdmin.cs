using System;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class ConstructionCompanyAdmin : User
    {
        public ConstructionCompanyAdmin()
        {
            Role = RoleType.CONSTRUCTIONCOMPANYADMIN;
            Lastname = "";
        }
    }
}

