using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateAdminRequest
    {
        private string _name;
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null)
                {
                    throw new InvalidArgumentException("name");
                }
                _name = value;
            }
        }

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
