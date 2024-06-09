using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerIDataAccess.Exceptions;
using System;
using BuildingManagerLogic.Helpers;
using BuildingManagerDomain.Enums;

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
                if(invitation.Role != RoleType.MANAGER && invitation.Role != RoleType.CONSTRUCTIONCOMPANYADMIN)
                {
                    throw new ArgumentException("role");
                }
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
                return _invitationRepository.DeleteInvitation(id);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }

        public List<Invitation> GetAllInvitations(string email)
        {
            return _invitationRepository.GetAllInvitations(email);
        }

        public Invitation ModifyInvitation(Guid id, long newDeadline)
        {
            try
            {
                return _invitationRepository.ModifyInvitation(id, newDeadline);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
            catch(InvalidOperationException e)
            {
                throw e;
            }
        }

        public InvitationAnswer RespondInvitation(InvitationAnswer invitationAnswer)
        {
            try
            {
                Invitation invitation = _invitationRepository.RespondInvitation(invitationAnswer);
                User manager = null;
                if (invitation.Status == InvitationStatus.ACCEPTED)
                {
                    manager = _userRepository.CreateUser(UserFromInvitation.Create(invitationAnswer, invitation.Name));

                } 
                return InvitationResponder.CreateAnswer(manager, invitation);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }
    }
}