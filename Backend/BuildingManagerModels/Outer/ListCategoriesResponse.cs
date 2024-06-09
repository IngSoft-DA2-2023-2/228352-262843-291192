using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class ListCategoriesResponse
    {
        public List<CreateCategoryResponse> Categories { get; set; }

        public ListCategoriesResponse(List<Category> categories)
        {
            Categories = new List<CreateCategoryResponse>();
            foreach (var category in categories)
            {
                Categories.Add(new CreateCategoryResponse(
                   category
                    ));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ListCategoriesResponse)obj;
            foreach (var category in Categories)
            {
                foreach (var otherCategory in other.Categories)
                {
                    return category.Equals(otherCategory);
                }
            }
            return true;
        }
    }
}