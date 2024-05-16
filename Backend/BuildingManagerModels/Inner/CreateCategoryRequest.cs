using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidArgumentException("name");
            }
        }

        public Category ToEntity()
        {
            Validate();
            return new Category
            {
                Name = Name
            };
        }
    }
}
