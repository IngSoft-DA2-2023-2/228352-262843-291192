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
        public Invitation InvitationByEmail(string email);
        public List<Invitation> GetAllInvitations();
    }
}