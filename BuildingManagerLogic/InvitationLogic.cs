using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerIDataAccess.Exceptions;

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
            try
            {
                return _invitationRepository.CreateInvitation(invitation);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }
    }
}