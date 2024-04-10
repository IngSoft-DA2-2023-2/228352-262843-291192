using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateAdminRequest
    {
        private string _name;
        private string _lastname;
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
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (value == null)
                {
                    throw new InvalidArgumentException("lastname");
                }
                _lastname = value;
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
