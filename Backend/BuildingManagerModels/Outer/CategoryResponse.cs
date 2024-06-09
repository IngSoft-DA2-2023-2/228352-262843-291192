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

        public CategoryResponse(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            ParentId = category.ParentId;
            ParentName = category.Parent?.Name ?? "";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            CategoryResponse other = (CategoryResponse)obj;
            return Id == other.Id && Name == other.Name && ParentId == other.ParentId && ParentName == other.ParentName;
        }
    }
}
