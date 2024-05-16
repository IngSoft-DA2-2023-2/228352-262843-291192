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
        public Guid ManagerId { get; set; }
        public int? ApartmentFloor { get; set; }
        public int? ApartmentNumber { get; set; }

        public Request ToEntity()
        {
            Validate();
            return new Request()
            {
                Description = Description,
                CategoryId = CategoryId,
                BuildingId = BuildingId,
                ApartmentFloor = ApartmentFloor.Value,
                ApartmentNumber = ApartmentNumber.Value,
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
            if (Guid.Empty.Equals(BuildingId))
            {
                throw new InvalidArgumentException("buildingId");
            }
            if (string.IsNullOrEmpty(ApartmentFloor.ToString()))
            {
                throw new InvalidArgumentException("apartmentFloor");
            }
            if (string.IsNullOrEmpty(ApartmentNumber.ToString()))
            {
                throw new InvalidArgumentException("apartmentNumber");
            }
        }
    }
}