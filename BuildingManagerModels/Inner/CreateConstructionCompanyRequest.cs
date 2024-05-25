using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateConstructionCompanyRequest
    {
        public string Name { get; set; }

        public ConstructionCompany ToEntity()
        {
            Validate();
            return new ConstructionCompany
            {
                Name = Name
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidArgumentException("name");
            }
        }
    }
}
