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
            if (invitation.Status == InvitationStatus.ACCEPTED)
            {
                throw new InvalidOperationException("Invitation is not declined.");
            }
            _context.Set<Invitation>().Remove(invitation);
            _context.SaveChanges();

            return invitation;

        }

        public bool IsAccepted(Guid invitationId)
        {
            try
            {
                var invitation = _context.Set<Invitation>().First(i => i.Id == invitationId);
                return invitation.Status == InvitationStatus.ACCEPTED;
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("Invitation not found.");
            }
        }

        public Invitation ModifyInvitation(Guid id, long newDeadline)
        {
            throw new NotImplementedException();
        }

        public bool ExpiresInMoreThanOneDay(Guid id)
        {
            var invitation = _context.Set<Invitation>().First(i => i.Id == id);
            return invitation.Deadline > DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeSeconds();
        }

        public bool IsDeadlineExtensionValid(Guid id, long newDeadline)
        {
            throw new NotImplementedException();
        }
    }
}