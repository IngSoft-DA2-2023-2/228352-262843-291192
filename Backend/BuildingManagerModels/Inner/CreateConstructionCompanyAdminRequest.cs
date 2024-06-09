using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateConstructionCompanyAdminRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ConstructionCompanyAdmin ToEntity()
        {
            Validate();
            return new ConstructionCompanyAdmin()
            {
                Name = this.Name,
                Lastname = this.LastName,
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