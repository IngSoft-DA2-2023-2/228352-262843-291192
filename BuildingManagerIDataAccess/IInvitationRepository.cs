using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        Invitation DeleteInvitation(Guid id);
        Invitation ModifyInvitation(Guid id, long newDeadline);
    }
}