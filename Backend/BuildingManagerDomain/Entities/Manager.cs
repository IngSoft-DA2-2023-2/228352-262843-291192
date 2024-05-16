using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class Manager : User
    {
        public Manager()
        {
            Role = RoleType.MANAGER;
            Lastname = "";
        }
    }
}

