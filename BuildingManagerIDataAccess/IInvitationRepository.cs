using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
    }
}