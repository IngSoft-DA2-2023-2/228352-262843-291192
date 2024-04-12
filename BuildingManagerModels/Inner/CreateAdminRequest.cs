using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateAdminRequest
    {
        private string _name;
        private string _lastname;
        private string _email;
        private string _password;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("lastname");
                }
                _lastname = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("email");
                }
                _email = value;
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidArgumentException("password");
                }
                _password = value;
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
