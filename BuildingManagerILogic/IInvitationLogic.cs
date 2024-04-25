using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IInvitationLogic
    {
        public Invitation CreateInvitation(Invitation invitation);
        public Invitation DeleteInvitation(Guid id);
    }
}