using System;
using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public RequestState State { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? MaintainerStaffId { get; set; }
        public Guid BuildingId { get; set; }
        public int ApartmentFloor { get; set; }
        public int ApartmentNumber { get; set; }
        public MaintenanceStaff MaintenanceStaff { get; set; }
        public Category Category { get; set; }
        public long AttendedAt { get; set; }
        public long CompletedAt { get; set; }
        public int Cost { get; set; }
    }
}