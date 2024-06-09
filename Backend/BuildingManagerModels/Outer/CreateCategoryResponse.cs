using BuildingManagerDomain.Entities;
using System;

namespace BuildingManagerModels.Outer
{
    public class CreateCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentName { get; set; }
        public List<CreateCategoryResponse> Children { get; set; }

        public CreateCategoryResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.ParentId;
            ParentName = category.Parent?.Name;
            Children = new List<CreateCategoryResponse>();
            foreach (Category c in category.Children)
            {
                Children.Add(new CreateCategoryResponse(c));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            CreateCategoryResponse other = (CreateCategoryResponse)obj;
            foreach (CreateCategoryResponse c in Children)
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
