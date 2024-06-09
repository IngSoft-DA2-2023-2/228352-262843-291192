using System;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerIDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        Invitation DeleteInvitation(Guid id);
        Invitation ModifyInvitation(Guid id, long newDeadline);
        Invitation RespondInvitation(InvitationAnswer invitationAnswer);
        Invitation GetInvitationByEmail(string email);
    }
}