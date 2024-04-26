using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
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
            _context.Set<Invitation>().Add(invitation);
            _context.SaveChanges();

            return invitation;
        }

        public Invitation DeleteInvitation(Guid id)
        {
            Invitation invitation = _context.Set<Invitation>().First(i => i.Id == id);
            _context.Set<Invitation>().Remove(invitation);
            _context.SaveChanges();

            return invitation;
        }

        public bool IsAccepted(Guid userId)
        {
            return _context.Set<Invitation>().First(i => i.Id == userId).Status == InvitationStatus.ACCEPTED;
        }
    }
}