using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Inner
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }

        public Category ToEntity()
        {
            return new Category
            {
                Name = Name
            };
        }
    }
}
