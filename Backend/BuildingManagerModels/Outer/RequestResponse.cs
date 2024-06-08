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
        public string BuildingName { get; set; }
        public Guid ManagerId { get; set; }
        public int ApartmentFloor { get; set; }
        public int ApartmentNumber { get; set; }
        public long AttendedAt { get; set; }
        public long CompletedAt { get; set; }
        public int Cost { get; set; }
        public Guid MaintainerStaffId { get; set; }
        public string MaintainerStaffName { get; set; }
        public string CategoryName { get; set; }

        public RequestResponse(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            CategoryId = request.CategoryId;
            BuildingId = request.BuildingId;
            ManagerId = request.ManagerId;
            ApartmentFloor = request.ApartmentFloor;
            ApartmentNumber = request.ApartmentNumber;
            State = request.State;
            BuildingName = request.Building?.Name ?? "";
            AttendedAt = request.AttendedAt;
            CompletedAt = request.CompletedAt;
            Cost = request.Cost;
            MaintainerStaffId = request.MaintainerStaffId ?? Guid.Empty;
            MaintainerStaffName = request.MaintenanceStaff?.Name ?? "";
            CategoryName = request.Category?.Name ?? "";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (RequestResponse)obj;
            return Id == other.Id &&
            Description == other.Description &&
            BuildingId == other.BuildingId &&
            CategoryId == other.CategoryId &&
            ManagerId == other.ManagerId &&
            State == other.State &&
            ApartmentFloor == other.ApartmentFloor &&
            ApartmentNumber == other.ApartmentNumber &&
            BuildingName == other.BuildingName &&
            AttendedAt == other.AttendedAt &&
            CompletedAt == other.CompletedAt &&
            Cost == other.Cost &&
            MaintainerStaffId == other.MaintainerStaffId &&
            MaintainerStaffName == other.MaintainerStaffName;
        }
    }
}