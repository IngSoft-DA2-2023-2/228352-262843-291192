using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateMaintenanceStaffRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set;}

        public MaintenanceStaff ToEntity()
        {
            Validate();
            return new MaintenanceStaff()
            {
                Name = this.Name,
                Lastname = this.Lastname,
                Email = this.Email,
                Password = this.Password
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidArgumentException("name");
            }
            if (string.IsNullOrEmpty(Lastname))
            {
                throw new InvalidArgumentException("lastname");
            }
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidArgumentException("email");
            }
            if (string.IsNullOrEmpty(Password))
            {
                throw new InvalidArgumentException("password");
            }
        }


    }
}
