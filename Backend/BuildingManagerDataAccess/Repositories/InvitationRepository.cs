using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            if(invitation.Role != RoleType.MANAGER && invitation.Role != RoleType.CONSTRUCTIONCOMPANYADMIN)
            {
                throw new InvalidOperationException("Role not allowed");
            }
            if (_context.Set<Invitation>().Any(i => i.Email == invitation.Email))
            {
                Invitation invitationInDb = _context.Set<Invitation>().First(i => i.Email == invitation.Email);
                if(invitationInDb.Status == InvitationStatus.DECLINED)
                {
                    invitationInDb.Status = InvitationStatus.PENDING;
                    invitationInDb.Deadline = invitation.Deadline;
                    invitationInDb.Name = invitation.Name;
                    invitationInDb.Role = invitation.Role;
                    _context.SaveChanges();
                    return invitationInDb;
                }
                else{
                    throw new ValueDuplicatedException("Email");
                }
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
        public Invitation RespondInvitation(InvitationAnswer invitationAnswer)
        {
            Invitation invitation = _context.Set<Invitation>().FirstOrDefault(i => i.Email == invitationAnswer.Email);

            if (invitation == null)
            {
                throw new ValueNotFoundException("Invitation not found.");
            }
            if(invitation.Status == InvitationStatus.ACCEPTED)
            {
                throw new InvalidOperationException("Invitation was accepted.");
            }
            if(invitation.Status == InvitationStatus.DECLINED)
            {
                throw new InvalidOperationException("Invitation was rejected.");
            }
            if (invitation.Deadline < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                throw new InvalidOperationException("Invitation expired.");
            }
            invitation.Status = invitationAnswer.Status;
            _context.SaveChanges();
            return invitation;
        }

        public List<Invitation> GetAllInvitations(string email, bool? expiredOrNear, int? status)
        {
            IQueryable<Invitation> query = _context.Set<Invitation>();
            if (email != null)
            {
                query = query.Where(invitation => invitation.Email == email);
            }

            if (expiredOrNear.HasValue && expiredOrNear.Value)
            {
                long unixTimestampNow = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
                long unixTimestamp24HoursAhead = unixTimestampNow + 86400;

                query = query.Where(invitation =>
                    invitation.Deadline <= unixTimestampNow ||
                    (invitation.Deadline > unixTimestampNow && invitation.Deadline <= unixTimestamp24HoursAhead)
                );
            }
            return query.ToList();
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