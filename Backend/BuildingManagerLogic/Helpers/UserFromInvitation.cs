
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerLogic.Helpers
{
    public static class UserFromInvitation
    {
        public static Manager Create(InvitationAnswer answer, string name)
        {
            if (answer.Status == InvitationStatus.DECLINED) { return null; }
            return new Manager
            {
                Id = new System.Guid(),
                Name = name,
                Email = answer.Email,
                Role = RoleType.MANAGER,
                Password = answer.Password,
            };
        }
    }
}
