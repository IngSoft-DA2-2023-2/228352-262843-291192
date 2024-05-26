using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class ConstructionCompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ConstructionCompanyResponse(ConstructionCompany constructionCompany)
        {
            Id = constructionCompany.Id;
            Name = constructionCompany.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            ConstructionCompanyResponse other = (ConstructionCompanyResponse)obj;
            return Id == other.Id && Name == other.Name;
        }
    }
}
