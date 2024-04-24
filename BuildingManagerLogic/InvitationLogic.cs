using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using System;

namespace BuildingManagerLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        public InvitationLogic(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }
        public Invitation CreateInvitation(Invitation invitation)
        {
           return _invitationRepository.CreateInvitation(invitation);
        }
    }
}