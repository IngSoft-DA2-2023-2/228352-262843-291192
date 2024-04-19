using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerModels.Outer
{
    public class CreateAdminResponse : CreateUserResponse<Admin>
    {
        public string Lastname { get; set; }

        public CreateAdminResponse(Admin admin) : base(admin)
        {
            Lastname = admin.Lastname;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            var other = (CreateAdminResponse)obj;
            return Lastname == other.Lastname;
        }
    }
}
