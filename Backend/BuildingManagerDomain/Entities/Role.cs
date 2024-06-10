using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class Role
    {
        public Guid UserId { get; set; }
        public RoleType UserRole { get; set; }
    }
}
