using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        Invitation DeleteInvitation(Guid id);
        Invitation ModifyInvitation(Guid id, long newDeadline);
        Invitation RespondInvitation(InvitationAnswer invitationAnswer);
        List<Invitation> GetAllInvitations(string email, bool? expiredOrNear, int? status);
    }
}