using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
        public List<CategoryResponse> Children { get; set; }

        public CategoryResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.ParentId;
            ParentName = category.Parent?.Name ?? "";
            Children = new List<CategoryResponse>();
            foreach (Category c in category.Children)
            {
                Children.Add(new CategoryResponse(c));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            CategoryResponse other = (CategoryResponse)obj;
            foreach (CategoryResponse c in Children)
            {
                if (!c.Equals(other.Children))
                {
                    return false;
                }
            }
            return Id == other.Id && Name == other.Name &&
            ParentId == other.ParentId &&
            ParentName == other.ParentName;
        }
    }
}
