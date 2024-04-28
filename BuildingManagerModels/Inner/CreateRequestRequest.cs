using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateRequestRequest
    {
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BuildingId { get; set; }
        public int ApartmentFloor { get; set; }
        public int ApartmentNumber { get; set; }

        public Request ToEntity()
        {
            Validate();
            return new Request()
            {
                Description = Description,
                CategoryId = CategoryId,
                BuildingId = BuildingId,
                ApartmentFloor = ApartmentFloor,
                ApartmentNumber = ApartmentNumber,
                State = RequestState.OPEN,
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Description))
            {
                throw new InvalidArgumentException("description");
            }
            if (Guid.Empty.Equals(CategoryId))
            {
                throw new InvalidArgumentException("categoryId");
            }
        }
    }
}