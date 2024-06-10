using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class OwnerLogic: IOwnerLogic
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerLogic(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepository.CreateOwner(owner);
        }
    }
}
