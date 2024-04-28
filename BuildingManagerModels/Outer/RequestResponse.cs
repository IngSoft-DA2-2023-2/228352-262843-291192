using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerModels.Outer
{
    public class RequestResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public RequestState State { get; set; }
        public Guid BuildingId { get; set; }
        public int ApartmentFloor { get; set; }

        public RequestResponse(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            CategoryId = request.CategoryId;
            BuildingId = request.BuildingId;
            ApartmentFloor = request.ApartmentFloor;
            State = request.State;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (RequestResponse)obj;
            return Id == other.Id && Description == other.Description && BuildingId == other.BuildingId && CategoryId == other.CategoryId && State == other.State && ApartmentFloor == other.ApartmentFloor;
        }
    }
}