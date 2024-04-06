using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateAdminResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public CreateAdminResponse(Admin admin)
        {
            Id = admin.Id;
            Name = admin.Name;
            Lastname = admin.Lastname;
            Email = admin.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CreateAdminResponse)obj;
            return Id == other.Id && Name == other.Name && Lastname == other.Lastname && Email == other.Email;
        }

    }
}
