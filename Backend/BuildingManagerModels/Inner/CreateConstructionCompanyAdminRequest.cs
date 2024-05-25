using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Inner
{
    public class CreateConstructionCompanyAdminRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ConstructionCompanyAdmin ToEntity()
        {
            return new ConstructionCompanyAdmin()
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
        }
    }
}