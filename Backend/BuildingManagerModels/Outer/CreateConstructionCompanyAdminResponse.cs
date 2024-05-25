using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateConstructionCompanyAdminResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CreateConstructionCompanyAdminResponse(User admin)
        {
            Id = admin.Id;
            Name = admin.Name;
            Email = admin.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CreateConstructionCompanyAdminResponse)obj;
            return Id == other.Id && Name == other.Name && Email == other.Email;
        }

    }
}