using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateOwnerResponse
    {
        public string Name { get; }
        public string LastName { get; }
        public string Email { get; }

        public CreateOwnerResponse(Owner owner)
        {
            Name = owner.Name;
            LastName = owner.LastName;
            Email = owner.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CreateOwnerResponse other = (CreateOwnerResponse)obj;
            return Name == other.Name && LastName == other.LastName && Email == other.Email;
        }

    }
}
