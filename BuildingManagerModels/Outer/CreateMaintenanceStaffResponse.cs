using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateMaintenanceStaffResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public CreateMaintenanceStaffResponse(MaintenanceStaff maintenanceStaff)
        {
            Id = maintenanceStaff.Id;
            Name = maintenanceStaff.Name;
            Lastname = maintenanceStaff.Lastname;
            Email = maintenanceStaff.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CreateMaintenanceStaffResponse)obj;
            return Id == other.Id && Name == other.Name && Lastname == other.Lastname && Email == other.Email;
        }

    }
}
