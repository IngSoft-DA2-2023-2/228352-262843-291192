using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public abstract class CreateUserRequest<T> where T : User, new()
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual T ToEntity()
        {
            Validate();
            return new T()
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
        }

        public virtual void Validate()
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
