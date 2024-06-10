
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerLogic.Helpers
{
    public static class UserFromInvitation
    {
        public static User Create(InvitationAnswer answer, Invitation invitation)
        {
            if (answer.Status == InvitationStatus.DECLINED) { return null; }
            if(invitation.Role == RoleType.CONSTRUCTIONCOMPANYADMIN)
            {
                return new ConstructionCompanyAdmin
                {
                    Id = new System.Guid(),
                    Name = invitation.Name,
                    Email = answer.Email,
                    Role = invitation.Role,
                    Password = answer.Password,
                };
            }
            else {
                return new Manager
                {
                    Id = new System.Guid(),
                    Name = invitation.Name,
                    Email = answer.Email,
                    Role = invitation.Role,
                    Password = answer.Password,
                };
            }
        }
    }
}
