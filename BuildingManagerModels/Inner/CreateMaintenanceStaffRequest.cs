using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateMaintenanceStaffRequest : CreateUserRequest<MaintenanceStaff>
    {
        public string Lastname { get; set; }

        public override MaintenanceStaff ToEntity()
        {
            var entity = base.ToEntity();
            entity.Lastname = this.Lastname;
            return entity;
        }

        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(Lastname))
            {
                throw new InvalidArgumentException("lastname");
            }
        }
    }
}
