using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class CreateConstructionCompanyResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CreateConstructionCompanyResponse(ConstructionCompany constructionCompany)
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
            CreateConstructionCompanyResponse other = (CreateConstructionCompanyResponse)obj;
            return Id == other.Id && Name == other.Name;
        }
    }
}
