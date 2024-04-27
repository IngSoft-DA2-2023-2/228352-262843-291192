using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerIDataAccess.Exceptions;
using System;

namespace BuildingManagerLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;
        public InvitationLogic(IInvitationRepository invitationRepository, IUserRepository userRepository)
        {
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
        }
        public Invitation CreateInvitation(Invitation invitation)
        {
            try
            {
                if (_userRepository.EmailExists(invitation.Email))
                {
                    ValueDuplicatedException e = new ValueDuplicatedException("Email");
                    throw new DuplicatedValueException(e, e.Message);
                }
                return _invitationRepository.CreateInvitation(invitation);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }

        public Invitation DeleteInvitation(Guid id)
        {
            try
            {
                if (_invitationRepository.IsAccepted(id))
                {
                    throw new InvalidOperationException("Invitation is not declined.");
                }
                return _invitationRepository.DeleteInvitation(id);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Invitation ModifyInvitation(Guid id, long newDeadline)
        {
            try
            {
                if (_invitationRepository.IsAccepted(id))
                {
                    throw new InvalidOperationException("Invitation was accepted.");
                }
                return _invitationRepository.ModifyInvitation(id, newDeadline);
            } 
            catch(ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }
    }
}