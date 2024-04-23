using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateMaintenanceStaffResponse : CreateUserResponse<MaintenanceStaff>
    {
        public string Lastname { get; set; }

        public CreateMaintenanceStaffResponse(User maintenanceStaff)
        {
            Lastname = maintenanceStaff.Lastname;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (CreateMaintenanceStaffResponse)obj;
            return Id.Equals(other.Id)
                && Name.Equals(other.Name)
                && Lastname.Equals(other.Lastname)
                && Email.Equals(other.Email);
        }
    }
}
