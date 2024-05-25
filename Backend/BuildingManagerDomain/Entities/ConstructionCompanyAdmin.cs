using System;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class ConstructionCompanyAdmin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoleType Role = RoleType.CONSTRUCTIONCOMPANYADMIN;
    }
}

