using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateMaintenanceStaffResponse : CreateUserResponse<MaintenanceStaff>
    {
        public string Lastname { get; set; }

        public CreateMaintenanceStaffResponse(MaintenanceStaff maintenanceStaff) : base(maintenanceStaff)
        {
            Lastname = maintenanceStaff.Lastname;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            var other = (CreateMaintenanceStaffResponse)obj;
            return Lastname == other.Lastname;
        }
    }
}
