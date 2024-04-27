using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly DbContext _context;
        public InvitationRepository(DbContext context)
        {
            _context = context;
        }
        public Invitation CreateInvitation(Invitation invitation)
        {
            if (_context.Set<Invitation>().Any(i => i.Email == invitation.Email))
            {
                throw new ValueDuplicatedException("Email");
            }
            _context.Set<Invitation>().Add(invitation);
            _context.SaveChanges();

            return invitation;
        }

        public Invitation DeleteInvitation(Guid id)
        {
            Invitation invitation;
            try
            {
                invitation = _context.Set<Invitation>().First(i => i.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("Invitation not found.");
            }
            ThrowExceptionIfIsAccepted(invitation);
            _context.Set<Invitation>().Remove(invitation);
            _context.SaveChanges();

            return invitation;

        }

        public Invitation ModifyInvitation(Guid id, long newDeadline)
        {
            Invitation invitation;
            try
            {
                invitation = _context.Set<Invitation>().First(i => i.Id == id);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("Invitation not found.");
            }

            ThrowExceptionIfIsAccepted(invitation);
            ThrowExceptionIfExpiresInMoreThanOneDay(invitation);
            ThrowExceptionIfDeadlineExtensionIsInvalid(invitation, newDeadline);

            invitation.Deadline = newDeadline;
            _context.SaveChanges();

            return invitation;
        }
        private static void ThrowExceptionIfIsAccepted(Invitation invitation)
        {
            if (invitation.Status == InvitationStatus.ACCEPTED)
            {
                throw new InvalidOperationException("Invitation was accepted.");
            }
        }

        private static void ThrowExceptionIfExpiresInMoreThanOneDay(Invitation invitation)
        {
            if (invitation.Deadline > DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeSeconds())
            {
                throw new InvalidOperationException("Invitation will expire in more than one day.");
            }
        }

        private static void ThrowExceptionIfDeadlineExtensionIsInvalid(Invitation invitation, long newDeadline)
        {
            if (newDeadline < invitation.Deadline)
            {
                throw new InvalidOperationException("New deadline must be greater than the current deadline.");
            }
        }
    }
}