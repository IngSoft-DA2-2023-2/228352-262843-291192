using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IInvitationLogic
    {
        public Invitation CreateInvitation(Invitation invitation);
        public Invitation DeleteInvitation(Guid id);
        public Invitation ModifyInvitation(Guid id, long newDeadline);
        public InvitationAnswer RespondInvitation(InvitationAnswer invitationAnswer);
        public List<Invitation> GetAllInvitations(string? email = null, bool? expiredOrNear = null, int? status = null);
    }
}