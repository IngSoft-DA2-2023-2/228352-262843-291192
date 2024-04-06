using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Inner
{
    public class CreateAdminRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Admin ToEntity()
        {
            return new Admin()
            {
                Name = this.Name,
                Lastname = this.Lastname,
                Email = this.Email,
                Password = this.Password
            };
        }
        

    }
}
