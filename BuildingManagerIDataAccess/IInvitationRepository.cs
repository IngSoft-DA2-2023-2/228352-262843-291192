using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        Invitation DeleteInvitation(Guid id);
        bool IsAccepted(Guid userId);
        Invitation ModifyInvitation(Guid id, long newDeadline);
    }
}