using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateRequestRequest
    {
        public string Description { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid CategoryId { get; set; }

        public Request ToEntity()
        {
            Validate();
            return new Request()
            {
                Description = Description,
                ApartmentId = ApartmentId,
                CategoryId = CategoryId,
                State = RequestState.OPEN,
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Description))
            {
                throw new InvalidArgumentException("description");
            }
            if (Guid.Empty.Equals(ApartmentId))
            {
                throw new InvalidArgumentException("apartmentId");
            }
            if (Guid.Empty.Equals(CategoryId))
            {
                throw new InvalidArgumentException("categoryId");
            }
        }
    }
}