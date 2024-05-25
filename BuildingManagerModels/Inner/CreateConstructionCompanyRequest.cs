using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Inner
{
    public class CreateConstructionCompanyRequest
    {
        public string Name { get; set; }

        public ConstructionCompany ToEntity()
        {
            return new ConstructionCompany
            {
                Name = Name
            };
        }
    }
}
