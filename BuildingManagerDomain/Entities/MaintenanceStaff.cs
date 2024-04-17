using System;

namespace BuildingManagerDomain.Entities
{
    public class MaintenanceStaff
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
