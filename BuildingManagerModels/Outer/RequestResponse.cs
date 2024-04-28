using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerModels.Outer
{
    public class RequestResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid CategoryId { get; set; }
        public RequestState State { get; set; }

        public RequestResponse(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            ApartmentId = request.ApartmentId;
            CategoryId = request.CategoryId;
            State = request.State;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (RequestResponse)obj;
            return Id == other.Id && Description == other.Description && ApartmentId == other.ApartmentId && CategoryId == other.CategoryId && State == other.State;
        }
    }
}