using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateOwnerRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Owner ToEntity()
        {
            Validate();
            return new Owner
            {
                Name = Name,
                LastName = LastName,
                Email = Email
            };
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new InvalidArgumentException("name");
            }
            if (string.IsNullOrEmpty(LastName))
            {
                throw new InvalidArgumentException("lastName");
            }
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidArgumentException("email");
            }
        }

    }
}
