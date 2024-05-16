using System;

namespace BuildingManagerDomain.Entities
{
    public class Admin: User
    {
        public Admin()
        {
            Role = Enums.RoleType.ADMIN;
        }
    }
}
