using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerLogic
{
    public class AdminLogic : IAdminLogic
    {
        private readonly IAdminRepository _adminRepository;
        public AdminLogic(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Admin CreateAdmin(Admin admin)
        {
            try
            {
                return _adminRepository.CreateAdmin(admin);
            }
            catch(ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, "");
            }
        }
    }
}
