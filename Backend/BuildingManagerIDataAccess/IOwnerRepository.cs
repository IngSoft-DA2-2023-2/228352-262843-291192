using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IOwnerRepository
    {
        public Owner CreateOwner(Owner owner);
    }
}
